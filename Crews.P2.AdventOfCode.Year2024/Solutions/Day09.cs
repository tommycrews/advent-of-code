namespace Crews.P2.AdventOfCode.Year2024.Solutions;

public class Day09 : Solution
{
  public override string Name => "Disk Fragmenter";

  public override string ExecutePart1() => GetChecksum(ExpandDrive(InputText)).ToString();

  public override string ExecutePart2() => GetChecksumPart2(ExpandDrive(InputText)).ToString();

  private static List<File> ExpandDrive(string data)
  {
    List<File> files = new();
    for (int index = 0; index < data.Length; index++)
    {
      files.Add(
        new()
        {
          ID = index % 2 == 0 ? index / 2 : -1,
          Length = (int)char.GetNumericValue(data[index]),
        }
      );
    }
    return files;
  }

  private static long GetChecksum(List<File> files)
  {
    int blocksLength = files.Count;
    int pointerIndex = blocksLength - 1;
    long checksum = 0;

    int evaluated = 0;
    // Don't actually rearrange the blocks. Just calculete in-place.
    for (int index = 0; index < blocksLength; index++)
    {
      if (index > pointerIndex)
        break;

      if (files[index].IsFreeSpace)
      {
        while (files[pointerIndex].IsFreeSpace)
          pointerIndex--;
        if (index > pointerIndex)
          break;

        checksum += index * files[pointerIndex].ID;
        pointerIndex--;
      }
      else
        checksum += index * files[index].ID;

      evaluated++;
      if (evaluated >= blocksLength)
        break;
    }
    return checksum;
  }

  private static long GetChecksumPart2(List<File> files)
  {
    Console.WriteLine(
      string.Join(", ", files.Take(100).Select(file => $"{file.ID} ({file.Length})"))
    );

    Console.WriteLine();

    Console.WriteLine(
      string.Join(", ", files.Skip(19900).Select(file => $"{file.ID} ({file.Length})"))
    );

    Console.WriteLine();

    for (int reverseIndex = files.Count - 1; reverseIndex >= 0; reverseIndex--)
    {
      File reverseFile = files[reverseIndex];
      if (reverseFile.IsFreeSpace)
        continue;

      for (int index = 0; index < reverseIndex; index++)
      {
        File searchFile = files[index];
        if (!searchFile.IsFreeSpace || searchFile.Length < reverseFile.Length)
          continue;

        int hangingLength = searchFile.Length - reverseFile.Length;

        files[index] = reverseFile;
        files[reverseIndex] = new() { ID = -1, Length = reverseFile.Length };

        if (hangingLength > 0)
        {
          files.Insert(index + 1, new() { ID = -1, Length = hangingLength });
          reverseIndex++;
        }
        break;
      }
    }

    Console.WriteLine(
      string.Join(", ", files.Take(100).Select(file => $"{file.ID} ({file.Length})"))
    );

    return CalculateChecksum(files);
  }

  private static long CalculateChecksum(List<File> files)
  {
    int pointerIndex = 0;
    long checksum = 0;

    for (int index = 0; index < files.Count; index++)
    {
      File file = files[index];
      if (file.IsFreeSpace)
      {
        pointerIndex += file.Length;
        continue;
      }

      for (int fileIndex = 0; fileIndex < file.Length; fileIndex++)
      {
        checksum += (fileIndex + pointerIndex) * file.ID;
      }
      pointerIndex += file.Length;
    }

    return checksum;
  }

  struct File
  {
    public int ID { get; init; }
    public int Length { get; init; }
    public bool IsFreeSpace => ID == -1;
  }
}
