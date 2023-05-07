using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileService : MonoBehaviour
{   
    public TileType tileType;
    private Vector2Int tileIndex;
    public List<TileService> Neighbours;
    private TileSM tileSM;

    private void Awake() {
        tileSM = new TileSM();
        tileSM.SetTileService(this);
        tileSM.SwitchState(TileState.EMPTY);
    }
    
    public void SetTileNeighbours(List<TileService> TileNeighbours) {
        Neighbours = TileNeighbours;
    }

    public void SetTileIndex(int row, int col) {
        tileIndex.x = row;
        tileIndex.y = col;
    }

    public void SetTileType() {
        if (Neighbours.Count <= 2) {
            tileType = TileType.CORNER;
        } else if (Neighbours.Count == 3) {
            tileType = TileType.EDGE;
        } else {
            tileType = TileType.MIDDLE;
        }
    }

}
