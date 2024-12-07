using Crews.P2.AdventOfCode.Year2024.Solutions;

if (args.Length == 0)
{
	foreach (Solution solution in SolutionFactory.GetAllSolutions())
	{
		ExecuteSolution(solution);
	}
	return;
}

ExecuteSolution(SolutionFactory.GetSolution(int.Parse(args[0])));

static void ExecuteSolution(Solution solution)
{
	string title = $"Solutions for '{solution.Name}' (day {solution.DayNumber})";
	
	Console.WriteLine();
	Console.WriteLine(title);
	Console.WriteLine(new string('=', title.Length));
	Console.WriteLine($"Part 1: {solution.ExecutePart1()}");
	Console.WriteLine($"Part 2: {solution.ExecutePart2()}");
	Console.WriteLine();
}
