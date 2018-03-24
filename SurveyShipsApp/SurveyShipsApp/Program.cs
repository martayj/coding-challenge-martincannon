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
			var grid = GetGrid();

			// Get the ships
			var ships = GetShips(grid);

			// Process the ships
			ProcessShips(ships);

			Console.WriteLine("Press any key to exit");
			Console.ReadKey();
		}

		private static void ProcessShips(List<Ship> ships)
		{
			foreach (var ship in ships)
			{
				ship.Go();

				Console.WriteLine(ship.ToString());
			}
		}

		private static Grid GetGrid()
		{
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
			return grid;
		}

		private static List<Ship> GetShips(Grid grid)
		{
			var ships = new List<Ship>();
			while (true)
			{
				var ship = GetShip(grid);
				if (ship == null)
					break;

				// Add the ship to the fleet.
				ships.Add(ship);
			}

			return ships;
		}

		private static Ship GetShip(Grid grid)
		{
			Console.WriteLine("Please enter ship co-ordinates and orientation [X Y O], or press return finish:");

			var ship = new Ship(grid);

			// Get the position
			do
			{
				var positionLine = Console.ReadLine();
				if (string.IsNullOrWhiteSpace(positionLine))
					return null;

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
			do
			{
				var instructionsLine = Console.ReadLine();
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

			return ship;
		}
	}
}
