namespace Crews.P2.AdventOfCode.Year2024.Solutions;

public class Day06 : Solution
{
  private readonly Map _map;

  public override string Name => "Guard Gallivant";

  public override string ExecutePart1() => "Not implemented";

  public override string ExecutePart2() => "Not implemented";

  public Day06()
  {
    _map = BuildMap();
  }

  private Map BuildMap()
  {
    List<Point> obstacles = new();
    string[] data = InputLines;

    for (int y = 0; y < data.Length; y++)
    {
      string row = data[y];
      for (int x = 0; x < row.Length; x++)
      {
        if (row[x] == '#')
          obstacles.Add(new() { X = x, Y = y });
      }
    }

    return new()
    {
      Size = new() { X = data[0].Length, Y = data.Length },
      Obstacles = obstacles.ToArray(),
    };
  }

  private int DistanceToObstacle(Point from, Direction direction)
  {
    switch (direction)
    {
      case Direction.Up:
        return from.Y
          - _map.Obstacles.Where(obstacle => obstacle.X == from.X && obstacle.Y < from.Y)
            .Select(obstacle => obstacle.Y)
            .Max();
      case Direction.Right:
        return _map.Obstacles.Where(obstacle => obstacle.Y == from.Y && obstacle.X > from.X)
            .Select(obstacle => obstacle.X)
            .Min() - from.X;
      case Direction.Down:
        return _map.Obstacles.Where(obstacle => obstacle.X == from.X && obstacle.Y > from.Y)
            .Select(obstacle => obstacle.Y)
            .Min() - from.Y;
      case Direction.Left:
        return from.X
          - _map.Obstacles.Where(obstacle => obstacle.Y == from.Y && obstacle.X < from.X)
            .Select(obstacle => obstacle.X)
            .Max();
      default:
        return -1;
    }
  }

  private struct Point
  {
    public int X { get; set; }
    public int Y { get; set; }
  }

  private struct Map
  {
    public Point Size { get; set; }
    public Point[] Obstacles { get; set; }
  }

  private enum Direction
  {
    Up,
    Right,
    Down,
    Left,
  }
}
