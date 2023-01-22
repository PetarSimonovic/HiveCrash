using System.Collections;
using System.Collections.Generic;

public class TileChecker
{
    public bool TileJsTouchingHive(int tileColumn, int tileRow, int hiveRow, int hiveColumn) 
    {
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
