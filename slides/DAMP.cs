public Field Fix(Figure figure, Cell point, out int points)
{
	List<Figure> pieces = DivideFigureByRowsAndConnectedComponents(figure, point);
	pieces.Sort((x, y) => x.BoundingBox.X - y.BoundingBox.X);
	List<Tuple<int, int>> indexes = new List<Tuple<int, int>>() 
					{ Tuple.Create(0, pieces[0].BoundingBox.X / optimalColumnWidth) };
	for (int i = 1; i < pieces.Count; ++i)
	{
		int verticalAreaIndex = pieces[i].BoundingBox.X / optimalColumnWidth;
		if (indexes[indexes.Count - 1].Item2 < verticalAreaIndex)
			indexes.Add(Tuple.Create(i, verticalAreaIndex));
	}
	indexes.Add(Tuple.Create(pieces.Count, indexes[indexes.Count - 1].Item2));

	IVerticalArea[] verticalAreas = new IVerticalArea[columns.Length];
	int currentAreaIndex;
	int currentIndex = 0;
	for (currentAreaIndex = 0; currentAreaIndex < columns.Length; ++currentAreaIndex)
		if (indexes[currentIndex].Item2 == currentAreaIndex)
		{
			verticalAreas[currentAreaIndex] = columns[currentAreaIndex]
				.Fix(pieces
				.Skip(indexes[currentIndex].Item1)
				.Take(indexes[currentIndex + 1].Item1 - indexes[currentIndex].Item1)
				.ToList(),
				point);
			++currentIndex;
		}
		else
			verticalAreas[currentAreaIndex] = columns[currentAreaIndex];
	List<int> filledRows = new List<int>();
	List<int> changed = GetChangedRows(figure, point);
	for (int i = 0; i < changed.Count; ++i)
		if (verticalAreas.All(area => area[changed[i]].IsFilled()))
			filledRows.Add(changed[i]);

	points = filledRows.Count;
	return new Field(width, height, optimalColumnWidth,
		verticalAreas
		.Select(area => area.DeleteFilledRowsAndResize(filledRows))
		.ToImmutableArray<IVerticalArea>());
}
