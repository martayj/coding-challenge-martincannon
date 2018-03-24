using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyShipsApp
{
	public class Ship
	{
		public int X { get; private set; }
		public int Y { get; private set; }
		public char Orientation { get; private set; }

		public void SetPosition(int x, int y, char orientation)
		{
			// validate the coordinates
			// validate the orientation
		}
	}
}
