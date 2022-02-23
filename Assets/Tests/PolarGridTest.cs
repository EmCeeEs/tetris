using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class GeometryTest
{
	[Test]
	public void TransformTest()
	{
		Assert.AreEqual(
			new TransformedPoint(1F, 0),
			Geometry.Transform(
				new Slot(0, 0)
			),
			"Slot(0,0) is at unit scale and no rotation."
		);
		Assert.AreEqual(
			new TransformedPoint(1.2F, 0),
			Geometry.Transform(
				new Slot(1, 0)
			),
			"Slot(1,0) is scaled to 1.2F."
		);
		Assert.AreEqual(
			new TransformedPoint(1.44F, 0),
			Geometry.Transform(
				new Slot(2, 0)
			),
			"Slot(2,0) is scaled to 1.44F."
		);
		Assert.AreEqual(
			new TransformedPoint(1F, 30F),
			Geometry.Transform(
				new Slot(0, 1)
			),
			"Slot(0,1) is rotated by 30."
		);
		Assert.AreEqual(
			new TransformedPoint(1F, 360F),
			Geometry.Transform(
				new Slot(0, 12)
			),
			"Slot(0,12) is rotated by 360."
		);
	}
}

// [TestFixture]
// public class PolarGridTest
// {
// 	[Test]
// 	public void GetScaleTest()
// 	{
// 		PolarGrid grid = new PolarGrid();

// 		Assert.AreEqual(
// 			1,
// 			grid.GetScale(new Slot(0, 0)),
// 			"Scale of (0, 0) is 1."
// 		);
// 		Assert.AreEqual(
// 			1.2F,
// 			grid.GetScale(new Slot(1, 0)),
// 			"Scale of (1, 0) is 1.2."
// 		);
// 		Assert.AreEqual(
// 			1.44F,
// 			grid.GetScale(new Slot(2, 0)),
// 			"Scale of (2, 0) is 1.44."
// 		);
// 		Assert.AreEqual(
// 			1.44F,
// 			grid.GetScale(new Slot(2, -1)),
// 			"Scale of (2, -1) is 1.44."
// 		);
// 		Assert.AreEqual(
// 			1.44F,
// 			grid.GetScale(new Slot(2, 7)),
// 			"Scale of (2, 7) is 1.44."
// 		);
// 		Assert.AreEqual(
// 			6.1917F,
// 			grid.GetScale(new Slot(10, 0)),
// 			0.0001F,
// 			"Scale of (0, 0) is 6.1917."
// 		);
// 	}

// 	[Test]
// 	public void GetRotationTest()
// 	{
// 		PolarGrid grid = new PolarGrid();

// 		Assert.AreEqual(
// 			0,
// 			grid.GetRotation(new Slot(0, 0)),
// 			"Rotation of (0, 0) is 0."
// 		);
// 		Assert.AreEqual(
// 			30,
// 			grid.GetRotation(new Slot(0, 1)),
// 			"Rotation of (0, 1) is 30 degrees."
// 		);
// 		Assert.AreEqual(
// 			330,
// 			grid.GetRotation(new Slot(0, -1)),
// 			"Rotation of (0, 1) is 330 degrees."
// 		);
// 		Assert.AreEqual(
// 			0,
// 			grid.GetRotation(new Slot(7, 0)),
// 			"Rotation of (7, 0) is 0."
// 		);
// 		Assert.AreEqual(
// 			30,
// 			grid.GetRotation(new Slot(7, 1)),
// 			"Rotation of (7, 1) is 30 degrees."
// 		);
// 		Assert.AreEqual(
// 			330,
// 			grid.GetRotation(new Slot(7, -1)),
// 			"Rotation of (7, -1) is 330 degrees."
// 		);
// 	}
// }
