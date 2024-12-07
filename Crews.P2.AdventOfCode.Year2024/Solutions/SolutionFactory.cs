using System.Reflection;

namespace Crews.P2.AdventOfCode.Year2024.Solutions;

public static class SolutionFactory
{
	private static readonly Assembly _assembly = Assembly.GetExecutingAssembly();

	public static Solution GetSolution(int day)
		=> GetSolutionInstance(_assembly.GetTypes()
			.SingleOrDefault(type => type.Name == $"Day0{day}" || type.Name == $"Day{day}")
			?? throw new NotImplementedException("The requested solution is not implemented."));

	public static IEnumerable<Solution> GetAllSolutions()
		=> _assembly.GetTypes()
		.Where(type => type.Name.StartsWith("Day"))
		.Select(GetSolutionInstance);

	private static Solution GetSolutionInstance(Type type) => (Solution)Activator.CreateInstance(type)!;
}
