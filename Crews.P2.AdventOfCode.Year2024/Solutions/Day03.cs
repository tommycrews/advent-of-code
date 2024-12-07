using System.Text.RegularExpressions;

namespace Crews.P2.AdventOfCode.Year2024.Solutions;

public class Day03 : Solution
{
	public override string Name => "Mull It Over";

	public override string ExecutePart1() => GetProducts(InputText)
		.Sum()
		.ToString();

	public override string ExecutePart2() => InputText
		.Split("do()")
		.Select(blocks => blocks.Split("don't()").First())
		.Select(doBlocks => string.Join("", doBlocks))
		.SelectMany(GetProducts)
		.Sum()
		.ToString();

	private static IEnumerable<int> GetProducts(string input) => new Regex(@"mul\((\d+),(\d+)\)")
		.Matches(input)
		.Where(match => match.Groups.Count >= 3)
		.Select(match => int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value));
}
