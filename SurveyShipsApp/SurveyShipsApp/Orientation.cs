using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyShipsApp
{
	// Let's use Enumeration class to do clever stuff with orientation
	public class Orientation : Enumeration
	{
		public static readonly Orientation North = new Orientation(0, "North", 'N');
		public static readonly Orientation East = new Orientation(1, "East", 'E');
		public static readonly Orientation South = new Orientation(2, "South", 'S');
		public static readonly Orientation West = new Orientation(3, "West", 'W');

		public char Instruction { get; private set; }

		public Orientation() { }
		private Orientation(int value, string displayName, char instruction) : base(value, displayName)
		{
			Instruction = instruction;
		}

		public static Orientation Rotate(Orientation orientation, bool left)
		{
			var max = Enumeration.GetAll<Orientation>().Max(x => x.Value);
			if (left)
				// rotate left
				return Orientation.FromValue<Orientation>(orientation.Value == 0 ? max : orientation.Value - 1);
			else
				// rotate right
				return Orientation.FromValue<Orientation>(orientation.Value == max ? 0 : orientation.Value + 1);
		}

		public static bool TryParse(string input, out Orientation orientation)
		{
			orientation = (!string.IsNullOrWhiteSpace(input) && input.Length == 1)
				? Enumeration.GetAll<Orientation>().FirstOrDefault(x => x.Instruction == input[0])
				: null;
			return orientation != null;
		}
	};
}
