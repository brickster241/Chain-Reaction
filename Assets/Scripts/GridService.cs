using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridService : MonoBehaviour
{
    [Range(5, 10)]
    [SerializeField] int ROWS = 10;

    [Range(10, 15)]
    [SerializeField] int COLS = 12;

    [SerializeField] GameObject TilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid() {
        Vector3 TilePrefabScale = TilePrefab.transform.localScale;
        Vector3 OffsetTile = CalculateTileOffset();
        for (int i = 0; i < ROWS; i++) {
            for (int j = 0; j < COLS; j++) {
                // CALCULATE OFFSET
                Vector3 TilePosition = new Vector3(j * TilePrefabScale.y, -i * TilePrefabScale.x, 0f);
                GameObject Tile = GameObject.Instantiate(TilePrefab, TilePosition + OffsetTile, Quaternion.identity, transform);
                Tile.name = "(" + i + ", " + j + ")";
            }
        }
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
