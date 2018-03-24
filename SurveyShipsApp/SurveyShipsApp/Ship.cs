using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyShipsApp
{
	public class Ship
	{
		private Grid _grid;

		/// <summary>
		/// Ctor
		/// </summary>
		public Ship(Grid grid)
		{
			_grid = grid;
		}

		// private setters so that they can only be changed by calling methods that validate
		public bool IsValidPosition { get; private set; } = false;
		public int X { get; private set; }
		public int Y { get; private set; }
		public Orientation Orientation { get; private set; }

		public bool IsValidInstructions { get; private set; } = false;
		public string Instructions { get; private set; }
		public bool IsLost { get; private set; }

		public void SetPosition(string input)
		{
			// TODO: there is some duplication here with Grid. Should refactor.
			// validate the coordinates
			if (string.IsNullOrWhiteSpace(input))
				throw new ArgumentException("Input cannot be empty");

			var s = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
			if (s.Length != 3)
				throw new ArgumentException("Input is incorrect length");

			int x, y;
			if (!int.TryParse(s[0], out x))
				throw new ArgumentException("Invalid x co-ordinate");

			if (!int.TryParse(s[1], out y))
				throw new ArgumentException("Invalid y co-ordinate");

			// Must be on the grid
			if (x > _grid.X || y > _grid.Y)
				throw new ArgumentException($"Co-ordinates are not on the grid");

			// Cannot be negative
			if (x < 0 || y < 0)
				throw new ArgumentException($"Co-ordinates cannot be negative");

			// validate the orientation
			//var accepted = new[] { 'N', 'S', 'E', 'W' };
			//if (s[2].Length > 1 || !accepted.Contains(s[2][0]))
			//	throw new ArgumentException($"Orientation must be " + string.Join(", ", accepted));
			Orientation orientation;
			if (!Orientation.TryParse(s[2], out orientation))
				throw new ArgumentException($"Orientation must be " + string.Join(", ", Orientation.GetAll<Orientation>().Select(o => o.Instruction)));

			// Valid co-ordinates so set the properties and the valid flag.
			this.X = x;
			this.Y = y;
			this.Orientation = orientation;
			this.IsValidPosition = true;
		}

		public void SetInstructions(string input)
		{
			// validate the instructions
			if (string.IsNullOrWhiteSpace(input))
				throw new ArgumentException("Input cannot be empty");

			var accepted = new[] { 'L', 'R', 'F' };
			if (input.Any(x => !accepted.Contains(x)))
				throw new ArgumentException($"Instructions must be " + string.Join(", ", accepted));

			// the instructions are valid so set the properties and valid flag.
			this.Instructions = input;
			this.IsValidInstructions = true;
		}

		public void Go()
		{
			// follow the instructions.
			for (int i = 0; i < this.Instructions.Length; i++)
			{
				switch (this.Instructions[i])
				{
					case 'L': this.Orientation = Orientation.Rotate(this.Orientation, true); break; // rotate left
					case 'R': this.Orientation = Orientation.Rotate(this.Orientation, false); break; // rotate right
					case 'F': // move forward
						{
							// Check that there isn't a warning about these co-ordinates. If there is, ignore.
							if (!_grid.CheckForWarning(this.X, this.Y, this.Orientation))
							{
								int x = this.X;
								int y = this.Y;

								// Get the next coords
								if (this.Orientation == Orientation.North)
									y++;
								else if (this.Orientation == Orientation.South)
									y--;
								else if (this.Orientation == Orientation.East)
									x++;
								else
									x--;

								if (IsOutsideBounds(x, y))
								{
									// The ship is lost! don't move, just report lost.
									this.IsLost = true;

									// issue a warning so that other ships don't suffer the same fate.
									_grid.AddWarning(this.X, this.Y, this.Orientation);

									return;
								}

								// Move.
								this.X = x;
								this.Y = y;
							}

							break;
						}
				}
			}
		}

		private bool IsOutsideBounds(int x, int y)
		{ 
			// The ship is lost if it is outside the bounds of the grid.
			return (x < 0 || x > _grid.X || y < 0 || y > _grid.Y);
		}

		public override string ToString()
		{
			// KLUDGE: probably should be an extension method.
			var output = $"{this.X} {this.Y} {this.Orientation.Instruction}";
			if (this.IsLost)
				output += " LOST";

			return output;
		}
	}
}
