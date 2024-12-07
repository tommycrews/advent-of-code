namespace Crews.P2.AdventOfCode.Year2024.Solutions;

public class Day4 : ISolution
{
	private readonly string[] _lines = File.ReadAllLines("./InputData/Day4.txt");

	public string Name => "Ceres Search";
	public int Day => 4;

	public string ExecutePart1() => new string[] { "XMAS", "SAMX" }
		.Select(s => GetHorizontalCount(s)
			+ GetVerticalCount(_lines, s)
			+ GetForwardDiagonalCount(_lines, s)
			+ GetBackwardDiagonalCount(_lines, s))
		.Sum()
		.ToString();

	public string ExecutePart2() => GetXMasCount(_lines).ToString();

	private int GetHorizontalCount(string substring) => _lines
		.Select(line => line.Split(substring).Length - 1)
		.Sum();

	private static int GetVerticalCount(string[] lines, string substring)
		=> GetMultilineCount(lines, substring, 0, lines[0].Length, (i, x, y) => lines[y + i][x] != substring[y]);

	private static int GetForwardDiagonalCount(string[] lines, string substring)
		=> GetDiagonalCount(lines, substring, substring.Length - 1, (i, x, y) => lines[y + i][x - y] != substring[y]);

	private static int GetBackwardDiagonalCount(string[] lines, string substring)
		=> GetDiagonalCount(lines, substring, 0, (i, x, y) => lines[y + i][x + y] != substring[y]);

	private static int GetDiagonalCount(string[] lines, string substring, int xMin, Func<int, int, int, bool> breakFunc)
		=> GetMultilineCount(lines, substring, xMin, lines[0].Length - substring.Length + 1, breakFunc);

	private static int GetMultilineCount(
		string[] lines, string substring, int xMin, int xMax, Func<int, int, int, bool> breakFunc)
	{
		int count = 0;
		for (int i = 0; i <= lines.Length - substring.Length; i++)
		{
			for (int x = xMin; x < xMax; x++)
			{
				bool match = true;
				for (int y = 0; y < substring.Length; y++)
				{
					if (breakFunc(i, x, y))
					{
						match = false;
						break;
					}
				}
				if (match) count++;
			}
		}
		return count;
	}

	private static int GetXMasCount(string[] lines)
	{
		int count = 0;
		for (int y = 1; y < lines.Length - 1; y++)
		{
			for (int x = 1; x < lines[0].Length - 1; x++)
			{
				if (lines[y][x] == 'A')
				{
					// Don't judge my methods; it's late.
					char topLeft = lines[y - 1][x - 1];
					char topRight = lines[y - 1][x + 1];
					char bottomLeft = lines[y + 1][x - 1];
					char bottomRight = lines[y + 1][x + 1];

					bool validBackward = "MS".Contains(topLeft) && "MS".Contains(bottomRight) && topLeft != bottomRight;
					bool validForward = "MS".Contains(topRight) && "MS".Contains(bottomLeft) && topRight != bottomLeft;

					if (validForward && validBackward) count++;
				}
			}
		}
		return count;
	}
}
