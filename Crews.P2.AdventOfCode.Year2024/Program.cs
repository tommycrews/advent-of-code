using Crews.P2.AdventOfCode.Year2024.Solutions;

if (args.Length == 0)
{
	foreach (Solution solution in SolutionFactory.GetAllSolutions())
	{
		solution.ExecuteAndPrint();
	}
	return;
}
SolutionFactory.GetSolution(int.Parse(args[0])).ExecuteAndPrint();
