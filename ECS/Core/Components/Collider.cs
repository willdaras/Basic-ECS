using Microsoft.Xna.Framework;
using System.Text.Json.Serialization;

namespace ECS.Core.Components;

/// <summary>
/// Represents an entity's collider.
/// </summary>
public class Collider : Component
{
	/// <summary>
	/// The last valid position of the collider.
	/// </summary>
	public Vector2 lastValidPos { get; set; } = Vector2.Zero;
	/// <summary>
	/// Whether the collider should check every position between its current position and where it's being moved to.
	/// </summary>
	public bool continuous { get; set; } = false;
	/// <summary>
	/// The offset of the collider from the Entity's position.
	/// </summary>
	public Vector2 offset { get; set; } = Vector2.Zero;
	/// <summary>
	/// The Rectangle object representing the object's collider.
	/// </summary>
	public Rectangle collider { get; set; } = Rectangle.Empty;
	/// <summary>
	/// Whether the collider is colliding and the entity it's colliding with.
	/// </summary>
	[JsonIgnore] public Entity? collidingWith;
	public bool colliding;

	/// <summary>
	/// The layer the collider collides on.
	/// </summary>
	public int collisionLayer { get; set; } = 1;
}