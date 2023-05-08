using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridService : MonoBehaviour
{
    [Range(5, 10)]
    [SerializeField] int ROWS = 10;

    [Range(10, 15)]
    [SerializeField] int COLS = 12;

    [SerializeField] TileService TilePrefab;

    TileService[, ] GridTiles;

    // Start is called before the first frame update
    void Start()
    {
        GridTiles = new TileService[ROWS, COLS];
        GenerateGrid();
        SetTileAttributes();
    }

    private void GenerateGrid() {
        Vector3 TilePrefabScale = TilePrefab.transform.localScale;
        Vector3 OffsetTile = CalculateTileOffset();
        for (int i = 0; i < ROWS; i++) {
            for (int j = 0; j < COLS; j++) {
                // CALCULATE OFFSET
                Vector3 TilePosition = new Vector3(j * TilePrefabScale.y, -i * TilePrefabScale.x, 0f);
                TileService Tile = GameObject.Instantiate<TileService>(TilePrefab, TilePosition + OffsetTile, Quaternion.identity, transform);
                Tile.gameObject.name = "(" + i + ", " + j + ")";
                GridTiles[i, j] = Tile;
            }
        }
    }

    private void SetTileAttributes() {
        for (int i = 0; i < ROWS; i++) {
            for (int j = 0; j < COLS; j++) {
                TileService tile = GetTile(i, j);
                tile.SetTileIndex(i, j);
                tile.SetTileNeighbours(GetTileNeighbours(i, j));
                tile.SetTileType();
            }
        }
    }

    public TileService GetTile(int ROW, int COL) {
        if (0 <= ROW && ROW < ROWS && 0 <= COL && COL < COLS) {
            return GridTiles[ROW, COL];
        } else {
            return null;
        }
    }

    private bool isTileValid(int ROW_INDEX, int COL_INDEX) {
        if (ROW_INDEX >= 0 && ROW_INDEX < ROWS && COL_INDEX >= 0 && COL_INDEX < COLS) {
            return true;
        }
        return false;
    }

    private List<TileService> GetTileNeighbours(int ROW, int COL) {
        TileService tile = GetTile(ROW, COL);
        List<TileService> TileNeighbours = new List<TileService>();
        if (isTileValid(ROW - 1, COL)) {
            TileNeighbours.Add(GetTile(ROW - 1, COL));
        }
        if (isTileValid(ROW, COL - 1)) {
            TileNeighbours.Add(GetTile(ROW, COL - 1));
        }
        if (isTileValid(ROW + 1, COL)) {
            TileNeighbours.Add(GetTile(ROW + 1, COL));
        }
        if (isTileValid(ROW, COL + 1)) {
            TileNeighbours.Add(GetTile(ROW, COL + 1));
        }
        return TileNeighbours;
    }

    private Vector3 CalculateTileOffset() {
        float tileWidth = TilePrefab.transform.localScale.x;
        float tileHeight = TilePrefab.transform.localScale.y;
        float gridHeight = ROWS * tileHeight;
        float gridWidth = COLS * tileWidth;
        Vector3 offset = new Vector3(-gridWidth / 2 + tileWidth / 2, gridHeight / 2 - tileHeight / 2, 0f);
        return offset;
    }
}
