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
		public bool IsValid { get; private set; } = false;
		public int X { get; private set; }
		public int Y { get; private set; }
		public char Orientation { get; private set; }

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
				throw new ArgumentException("Invalid x co-oridinate");

			if (!int.TryParse(s[1], out y))
				throw new ArgumentException("Invalid y co-oridinate");

			// Must be on the grid
			if (x > _grid.X || y > _grid.Y)
				throw new ArgumentException($"Co-oridinates are not on the grid");

			// Cannot be negative
			if (x < 0 || y < 0)
				throw new ArgumentException($"Co-oridinates cannot be negative");

			// validate the orientation
			var accepted = new[] { 'N', 'S', 'E', 'W' };
			if (s[2].Length > 1 || !accepted.Contains(s[2][0]))
				throw new ArgumentException($"Orientation must be " + string.Join(", ", accepted));

			// Valid co-ordinates so set the properties and the valid flag.
			this.X = x;
			this.Y = x;
			this.Orientation = s[2][0];
			this.IsValid = true;
		}


	}
}
