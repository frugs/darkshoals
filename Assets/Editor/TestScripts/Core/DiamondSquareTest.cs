using Frugs.Darkshoals.Core.HeightMapGeneration.Util;
using NUnit.Framework;

namespace Frugs.Darkshoals.Editor.Core
{
	public class DiamondSquareTest {

		[Test]
		public void DiamondSquareWithFixedValueFuncCreates5X5Plane()
		{
			var map = DiamondSquare.CreateHeightMap(5, 1f);
			DiamondSquare.Apply(ref map, null, () => 0f);
			
			Assert.That(map, Has.All.EqualTo(1f));
		}
	}
}
