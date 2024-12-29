namespace Crews.P2.AdventOfCode.Year2024.Solutions;

public class Day05 : Solution
{
  public override string Name => "Print Queue";

  public override string ExecutePart1() => MedianSum().ToString();

  public override string ExecutePart2() => InvalidMedianSum().ToString();

  private int MedianSum()
  {
    IEnumerable<Rule> rules = GetRules();
    IEnumerable<List<int>> updates = GetUpdates();

    IEnumerable<List<int>> validUpdates = updates.Where(update =>
      rules.All(rule => rule.Validate(update))
    );

    return validUpdates.Select(update => update[update.Count / 2]).Sum();
  }

  private int InvalidMedianSum()
  {
    IEnumerable<Rule> rules = GetRules();

    List<List<int>> invalidUpdates = GetUpdates()
      .Where(update => rules.Any(rule => !rule.Validate(update)))
      .ToList();

    int invalidCount = invalidUpdates.Count;

    while (invalidCount > 0)
    {
      foreach (List<int> update in invalidUpdates)
      {
        foreach (Rule rule in rules)
        {
          if (!rule.Validate(update))
          {
            update.Remove(rule.After);
            update.Add(rule.After);
          }
        }
      }

      invalidCount = invalidUpdates
        .Where(update => rules.Any(rule => !rule.Validate(update)))
        .Count();
    }

    return invalidUpdates.Select(update => update[update.Count / 2]).Sum();
  }

  private IEnumerable<Rule> GetRules() =>
    InputLines
      .Where(line => line.Contains('|'))
      .Select(line => line.Split('|'))
      .Select(values => new Rule { Before = int.Parse(values[0]), After = int.Parse(values[1]) });

  private IEnumerable<List<int>> GetUpdates() =>
    InputLines
      .Where(line => line.Contains(','))
      .Select(line => line.Split(',').Select(value => int.Parse(value)).ToList());

  struct Rule
  {
    public int Before { get; set; }
    public int After { get; set; }

    public bool Validate(List<int> values) =>
      Applies(values) ? values.IndexOf(Before) < values.IndexOf(After) : true;

    public bool Applies(IEnumerable<int> values) =>
      values.Contains(Before) && values.Contains(After);
  }
}
