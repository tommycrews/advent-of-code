namespace Crews.P2.AdventOfCode.Year2024.Solutions;

public class Day7 : ISolution
{
	private readonly Dictionary<long, int[]> _equations = File.ReadAllLines("./InputData/Day7.txt")
		.Select(line => line.Split(":", StringSplitOptions.TrimEntries))
		.Select(splitLine => new KeyValuePair<long, int[]>(long.Parse(splitLine[0]), splitLine[1]
			.Split(' ')
			.Select(operand => int.Parse(operand))
			.ToArray()))
		.ToDictionary();

	public string Name => "Bridge Repair";
	public int Day => 7;

	public string ExecutePart1() => GetTotal().ToString();
	public string ExecutePart2() => GetTotal(useConcatenation: true).ToString();

	public long GetTotal(bool useConcatenation = false) => _equations
		.Where(equation => RecursivelyGetPermutations(equation.Value, useConcatenation).Contains(equation.Key))
		.Select(equation => equation.Key)
		.Sum();

	private static List<long> RecursivelyGetPermutations(int[] operands, bool useConcatenation)
	{
		List<long> results = [];
		List<long> leftOperandPermutations = [operands[0]];

		for (int i = 1; i < operands.Length; i++)
		{
			List<long> newPermutations = [];
			for (int j = 0; j < leftOperandPermutations.Count; j++)
			{
				newPermutations.AddRange(useConcatenation
					? GetComplexPermutations(leftOperandPermutations[j], operands[i])
					: GetPermutations(leftOperandPermutations[j], operands[i]));
			}
			leftOperandPermutations = newPermutations;
			if (i == operands.Length - 1)
			{
				results.AddRange(newPermutations);
			}
		}
		return results;
	}

	private static List<long> GetPermutations(long a, long b) => [a * b, a + b];
	private static List<long> GetComplexPermutations(long a, long b) => [.. GetPermutations(a, b), long.Parse($"{a}{b}")];
}
