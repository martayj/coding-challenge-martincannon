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
				Console.WriteLine("Please enter grid top-right co-ordinates [X Y]:");
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

			// Get the ships
			Console.WriteLine("Please enter ship co-ordinates and orientation [X Y O], or press return finish:");
			var ships = new List<Ship>();
			while (true)
			{
				var positionLine = Console.ReadLine();
				if (string.IsNullOrWhiteSpace(positionLine))
					break;

				var ship = new Ship(grid);

				// Get the position
				do
				{
					try
					{
						ship.SetPosition(positionLine);
					}
					catch (ArgumentException ex)
					{
						Console.WriteLine("Invalid ship position: " + ex.Message);
					}
				}
				while (!ship.IsValidPosition);

				// Get the instructions
				Console.WriteLine("Please enter ship instructions (L, R, F):");
				var instructionsLine = Console.ReadLine();
				do
				{
					try
					{
						ship.SetInstructions(instructionsLine);
					}
					catch (ArgumentException ex)
					{
						Console.WriteLine("Invalid instructions: " + ex.Message);
					}
				}
				while (!ship.IsValidInstructions);

				// Add the ship to the fleet.
				ships.Add(ship);
			}

			// if we're here and we have ships in our fleet then set sail!
			foreach (var ship in ships)
			{
				ship.Go();

				var output = $"{ship.X} {ship.Y}";
				if (ship.IsLost)
				{
					output += " LOST";

					// TODO: record that the ship was lost at these coordinates.
					//_grid.AddWarning(ship.X, ship.Y);
				}
				Console.WriteLine(output);
			}

			Console.WriteLine("Press any key to finish");
			Console.ReadKey();
		}
	}
}
