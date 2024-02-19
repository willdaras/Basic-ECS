using Microsoft.Xna.Framework;
using System;

public static class ExtensionMethods
{
	public static void FromPoint(this Vector2 vector, Point point)
	{
		vector.X = point.X;
		vector.Y = point.Y;
	}
	public static Point FromVector(this Point point, Vector2 vector)
	{
		//point.X = (int)vector.X;
		//point.Y = (int)vector.Y;
		point.X = (int)MathF.Round(vector.X);
		point.Y = (int)MathF.Round(vector.Y);
		return point;
	}
}