using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Services;
using Enums;

namespace Controllers {
    public class TileController : MonoBehaviour
    {   
        [SerializeField] SpriteRenderer tileSprite;
        [SerializeField] SpriteRenderer tileOutlineSprite;
        public TileType tileType;
        private Vector2Int tileIndex;
        public List<TileController> Neighbours;
        [SerializeField] OrbController orbController;

        private void Start() {
            orbController.SetTileController(this);   
        }

        public void SetTileNeighbours(List<TileController> TileNeighbours) {
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

        public void SetTileOutlineColor(Color color) {
            tileOutlineSprite.color = color;
        }

        public void SetOrbPlayerType(PlayerType playerType) {
            orbController.SetOrbPlayer(playerType);
        }

        public OrbStatus GetOrbStatus() {
            return orbController.GetOrbStatus();
        }

        public PlayerType GetPlayerType() {
            return orbController.GetOrbPlayerType();
        }

        public void OnTileClick() {
            orbController.OnOrbClick();
        }

        public OrbController GetOrbController() {
            return orbController;
        }

        public void InvokeChainReaction() {
            GridService.Instance.InvokeChainReaction(this);
        }
    }

}
