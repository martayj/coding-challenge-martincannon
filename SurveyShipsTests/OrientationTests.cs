using NUnit.Framework;
using SurveyShipsApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyShipsTests
{
	[TestFixture]
	class OrientationTests
	{
		// should rotate to the right
		[Test]
		public void orientation_should_rotate_right()
		{
			var isLeft = false;
			Orientation o = Orientation.North;// start north
			o = Orientation.Rotate(o, isLeft); // rotate right
			Assert.That(o, Is.EqualTo(Orientation.East)); // should be east
			o = Orientation.Rotate(o, isLeft); // rotate right
			Assert.That(o, Is.EqualTo(Orientation.South)); // should be south
			o = Orientation.Rotate(o, isLeft); // rotate right
			Assert.That(o, Is.EqualTo(Orientation.West)); // should be west
			o = Orientation.Rotate(o, isLeft); // rotate right
			Assert.That(o, Is.EqualTo(Orientation.North)); // should be back to north
		}

		// should rotate to the isLeft
		[Test]
		public void orientation_should_rotate_isLeft()
		{
			var isLeft = true;
			Orientation o = Orientation.South;// start south
			o = Orientation.Rotate(o, isLeft); // rotate isLeft
			Assert.That(o, Is.EqualTo(Orientation.East)); // should be east
			o = Orientation.Rotate(o, isLeft); // rotate isLeft
			Assert.That(o, Is.EqualTo(Orientation.North)); // should be north
			o = Orientation.Rotate(o, isLeft); // rotate isLeft
			Assert.That(o, Is.EqualTo(Orientation.West)); // should be west
			o = Orientation.Rotate(o, isLeft); // rotate isLeft
			Assert.That(o, Is.EqualTo(Orientation.South)); // should be back to south
		}

		public static IEnumerable<TestCaseData> orientation_should_parse_cases
		{
			get
			{
				yield return new TestCaseData("N", true, Orientation.North);
				yield return new TestCaseData("S", true, Orientation.South);
				yield return new TestCaseData("E", true, Orientation.East);
				yield return new TestCaseData("W", true, Orientation.West);
				yield return new TestCaseData("P", false, null);
				yield return new TestCaseData("", false, null);
				yield return new TestCaseData("NN", false, null);
			}
		}

		// should parse values
		[TestCaseSource("orientation_should_parse_cases")]
		public void orientation_should_parse(string input, bool expected, Orientation expectedOrientation)
		{
			Orientation orientation;
			Assert.That(Orientation.TryParse(input, out orientation), Is.EqualTo(expected));
			Assert.That(orientation, Is.EqualTo(expectedOrientation));
		}
	}
}
