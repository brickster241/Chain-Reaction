using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileService : MonoBehaviour
{   
    [SerializeField] SpriteRenderer tileSprite;
    public TileType tileType;
    private Vector2Int tileIndex;
    public List<TileService> Neighbours;
    [SerializeField] OrbService orbService;

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

    public void SetTileSpriteColor(Color color) {
        tileSprite.color = color;
    }

    public void OnTileClick() {
        orbService.OnOrbClick();
    }

    public OrbService GetOrbService() {
        return orbService;
    }
}
