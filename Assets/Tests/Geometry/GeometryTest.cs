using NUnit.Framework;

using Tetris;

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
