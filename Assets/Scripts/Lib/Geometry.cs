using UnityEngine;

public class Geometry
{
	public const float SCALE = 1.2F;
	public const int PERIODICITY = 12;

	public static TransformedPoint Transform(Point point)
	{
		float scale = Mathf.Pow(SCALE, point.X);
		float rotation = point.Y * 360F / PERIODICITY;
		return new TransformedPoint(scale, rotation);
	}

	public static TransformedPoint Transform(Slot slot)
	{
		float scale = Mathf.Pow(SCALE, slot.X);
		float rotation = slot.Y * 360F / PERIODICITY;
		return new TransformedPoint(scale, rotation);
	}

	public static float RotationAngle()
		=> 360F / PERIODICITY;

	public static void MoveToPoint(Slot slot, GameObject go)
	{
		var point = Transform(slot);

		go.transform.localScale = new Vector3(point.Scale, 1, point.Scale);
		go.transform.localRotation = Quaternion.Euler(0, point.Rotation, 0);
	}
	public static void MoveToPoint(Point point, GameObject go)
	{
		var tpoint = Transform(point);

		go.transform.localScale = new Vector3(tpoint.Scale, 1, tpoint.Scale);
		go.transform.localRotation = Quaternion.Euler(0, tpoint.Rotation, 0);
	}
}

public readonly struct Point
{
	public readonly float X;
	public readonly float Y;

	public Point(float x, float y) { X = x; Y = y; }

	public static Point operator +(Point a, Point b)
		=> new Point(a.X + b.X, a.Y + b.Y);

	public static Point operator -(Point a, Point b)
		=> new Point(a.X - b.X, a.Y - b.Y);

	public static Point InvertX(Point Point)
		=> new Point(-Point.X, Point.Y);

	public static Point InvertY(Point Point)
		=> new Point(Point.X, -Point.Y);

	public override string ToString() => $"({X}, {Y})";
}

public readonly struct TransformedPoint
{
	public readonly float Scale;
	public readonly float Rotation;

	public TransformedPoint(float scale, float rotation)
	{
		Scale = scale;
		Rotation = rotation;
	}
	public override string ToString() => $"({Scale}, {Rotation})";
}

public class GridUtils
{
	public static Slot SnapToGrid(Point point)
		=> new Slot(Mathf.RoundToInt(point.X), Mathf.RoundToInt(point.Y));

	public static Slot SnapToNextX(Point point)
		=> new Slot(Mathf.FloorToInt(point.X), Mathf.RoundToInt(point.Y));

	public static Slot SnapToPreviousX(Point point)
		=> new Slot(Mathf.CeilToInt(point.X), Mathf.RoundToInt(point.Y));
}

public readonly struct Slot
{
	public readonly int X;
	public readonly int Y;

	public Slot(int x, int y)
	{
		X = x;
		Y = y;
	}

	public static Slot operator +(Slot a, Slot b)
		=> new Slot(a.X + b.X, a.Y + b.Y);

	public static Slot operator -(Slot a, Slot b)
		=> new Slot(a.X - b.X, a.Y - b.Y);

	public static Slot InvertX(Slot Slot)
		=> new Slot(-Slot.X, Slot.Y);

	public static Slot InvertY(Slot Slot)
		=> new Slot(Slot.X, -Slot.Y);

	public override string ToString() => $"({X}, {Y})";

	public Point AsPoint() => new Point(X, Y);
}

public class Utils
{
	// helper function implementing modulo operation
	// https://stackoverflow.com/questions/1082917
	public static int Mod(int k, int n)
	{
		return ((k %= n) < 0) ? k + n : k;
	}
}
