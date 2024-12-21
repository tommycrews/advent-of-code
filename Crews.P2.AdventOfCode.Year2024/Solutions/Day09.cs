namespace Crews.P2.AdventOfCode.Year2024.Solutions;

public class Day09 : Solution
{
	public override string Name => "Disk Fragmenter";

	public override string ExecutePart1() => GetChecksum(ExpandDrive(InputText)).ToString();

	public override string ExecutePart2() => "Not implemented";

	private static List<int> ExpandDrive(string data)
	{
		List<int> blocks = [];
		for (int index = 0; index < data.Length; index++)
		{
			for (int blockValue = 0; blockValue < (int)char.GetNumericValue(data[index]); blockValue++)
			{
				blocks.Add(index % 2 == 0 ? index / 2 : -1);
			}
		}
		return blocks;
	}

	private static long GetChecksum(IEnumerable<int> blocks)
	{
		int blocksLength = blocks.Count();
		int pointerIndex = blocksLength - 1;
		long checksum = 0;

		int evaluated = 0;
		// Don't actually rearrange the blocks. Just calculete in-place.
		for (int index = 0; index < blocksLength; index++)
		{
			if (index > pointerIndex) break;

			if (blocks.ElementAt(index) == -1)
			{
				while (blocks.ElementAt(pointerIndex) == -1) pointerIndex--;
				if (index > pointerIndex) break;

				checksum += index * blocks.ElementAt(pointerIndex);
				pointerIndex--;
			}
			else checksum += index * blocks.ElementAt(index);

			evaluated++;
			if (evaluated >= blocksLength) break;
		}
		return checksum;
	}

	private static long GetChecksumPart2(IEnumerable<int> data)
	{
		(int, int) block = (data.ElementAt(0), 1);
		List<(int, int)> blocks = [];

		for (int i = 0; i < data.Count(); i++)
		{
			int currentValue = data.ElementAt(i);
			if (blocks.Last().Item1 == currentValue)
			{
				block = (block.Item1, block.Item2 + 1);
			}
			else
			{
				blocks.Add(block);
				block = (data.ElementAt(i), 1);
			}
		}

		int checksum = 0;
		IEnumerable<(int, int)> fileBlocks = blocks.Where(block => block.Item1 != -1);
		int reverseIndex = blocks.Count - 1;
		int pointerIndex = 0;

		for (int index = 0; index < blocks.Count; index++)
		{
			block = blocks[index];
			if (index > reverseIndex) break;

			if (block.Item1 == -1)
			{
				for (int i = 0; i < blocks[reverseIndex].Item2; i++)
				{
					checksum += blocks[reverseIndex].Item1;
				}
				pointerIndex += block.Item2;
			}
			else
			{
				for (int i = 0; i < block.Item2; i++)
				{
					checksum += block.Item1 * pointerIndex;
					pointerIndex++;
				}
			}
			pointerIndex += blocks[index].Item2;
		}
	}
}
