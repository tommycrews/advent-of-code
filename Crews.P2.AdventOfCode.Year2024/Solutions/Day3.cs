using System.Text.RegularExpressions;

namespace Crews.P2.AdventOfCode.Year2024.Solutions;

public class Day3 : ISolution
{
	private readonly string _data;

	public string Name => "Mull It Over";
	public int Day => 3;

	public Day3() => _data = File.ReadAllText("./InputData/Day3.txt");

	public string ExecutePart1() => GetProducts(_data)
		.Sum()
		.ToString();

	public string ExecutePart2() => _data
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
