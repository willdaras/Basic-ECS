using Microsoft.Xna.Framework;

namespace ECS.Core.Components;

/// <summary>
/// The position of an Entity.
/// </summary>
public class Position : Component
{
	public Vector2 position;

	public Position() { }
	public Position(Vector2 position) => this.position = position;
}