using System.Collections;
using System.Collections.Generic;

public class TileChecker
{
    public bool TileJsTouchingHive(int tileColumn, int tileRow, int hiveRow, int hiveColumn) 
    {
        switch (hiveColumn % 2)
        {
            case (0):
                return (
                    tileColumn == hiveColumn && tileRow == hiveRow - 1 ||
                    tileColumn == hiveColumn && tileRow == hiveRow + 1 ||
                    tileColumn == hiveColumn - 1 && tileRow == hiveRow - 1 ||
                    tileColumn == hiveColumn + 1 && tileRow == hiveRow - 1 ||
                    tileColumn == hiveColumn - 1 && tileRow == hiveRow ||
                    tileColumn == hiveColumn + 1 && tileRow == hiveRow
                );
            default:
                return (
                    tileColumn == hiveColumn && tileRow == hiveRow - 1 ||
                    tileColumn == hiveColumn && tileRow == hiveRow + 1 ||
                    tileColumn == hiveColumn - 1 && tileRow == hiveRow - 1 ||
                    tileColumn == hiveColumn + 1 && tileRow == hiveRow - 1 ||
                    tileColumn == hiveColumn - 1 && tileRow == hiveRow ||
                    tileColumn == hiveColumn + 1 && tileRow == hiveRow
                );
        }
    }

    public List<Coordinate> GetAdjacentTiles(int hiveColumn) 
    {
        List<Coordinate> coordinates = new List<Coordinate>();
        coordinates.Add(new Coordinate(0,-1));
        coordinates.Add(new Coordinate(0, 1));
        coordinates.Add(new Coordinate(-1, -1));
        coordinates.Add(new Coordinate(1, -1));
        coordinates.Add(new Coordinate(-1, 0));
        coordinates.Add(new Coordinate(1, 0));
        return coordinates;
    }

}

public class Coordinate 
{
    private int column;
    private int row;

    public Coordinate(int column, int row)
   {
      this.column = column;
      this.row  = row;
   }

}
