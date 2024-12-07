namespace Crews.P2.AdventOfCode.Year2024.Solutions;

public class Day2 : ISolution
{
	private readonly IEnumerable<IEnumerable<int>> _reports;

	public string Name => "Red-Nosed Reports";
	public int Day => 2;

	public Day2() => _reports = File.ReadAllLines("./InputData/Day2.txt")
		.Select(line => line
			.Split(' ')
			.Select(number => int.Parse(number)));

	public string ExecutePart1() => GetSafeReportCount().ToString();

	public string ExecutePart2() => GetSafeReportCount(allowBadLevel: true).ToString();

	private int GetSafeReportCount(bool allowBadLevel = false) => _reports
		.Select(report => new Record(report.ToArray(), allowBadLevel).Safe)
		.Count(isSafe => isSafe);

	private readonly struct Record(int[] levels, bool allowBadLevel)
	{
		public bool Safe { get; } = RecursivelyCheckSafety(levels, allowBadLevel);

		private static bool RecursivelyCheckSafety(int[] levels, bool allowBadLevel)
		{
			if (CheckSafety(levels)) return true;
			if (!allowBadLevel) return false;

			for (int i = 0; i < levels.Length; i++)
			{
				int[] adjustedLevels = levels.Where((_, index) => index != i).ToArray();
				if (CheckSafety(adjustedLevels)) return true;
			}
			return false;
		}

		private static bool CheckSafety(int[] levels)
		{
			int previousLevel = levels[0];
			int delta = 0;

			for (int i = 1; i < levels.Length; i++)
			{
				int currentLevel = levels[i];
				int difference = Math.Abs(currentLevel - previousLevel);

				if (difference > 3 || difference == 0) return false;
				
				// Increase or decrease delta by 1 by comparing current element to previous element.
				delta += currentLevel.CompareTo(previousLevel);
				previousLevel = currentLevel;
			}
			
			// For sequence to be continually increasing or decreasing, the delta should be equal to the number of "pairs".
			return Math.Abs(delta) == levels.Length - 1;
		}
	}
}