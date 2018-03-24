using NUnit.Framework;
using SurveyShipsApp;
using System;
using System.Collections.Generic;

namespace SurveyShipsTests
{
	[TestFixture]
	public class UnitTests
	{
		#region Input
		public static IEnumerable<TestCaseData> first_line_should_be_valid_cases
		{
			get
			{
				yield return new TestCaseData("1 1", 1, 1);
				yield return new TestCaseData("5 3", 5, 3);
				yield return new TestCaseData("50 50", 50, 50);
				yield return new TestCaseData("0 0", 0, 0);
				yield return new TestCaseData("   3 5    ", 3, 5); // leading and trailing spaces should be ignores
			}
		}

		[TestCaseSource("first_line_should_be_valid_cases")]
		public void first_line_should_be_valid(string input, int x, int y)
		{
			var grid = new Grid();
			grid.SetCoordinates(input);
			Assert.That(grid.IsValid, Is.True);
			Assert.That(grid.X, Is.EqualTo(x));
			Assert.That(grid.Y, Is.EqualTo(y));
		}

		// first line should fail validation if the input is not "[X] [Y]"
		// first line should fail validation if either coordinate is greater than 50
		[TestCase("")] 
		[TestCase("5")] 
		[TestCase("5 ")] 
		[TestCase("5 -6")] 
		[TestCase("50 51")] 
		[TestCase("A 1")] 
		public void first_line_should_fail_validation_validated(string input)
		{
			var grid = new Grid();
			Assert.Throws<ArgumentException>(() => grid.SetCoordinates(input));
		}

		// second line should fail validation if the input is not "[X] [Y] [orientation]"
		// second line should fail if the coordinates do not fall within the grid
		[TestCase("1 1 N", true)] // valid
		[TestCase("1 1 S", true)] // valid
		[TestCase("1 1 E", true)] // valid
		[TestCase("1 1 W", true)] // valid
		[TestCase("50 50 N", true)] // valid
		[TestCase("50 51 N", false)] // invalid - no on the grid
		[TestCase("", false)] // invalid
		[TestCase("5", false)] // invalid
		[TestCase("5 ", false)] // invalid
		[TestCase("5 -6", false)] // invalid
		[TestCase("5 -6 N", false)] // invalid
		[TestCase("0 0", false)] // invalid
		[TestCase("50 51", false)] // invalid
		[TestCase("A 1 N", false)] // invalid
		[TestCase("1 1 B", false)] // invalid
		[TestCase("1 1 ", false)] // invalid
		[TestCase("1 1 1", false)] // invalid
		public void second_line_should_be_validated(string input, bool expected)
		{
			var grid = new Grid();
			grid.SetCoordinates("50 50"); // valid grid.

			var ship = new Ship(grid);
			if (!expected)
				Assert.Throws<ArgumentException>(() => ship.SetPosition(input));
			else
			{
				ship.SetPosition(input);
				Assert.That(ship.IsValidPosition, Is.True);
			}
		}

		// third line should fail validation if not a combo of "L", "R" and "F"
		// third line should fail if >= 100 instructions
		[TestCase("LRF", true)] // valid
		[TestCase("LLL", true)] // invalid
		[TestCase("LLM", false)] // invalid character
		[TestCase("", false)] // invalid
		[TestCase("1", false)] // invalid
		[TestCase("LLLLLLLLLLRRRRRRRRRRFFFFFFFFFFLLLLLLLLLLRRRRRRRRRRFFFFFFFFFFLLLLLLLLLLRRRRRRRRRRFFFFFFFFFFLLLLLLLLL", true)] // valid - 99 chars
		[TestCase("LLLLLLLLLLRRRRRRRRRRFFFFFFFFFFLLLLLLLLLLRRRRRRRRRRFFFFFFFFFFLLLLLLLLLLRRRRRRRRRRFFFFFFFFFFLLLLLLLLLL", true)] // invalid - 100 chars
		public void third_line_should_be_validated(string input, bool expected)
		{
			var grid = new Grid();
			grid.SetCoordinates("50 50"); // valid grid.

			var ship = new Ship(grid);
			ship.SetPosition("10 10 N"); // valid position.

			if (!expected)
				Assert.Throws<ArgumentException>(() => ship.SetInstructions(input));
			else
			{
				ship.SetInstructions(input);
				Assert.That(ship.IsValidInstructions, Is.True);
			}
		}

		// all subsequent lines should be ship position then instructions
		[Test]
		public void subsequent_lines_should_be_ship_position_then_instructions()
		{
			Assert.Fail();
		}
		#endregion Input

		#region Output
		// should indicate the final grid position and orientation of each ship
		[Test]
		public void should_indicate_the_final_grid_position_and_orientation_of_each_ship()
		{
			var grid = new Grid();
			grid.SetCoordinates("5 3");

			var ship = new Ship(grid);
			ship.SetPosition("1 1 E");
			ship.SetInstructions("RFRFRFRF");
			ship.Go();

			Assert.That(ship.ToString(), Is.EqualTo("1 1 E"));
		}

		// should indicate lost if a ship falls off the edge of the grid
		[Test]
		public void should_indicate_lost_if_a_ship_falls_off_the_edge_of_the_grid()
		{
			var grid = new Grid();
			grid.SetCoordinates("5 3");

			var ship = new Ship(grid);
			ship.SetPosition("1 1 E");
			ship.SetInstructions("FFFFF");
			ship.Go();

			Assert.That(ship.ToString(), Is.EqualTo("5 1 E LOST"));
		}

		// should ignore instruction if warning indicates that a ship has already fallen off at that grid point
		[Test]
		public void should_ignore_instruction_if_warning_indicates_that_a_ship_has_already_fallen_off_at_that_grid_point()
		{
			Assert.Fail();
		}

		// should work like given example output
		[Test]
		public void should_work_like_given_example_output()
		{
			var grid = new Grid();
			grid.SetCoordinates("5 3");

			var ships = new List<Ship>();
			var addShip = new Action<string, string>((position, instructions) =>
			{
				var ship = new Ship(grid);
				ship.SetPosition(position);
				ship.SetInstructions(instructions);
				ships.Add(ship);
			});

			addShip("1 1 E", "RFRFRFRF");
			addShip("3 2 N", "FRRFLLFFRRFLL");
			addShip("0 3 W", "LLFFFLFLFL");

			foreach (var ship in ships)
			{
				ship.Go();
			}

			Assert.That(ships[0].ToString(), Is.EqualTo("1 1 E"));
			Assert.That(ships[1].ToString(), Is.EqualTo("3 3 N LOST"));
			Assert.That(ships[2].ToString(), Is.EqualTo("2 3 S"));
		}
		#endregion Output
	}
}
