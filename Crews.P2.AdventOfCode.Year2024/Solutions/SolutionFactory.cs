using System.Reflection;

namespace Crews.P2.AdventOfCode.Year2024.Solutions;

public static class SolutionFactory
{
	private static readonly Assembly _assembly = Assembly.GetExecutingAssembly();

	public static ISolution GetSolution(int day)
		=> GetSolutionInstance(_assembly.GetTypes().SingleOrDefault(type => type.Name == $"Day{day}")
			?? throw new NotImplementedException("The requested solution is not implemented."));

	public static IEnumerable<ISolution> GetAllSolutions()
		=> _assembly.GetTypes()
		.Where(type => type.Name.StartsWith("Day"))
		.Select(GetSolutionInstance);

	private static ISolution GetSolutionInstance(Type type) => (ISolution)Activator.CreateInstance(type)!;
}
