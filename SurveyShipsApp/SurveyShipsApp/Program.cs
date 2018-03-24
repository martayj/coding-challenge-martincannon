using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyShipsApp
{


	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("--- Survey Ships ---");

			// Get the grid size
			var grid = new Grid();
			while (!grid.IsValid)
			{
				Console.WriteLine("Please enter grid top-right co-ordinates:");
				var gridLine = Console.ReadLine();
				try
				{
					grid.SetCoordinates(gridLine);
				}
				catch (ArgumentException ex)
				{
					Console.WriteLine("Invalid grid co-ordinates: " + ex.Message);
				}
			}

		}
	}
}
