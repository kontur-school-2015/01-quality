private static void Main(string[] args)
{
	var source = File.ReadAllText(args[0]);
	var gameData = JsonConvert.DeserializeObject<GameData>(source);
	Console.WriteLine(GetLog());
}

private string GetLog()
{
	var log = new StringBuilder();
	var score = 0;
	var commands = Commands.Clone() as string;
	var wellContent = new HashSet<Cell>(new CellEqualityComparer());
	var wellAABB = new List<Rectangle>();
	var currentCommandIndex = 0;
	while (commands != null && currentCommandIndex < commands.Length)
	{
		foreach (var piece in Pieces)
		{
			if (currentCommandIndex >= commands.Length)
			{
				break;
			}
			var unit = piece;
			if (IsCollidePossible(unit.AABB, wellAABB))
			{
				if (IsCollide(unit.WorldCoords, wellContent))
				{
					wellContent.Clear();
					wellAABB.Clear();
					score -= 10;
				}
			}
			if (currentCommandIndex != 0)
			{
				if (log.Length != 0)
				{
					log.Append(Environment.NewLine);
				}
				log.Append(currentCommandIndex - 1 + " " + score);
			}
			while (true)
			{

				if (currentCommandIndex >= commands.Length)
				{
					break;
				}

				if (Commands[currentCommandIndex] == 'P')
				{
					if (log.Length != 0)
					{
						log.Append(Environment.NewLine);
					}
					log.Append(GetField(wellContent, unit));
					currentCommandIndex++;
					continue;
				}

				var testUnit =
					unit.ApplyTransform(CommandToMovement(commands[currentCommandIndex]));

				if (IsCollidePossible(testUnit.AABB, wellAABB))
				{
					if (IsCollide(testUnit.WorldCoords, wellContent))
					{
						int incomeScore;
						wellContent.UnionWith(unit.WorldCoords);
						wellAABB.Add(unit.AABB);
						WellAgregate(wellContent, out incomeScore);
						score += incomeScore;
						currentCommandIndex++;
						break;
					}
				}
				else if (IsAABBOutOfBount(testUnit))
				{
					int incomeScore;
					wellContent.UnionWith(unit.WorldCoords);
					wellAABB.Add(unit.AABB);
					WellAgregate(wellContent, out incomeScore);
					score += incomeScore;
					currentCommandIndex++;
					break;
				}
				unit = testUnit;
				currentCommandIndex++;
			}
		}
	}
	return log.ToString();
}
