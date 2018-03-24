using NUnit.Framework;
using SurveyShipsApp;
using System;

namespace SurveyShipsTests
{
	[TestFixture]
	public class UnitTests
	{
		#region Input
		// first line should fail validation if the input is not "[X] [Y]"
		// first line should fail validation if either coordinate is greater than 50
		[TestCase("1 1", true)] // valid
		[TestCase("5 3", true)] // valid
		[TestCase("50 50", true)] // valid
		[TestCase("", false)] // invalid
		[TestCase("5", false)] // invalid
		[TestCase("5 ", false)] // invalid
		[TestCase("5 -6", false)] // invalid
		[TestCase("0 0", true)] // valid
		[TestCase("50 51", false)] // invalid
		[TestCase("A 1", false)] // invalid
		[TestCase("   3 5    ", true)] // valid - leading and trailing spaces should be ignores
		public void first_line_should_be_validated(string input, bool expected)
		{
			var grid = new Grid();
			if (!expected)
				Assert.Throws<ArgumentException>(() => grid.SetCoordinates(input));
			else
			{
				grid.SetCoordinates(input);
				Assert.That(grid.IsValid, Is.True);
			}
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
			Assert.Fail();
		}

		// should indicate lost if a ship falls off the edge of the grid
		[Test]
		public void should_indicate_lost_if_a_ship_falls_off_the_edge_of_the_grid()
		{
			Assert.Fail();
		}

		// should ignore instruction if warning indicates that a ship has already fallen off at that grid point
		[Test]
		public void should_ignore_instruction_if_warning_indicates_that_a_ship_has_already_fallen_off_at_that_grid_point()
		{
			Assert.Fail();
		}
		#endregion Output
	}
}
