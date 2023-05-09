using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridService : GenericMonoSingleton<GridService>
{
    [Range(5, 10)]
    [SerializeField] int ROWS = 10;

    [Range(10, 15)]
    [SerializeField] int COLS = 12;

    [SerializeField] TileService TilePrefab;
    [SerializeField] Color TileDefaultColor;
    [SerializeField] Color TileHoverColor;

    TileService[, ] GridTiles;
    PlayerType currentPlayerType;
    Vector2Int hoverIndex;
    bool isChainReaction;

    // Start is called before the first frame update
    void Start()
    {
        isChainReaction = false;
        hoverIndex = new Vector2Int(-1, -1);
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

    public void UpdateGridOutlineColor(PlayerType playerType) {
        currentPlayerType = playerType;
        PlayerScriptableObject playerConfig = PlayerManager.Instance.GetPlayerConfig(playerType);
        for (int i = 0; i < ROWS; i++) {
            for (int j = 0; j < COLS; j++) {
                if (playerConfig != null) {
                    GridTiles[i, j].SetTileOutlineColor(playerConfig.PlayerGridColor);
                } else {
                    GridTiles[i, j].SetTileOutlineColor(Color.white);
                }
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

    private void Update() {
        if (!UIService.Instance.isUIVisible) {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            UpdateTileColors(mousePosition);
            if (Input.GetMouseButtonDown(0)) {
                UpdateOnTileClick(mousePosition);
            }
        }
    }

    public void UpdateOnTileClick(Vector3 mousePosition) {
        Vector3 mouseOffsetPosition = mousePosition - CalculateTileOffset();
        float row = (-mouseOffsetPosition.y + TilePrefab.transform.localScale.y * 0.5f) / TilePrefab.transform.localScale.y;
        float col = (mouseOffsetPosition.x + TilePrefab.transform.localScale.x * 0.5f) / TilePrefab.transform.localScale.x;
        Vector2Int tileIndex = new Vector2Int((int)row, (int)col);
        if (isTileValid(tileIndex.x, tileIndex.y) && !isChainReaction) {
            PlayerType playerType = GridTiles[tileIndex.x, tileIndex.y].GetPlayerType();
            if (playerType == PlayerType.NONE || playerType == currentPlayerType) {
                TileService tileService = GridTiles[tileIndex.x, tileIndex.y];
                tileService.SetOrbPlayerType(currentPlayerType);
                bool isTileUnstable = (tileService.GetOrbStatus() == OrbStatus.UNSTABLE);
                tileService.OnTileClick();
                if (!isTileUnstable) 
                    PlayerManager.Instance.UpdateTurn();
            }
        }
    }

    public void UpdateTileColors(Vector3 mousePosition) {
        Vector3 mouseOffsetPosition = mousePosition - CalculateTileOffset();
        float row = (-mouseOffsetPosition.y + TilePrefab.transform.localScale.y * 0.5f) / TilePrefab.transform.localScale.y;
        float col = (mouseOffsetPosition.x + TilePrefab.transform.localScale.x * 0.5f) / TilePrefab.transform.localScale.x;
        Vector2Int newhoverIndex = new Vector2Int((int)row, (int)col);
        if (newhoverIndex == hoverIndex)
            return;
        if (isTileValid(hoverIndex.x, hoverIndex.y)) {
            GridTiles[hoverIndex.x, hoverIndex.y].SetTileSpriteColor(TileDefaultColor);
        }
        if (isTileValid(newhoverIndex.x, newhoverIndex.y)) {
            GridTiles[newhoverIndex.x, newhoverIndex.y].SetTileSpriteColor(TileHoverColor);
            hoverIndex = newhoverIndex;
        } else {
            hoverIndex.x = -1;
            hoverIndex.y = -1;
        }
    }

    public void InvokeChainReaction(TileService tile) {
        StartCoroutine(StartChainReaction(tile));
    }

    private IEnumerator StartChainReaction(TileService tile) {
        if (!isChainReaction) {
            Queue<List<TileService>> tiles = new Queue<List<TileService>>();
            PlayerType playerType = tile.GetPlayerType();
            isChainReaction = true;
            tiles.Enqueue(new List<TileService>() {tile});
            while (tiles.Count > 0) {
                List<TileService> frontTiles = tiles.Dequeue();
                List<TileService> nextTiles = new List<TileService>();
                yield return new WaitForSeconds(0.5f);
                for (int i = 0; i < frontTiles.Count; i++) {
                    TileService frontTile = frontTiles[i];
                    frontTile.GetOrbService().DisableOrb();
                    for (int j = 0; j < frontTile.Neighbours.Count; j++) {
                        TileService neighbourTile = frontTile.Neighbours[j];
                        TileType neighbourTileType = neighbourTile.tileType;
                        OrbStatus neighbourOrbStatus = neighbourTile.GetOrbService().GetOrbStatus();
                        if (neighbourOrbStatus == OrbStatus.UNSTABLE) {
                            nextTiles.Add(neighbourTile);
                        }
                        neighbourTile.SetOrbPlayerType(playerType);
                        neighbourTile.OnTileClick();
                    }
                }
                if (nextTiles.Count != 0)
                    tiles.Enqueue(nextTiles);
                
            }
            isChainReaction = false;
            PlayerManager.Instance.UpdateTurn();
        }
    }

    public Dictionary<PlayerType, int> GetPlayerActiveTileCount(PlayerScriptableObjectList PlayerConfigs, int playerCount) {
        Dictionary<PlayerType, int> playerTileCount = new Dictionary<PlayerType, int>();
        for (int i = 0; i < playerCount; i++) {
            playerTileCount[PlayerConfigs.playerConfigs[i].playerType] = 0;
        }
        for (int i = 0; i < ROWS; i++) {
            for (int j = 0; j < COLS; j++) {
                TileService tile = GridTiles[i, j];
                PlayerType playerType = tile.GetPlayerType();
                if (playerType != PlayerType.NONE) {
                    playerTileCount[playerType] += 1;
                }
            }
        }
        return playerTileCount;
    }
}
