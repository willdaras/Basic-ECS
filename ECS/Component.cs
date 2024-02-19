using System.Text.Json.Serialization;
using ECS.Core.Components;

namespace ECS;

/// <summary>
/// A component, a container for data.
/// </summary>
[JsonPolymorphic(TypeDiscriminatorPropertyName = "$component")]
[JsonDerivedType(typeof(Position), typeDiscriminator: "position")]
[JsonDerivedType(typeof(Name), typeDiscriminator: "name")]
[JsonDerivedType(typeof(Physics), typeDiscriminator: "physics")]
[JsonDerivedType(typeof(Collider), typeDiscriminator: "collider")]
public abstract class Component { }