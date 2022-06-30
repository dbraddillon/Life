using System.Text;

namespace Life
{
	class Program
	{
		const int rowCount = 4;
		const int columnCount = 3;

		//use the example or generate a random one
		const bool useRandom = false;

		public static void Main(string[] args)
		{
			bool[,] grid;
			if (useRandom)
			{
				grid = GetRandomGrid();
			}
			else
			{
				grid = GetExampleGrid();
			}

			Console.Clear();

			while (true)
			{
				ShowBoard(grid);
				grid = ComputeNextGeneration(grid);
			}
		}

		private static bool[,] GetExampleGrid()
		{
			bool[,] grid = { { false, true, false }, { false, false, true }, { true, true, true }, { false, false, false } };

			return grid;
		}

		private static bool[,] GetRandomGrid()
		{
			var grid = new bool[rowCount, columnCount];

			var rando = new Random();
			foreach (int row in Enumerable.Range(0, rowCount))
			{
				foreach (int column in Enumerable.Range(0, columnCount))
				{
					grid[row, column] = Convert.ToBoolean(rando.Next(0, 2));
				}
			}

			return grid;
		}

		private static bool[,] ComputeNextGeneration(bool[,] currentGrid)
		{
			var nextGeneration = new bool[rowCount, columnCount];

			// Loop through every cell 
			for (var row = 1; row < rowCount - 1; row++)
			{
				for (var column = 1; column < columnCount - 1; column++)
				{
					var aliveNeighbors = 0;
					for (var i = -1; i <= 1; i++)
					{
						for (var j = -1; j <= 1; j++)
						{
							aliveNeighbors += currentGrid[row + i, column + j] ? 1 : 0;
						}
					}

					var currentCell = currentGrid[row, column];

					aliveNeighbors -= currentCell ? 1 : 0;

					// Any live cell with fewer than two live neighbors dies, as if by underpopulation.
					if (currentCell && aliveNeighbors < 2)
					{
						nextGeneration[row, column] = false;
					}

					// Any live cell with more than three live neighbors dies, as if by overpopulation.
					else if (currentCell && aliveNeighbors > 3)
					{
						nextGeneration[row, column] = false;
					}

					// Any dead cell with exactly three live neighbors becomes a live cell, as if by reproduction.
					else if (!currentCell && aliveNeighbors == 3)
					{
						nextGeneration[row, column] = true;
					}
					else
					{
						nextGeneration[row, column] = currentCell;
					}
				}
			}
			return nextGeneration;
		}

		private static void ShowBoard(bool[,] board)
		{
			var sb = new StringBuilder();

			foreach (int row in Enumerable.Range(0, rowCount))
			{
				foreach (int column in Enumerable.Range(0, columnCount))
				{
					var cell = board[row, column];
					sb.Append($"{cell.ToString()} ");
				}
				sb.Append(Environment.NewLine);
			}

			Console.CursorVisible = false;
			Console.SetCursorPosition(0, 0);
			Console.Write(sb.ToString());
			Thread.Sleep(3000);
		}
	}
}
