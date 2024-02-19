using ECS.Core.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ECS;

/// <summary>
/// Keeps track of entities and sorts them by components, along the lines of the Archetype ECS design.
/// </summary>
public class EntityMap : IEnumerable<Entity>
{
	private List<Entity> _entities = new List<Entity>();
	private Dictionary<Type, HashSet<Entity>> _entityLists = new Dictionary<Type, HashSet<Entity>>();

	public EntityMap()
	{

	}

	public EntityMap(IEnumerable<Entity> entities)
	{
		foreach (Entity entity in entities)
		{
			AddNewEntity(entity);
		}
	}

	/// <summary>
	/// Resets the entitymap, a way to reuse the map with a new set of objects.
	/// </summary>
	/// <param name="entities"> The entities to sort after clearing. </param>
	public void Reset(IEnumerable<Entity> entities)
	{
		_entities = new List<Entity>();
		_entityLists = new Dictionary<Type, HashSet<Entity>>();
		foreach (Entity entity in entities)
		{
			AddNewEntity(entity);
		}
	}

	/// <summary>
	/// Adds an entity to the map and sorts it according to its components.
	/// </summary>
	/// <param name="entity"> The entity to be added and sorted. </param>
	public void AddNewEntity(Entity entity)
	{
		if (entity == null || _entities.Contains(entity)) return;
		_entities.Add(entity);
		foreach (Component component in entity.Components)
		{
			if (!_entityLists.ContainsKey(component.GetType()))
			{
				_entityLists.Add(component.GetType(), new HashSet<Entity> { entity });
			}
			else
			{
				_entityLists[component.GetType()].Add(entity);
			}
		}
	}
	/// <summary>
	/// Removes the entity from the map along with unsorting it.
	/// </summary>
	/// <param name="entity"> The entity to be removed. </param>
	public void RemoveEntity(Entity entity)
	{
		if (!_entities.Contains(entity)) { return; }
		_entities.Remove(entity);
		foreach (Component component in entity.Components)
		{
			_entityLists[component.GetType()].Remove(entity);
		}
	}

	/// <summary>
	/// For when a component gets added to an entity, tells the map to add to any associated archetypes.
	/// </summary>
	/// <remarks> Use events to trigger, potentially unsafe. </remarks>
	/// <param name="entity"> The entity the component was added to. </param>
	/// <param name="componentType"> The component type added. </param>
	public void ComponentAdded(Entity entity, Type componentType)
	{
		if (entity == null) return;
		if (!_entityLists.TryGetValue(componentType, out var componentSet))
		{
			_entityLists[componentType] = new HashSet<Entity>();
		}
		componentSet.Add(entity);
	}
	/// <summary>
	/// For when a component gets removed from an entity, tells the map to remove the entity from associated archetypes.
	/// </summary>
	/// <remarks> Use events to trigger, potentially unsafe. </remarks>
	/// <param name="entity"> The entity the component was removed from. </param>
	/// <param name="componentType"> The component type removed. </param>
	public void ComponentRemoved(Entity entity, Type componentType)
	{
		if (_entityLists.ContainsKey(componentType))
		{
			_entityLists[componentType].Remove(entity);
		}
	}

	/// <summary>
	/// Used to get all entities with all the components listed.
	/// </summary>
	/// <remarks> Main method used by systems to retrieve entities / archetypes. </remarks>
	/// <param name="componentTypes"> The list of components entities returned must have, the archetype of returned entities. </param>
	/// <returns> A list of entities with the specified components. </returns>
	public List<Entity> GetEntitiesWithComponents(params Type[] componentTypes)
	{
		if (!_entityLists.TryGetValue(componentTypes[0], out HashSet<Entity> firstSet))
			return new List<Entity>();
		HashSet<Entity> entities = new HashSet<Entity>(firstSet);
		for (int i = 1; i < componentTypes.Length; i++)
		{
			if (!_entityLists.TryGetValue(componentTypes[i], out HashSet<Entity> entitySet))
				return new List<Entity>();
			entities.IntersectWith(entitySet);
		}
		return new List<Entity>(entities);
	}

	#region Enumerator
	public IEnumerator<Entity> GetEnumerator()
	{
		return _entities.GetEnumerator();
	}
	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
	#endregion
}