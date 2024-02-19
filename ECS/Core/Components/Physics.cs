using Microsoft.Xna.Framework;

namespace ECS.Core.Components;

/// <summary>
/// The physics values of an Entity.
/// </summary>
public class Physics : Component
{
	public Vector2 velocity { get; set; } = Vector2.Zero;
	public Vector2 acceleration { get; set; } = Vector2.Zero;
	public float dragScale { get; set; } = 1;
	public float xDragScale { get; set; } = 1;
	public float yDragScale { get; set; } = 1;
	public float mass { get; set; } = 1;
}