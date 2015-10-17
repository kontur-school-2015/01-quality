public Field MoveLeft(Figure current, PointComparer comparer)
{
	if (current.MinX + this.Offset.X == 0)
		return new Field(this.Width, this.Height,
						 ImmutableHashSet.CreateRange<Point>(this.OccupiedCells.Union(current.Cells
										   .Select(cell => new Point(cell.X + this.Offset.X,
																	 cell.Y + this.Offset.Y)))),
						 this.Points, new Point(-1, -1));
	if (current.Cells.Intersect(this.OccupiedCells.Select(x => new Point(x.X + 1, x.Y)), comparer).Any())
				return new Field(this.Width, this.Height,
								 ImmutableHashSet.CreateRange<Point>(this.OccupiedCells.Union(current.Cells
												   .Select(cell => new Point(cell.X + this.Offset.X,
																			 cell.Y + this.Offset.Y)))),
								 this.Points, new Point(-1, -1));
	return new Field(this.Width, this.Height, this.OccupiedCells, 
					 this.Points, new Point(this.Offset.X - 1, this.Offset.Y));
}

public Field MoveRight(Figure current, PointComparer comparer)
{
	if (current.MinX + current.Width + this.Offset.X == this.Width)
		return new Field(this.Width, this.Height,
						 ImmutableHashSet.CreateRange<Point>(this.OccupiedCells.Union(current.Cells
										   .Select(cell => new Point(cell.X + this.Offset.X,
																	 cell.Y + this.Offset.Y)))),
						 this.Points, new Point(-1, -1));
	if (current.Cells.Intersect(this.OccupiedCells.Select(x => new Point(x.X - 1, x.Y)), comparer).Any())
				return new Field(this.Width, this.Height,
								 ImmutableHashSet.CreateRange<Point>(this.OccupiedCells.Union(current.Cells
												   .Select(cell => new Point(cell.X + this.Offset.X,
																			 cell.Y + this.Offset.Y)))),
								 this.Points, new Point(-1, -1));
	return new Field(this.Width, this.Height, this.OccupiedCells, 
					 this.Points, new Point(this.Offset.X + 1, this.Offset.Y));
}

public Field MoveDown(Figure current, PointComparer comparer)
{
	if (current.MinY + current.Height + this.Offset.Y == this.Height)
		return new Field(this.Width, this.Height,
						 ImmutableHashSet.CreateRange<Point>(this.OccupiedCells.Union(current.Cells
										   .Select(cell => new Point(cell.X + this.Offset.X,
																	 cell.Y + this.Offset.Y)))),
						 this.Points, new Point(-1, -1));
	if (current.Cells.Intersect(this.OccupiedCells.Select(x => new Point(x.X, x.Y-1)), comparer).Any())
				return new Field(this.Width, this.Height,
								 ImmutableHashSet.CreateRange<Point>(this.OccupiedCells.Union(current.Cells
												   .Select(cell => new Point(cell.X + this.Offset.X,
																			 cell.Y + this.Offset.Y)))),
								 this.Points, new Point(-1, -1));
	return new Field(this.Width, this.Height, this.OccupiedCells, 
					 this.Points, new Point(this.Offset.X, this.Offset.Y + 1));
}
