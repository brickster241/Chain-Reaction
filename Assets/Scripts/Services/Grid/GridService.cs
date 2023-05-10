using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers;
using Enums;
using Generics;
using Scriptables;

namespace Services {
    /*
        GridService MonoSingleton Class. Handles all the Grid Operations, References to TileControllers & OrbControllers.
        Handles all the Grid Visual aspects. Interacts with Services to Update Turns.
    */
    public class GridService : GenericMonoSingleton<GridService>
    {
        [Range(5, 10)]
        [SerializeField] int ROWS = 10;

        [Range(10, 15)]
        [SerializeField] int COLS = 12;

        [SerializeField] TileController TilePrefab;
        [SerializeField] Color TileDefaultColor;
        [SerializeField] Color TileHoverColor;

        TileController[, ] GridTiles;
        PlayerType currentPlayerType;
        Vector2Int hoverIndex;
        bool isChainReactionRunning;

        // Start is called before the first frame update
        void Start()
        {
            isChainReactionRunning = false;
            hoverIndex = new Vector2Int(-1, -1);
            GridTiles = new TileController[ROWS, COLS];
            GenerateGrid();
            SetTileAttributes();
        }

        /*
            Generates the Grid based on ROWS, COLS & TilePrefab. Dynamically calculates Offset.
        */
        private void GenerateGrid() {
            Vector3 TilePrefabScale = TilePrefab.transform.localScale;
            Vector3 OffsetTile = CalculateTileOffset();
            for (int i = 0; i < ROWS; i++) {
                for (int j = 0; j < COLS; j++) {
                    // CALCULATE OFFSET
                    Vector3 TilePosition = new Vector3(j * TilePrefabScale.y, -i * TilePrefabScale.x, 0f);
                    TileController Tile = GameObject.Instantiate<TileController>(TilePrefab, TilePosition + OffsetTile, Quaternion.identity, transform);
                    Tile.gameObject.name = "(" + i + ", " + j + ")";
                    GridTiles[i, j] = Tile;
                }
            }
        }

        /*
            Sets Initial Tile Attributes including Index, Neighbours, TileType.
        */
        private void SetTileAttributes() {
            for (int i = 0; i < ROWS; i++) {
                for (int j = 0; j < COLS; j++) {
                    TileController tile = GetTile(i, j);
                    tile.SetTileIndex(i, j);
                    tile.SetTileNeighbours(GetTileNeighbours(i, j));
                    tile.SetTileType();
                }
            }
        }

        /*
            Fetches the TileController reference at a specific ROW, COL INDEX.
        */
        public TileController GetTile(int ROW, int COL) {
            if (0 <= ROW && ROW < ROWS && 0 <= COL && COL < COLS) {
                return GridTiles[ROW, COL];
            } else {
                return null;
            }
        }

        /*
            Returns true if the TileIndex is Valid.
        */
        private bool isTileValid(int ROW_INDEX, int COL_INDEX) {
            if (ROW_INDEX >= 0 && ROW_INDEX < ROWS && COL_INDEX >= 0 && COL_INDEX < COLS) {
                return true;
            }
            return false;
        }

        /*
            Gets Neighbouring Tiles respective to a Tile.
        */
        private List<TileController> GetTileNeighbours(int ROW, int COL) {
            TileController tile = GetTile(ROW, COL);
            List<TileController> TileNeighbours = new List<TileController>();
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

        /*
            Updates GridOutline Color based on Current Player Turn.
        */
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

        /*
            Dynamically calculate TileOffset for Grid Positioning.
        */
        private Vector3 CalculateTileOffset() {
            float tileWidth = TilePrefab.transform.localScale.x;
            float tileHeight = TilePrefab.transform.localScale.y;
            float gridHeight = ROWS * tileHeight;
            float gridWidth = COLS * tileWidth;
            Vector3 offset = new Vector3(-gridWidth / 2 + tileWidth / 2, gridHeight / 2 - tileHeight / 2, 0f);
            return offset;
        }

        /*
            Updates Tile Colors based on Hover & Keeps track of Clicks.
        */
        private void Update() {
            if (!UIService.Instance.isUIVisible) {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                UpdateTileColorOnHover(mousePosition);
                if (Input.GetMouseButtonDown(0)) {
                    UpdateOnTileClick(mousePosition);
                }
            }
        }

        /*
            UpdateOnTileClick Method. Gets Called when Mouse Button is Pressed on Tile.
            Also checks if the TileClick is valid or not, if valid then it calls the onTileClick Method.
            Also Updates the Turn if the Tile is not Unstable.
        */
        public void UpdateOnTileClick(Vector3 mousePosition) {
            Vector3 mouseOffsetPosition = mousePosition - CalculateTileOffset();
            float row = (-mouseOffsetPosition.y + TilePrefab.transform.localScale.y * 0.5f) / TilePrefab.transform.localScale.y;
            float col = (mouseOffsetPosition.x + TilePrefab.transform.localScale.x * 0.5f) / TilePrefab.transform.localScale.x;
            Vector2Int tileIndex = new Vector2Int((int)row, (int)col);
            if (isTileValid(tileIndex.x, tileIndex.y) && !isChainReactionRunning) {
                PlayerType playerType = GridTiles[tileIndex.x, tileIndex.y].GetPlayerType();
                if (playerType == PlayerType.NONE || playerType == currentPlayerType) {
                    TileController tileController = GridTiles[tileIndex.x, tileIndex.y];
                    tileController.SetOrbPlayerType(currentPlayerType);
                    bool isTileUnstable = (tileController.GetOrbStatus() == OrbStatus.UNSTABLE);
                    tileController.OnTileClick();
                    if (!isTileUnstable) 
                        PlayerManager.Instance.UpdateTurn();
                }
            }
        }

        /*
            UpdateTileColorOnHover Method. Highlights the Tile based on Mouse Position.
        */
        private void UpdateTileColorOnHover(Vector3 mousePosition) {
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

        /*
            Starts the Chain Reaction originating from the given Tile.
        */
        public void InvokeChainReaction(TileController tile) {
            StartCoroutine(StartChainReaction(tile));
        }

        /*
            StartChainReaction Coroutine. Implements the Chain Reaction Functionality using Queue of Lists.
        */
        private IEnumerator StartChainReaction(TileController tile) {
            if (!isChainReactionRunning) {
                Queue<List<TileController>> tiles = new Queue<List<TileController>>();
                PlayerType playerType = tile.GetPlayerType();
                Color orbColor = tile.GetOrbController().GetOrbColor();
                isChainReactionRunning = true;
                tiles.Enqueue(new List<TileController>() {tile});
                while (tiles.Count > 0) {
                    List<TileController> frontTiles = tiles.Dequeue();
                    List<TileController> nextTiles = new List<TileController>();
                    yield return StartCoroutine(ExplodeFrontTiles(frontTiles, orbColor));
                    for (int i = 0; i < frontTiles.Count; i++) {
                        TileController frontTile = frontTiles[i];
                        for (int j = 0; j < frontTile.Neighbours.Count; j++) {
                            TileController neighbourTile = frontTile.Neighbours[j];
                            TileType neighbourTileType = neighbourTile.tileType;
                            OrbStatus neighbourOrbStatus = neighbourTile.GetOrbController().GetOrbStatus();
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
                isChainReactionRunning = false;
                PlayerManager.Instance.UpdateTurn();
            }
        }

        /*
            ExplodeFrontTiles Method. As Chain Reaction is BFS, Explodes all Tiles in the front layer simultaneously.
        */
        private IEnumerator ExplodeFrontTiles(List<TileController> frontTiles, Color orbColor) {
            for (int i = 0; i < frontTiles.Count; i++) {
                TileController frontTile = frontTiles[i];
                AudioService.Instance.PlayAudio(SoundType.POP_CLICK);
                frontTile.GetOrbController().DisableOrb();
                List<Transform> NeighbourTransforms = new List<Transform>();
                for (int k = 0; k < frontTile.Neighbours.Count; k++) {
                    NeighbourTransforms.Add(frontTile.Neighbours[k].transform);
                }
                ExplosionService.Instance.ExplodeOrbs(frontTile.transform, NeighbourTransforms, orbColor);    
            }
            yield return new WaitForSeconds(0.25f);
        }

        /*
            GetPlayerActiveTileCount Method. Returns a Dictionary of PlayerType and respective Counts.
            Used to Calculate Win Condition.
        */
        public Dictionary<PlayerType, int> GetPlayerActiveTileCount(PlayerScriptableObjectList PlayerConfigs, int playerCount) {
            Dictionary<PlayerType, int> playerTileCount = new Dictionary<PlayerType, int>();
            for (int i = 0; i < playerCount; i++) {
                playerTileCount[PlayerConfigs.playerConfigs[i].playerType] = 0;
            }
            for (int i = 0; i < ROWS; i++) {
                for (int j = 0; j < COLS; j++) {
                    TileController tile = GridTiles[i, j];
                    PlayerType playerType = tile.GetPlayerType();
                    if (playerType != PlayerType.NONE) {
                        playerTileCount[playerType] += 1;
                    }
                }
            }
            return playerTileCount;
        }
    }

}
