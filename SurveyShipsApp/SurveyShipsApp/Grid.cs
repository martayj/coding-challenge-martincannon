using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyShipsApp
{
	public class Grid
	{
		public const int MaxCoordinate = 50;

		// private setters so that they can only be changed by calling methods that validate
		public bool IsValid { get; private set; } = false;
		public int X { get; private set; }
		public int Y { get; private set; }

		private List<Tuple<int, int, Orientation>> _warnings = new List<Tuple<int, int, Orientation>>();

		public void SetCoordinates(string input)
		{
			// validate the coordinates
			if (string.IsNullOrWhiteSpace(input))
				throw new ArgumentException("Input cannot be empty");

			var s = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
			if (s.Length != 2)
				throw new ArgumentException("Input is incorrect length");

			int x, y;
			if (!int.TryParse(s[0], out x))
				throw new ArgumentException("Invalid x co-ordinate");

			if (!int.TryParse(s[1], out y))
				throw new ArgumentException("Invalid y co-ordinate");

			// Cannot be greater than 50
			if (x > MaxCoordinate || y > MaxCoordinate)
				throw new ArgumentException($"Co-ordinates cannot exceed {MaxCoordinate}");

			// Cannot be negative
			if (x < 0 || y < 0)
				throw new ArgumentException($"Co-ordinates cannot be negative");

			// Valid co-ordinates so set the properties and the valid flag.
			this.X = x;
			this.Y = y;
			this.IsValid = true;
		}

		public void AddWarning(int x, int y, Orientation orientation)
		{
			// TODO: could use a hash of the instruction in a sorted list instead of tuple as it would be quicker.
			_warnings.Add(Tuple.Create(x, y, orientation));
		}

		public bool CheckForWarning(int x, int y, Orientation orientation)
		{
			return _warnings.Any(w => w.Item1 == x && w.Item2 == y && w.Item3 == orientation);
		}
	}
}
