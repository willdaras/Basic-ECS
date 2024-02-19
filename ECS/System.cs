using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace ECS;

/// <summary>
/// A System - looks for entities with certain components and acts on them.
/// </summary>
public abstract class System
{
	/// <summary>
	/// The components the system looks for entities with.
	/// </summary>
	public abstract Type[] RequiredComponents { get; }

	/// <summary>
	/// Updates the system, acts on the entities with the required components.
	/// </summary>
	/// <param name="entities"> The entitymap to look for entities in. </param>
	/// <param name="deltaTime"> The time since the previous frame. </param>
	public abstract void Update(EntityMap entities, float deltaTime);
}