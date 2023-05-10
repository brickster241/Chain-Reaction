using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Services;
using Enums;

namespace Controllers {
    /*
        TileController class. All logic of the Tile Gameobject is handled here.
        Also has reference to OrbController attached with the gameobject.
    */
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

        /*
            Sets Neighbouring TileController Gameobjects.
        */
        public void SetTileNeighbours(List<TileController> TileNeighbours) {
            Neighbours = TileNeighbours;
        }

        /*
            Sets TileIndex at which gameobject is present.
        */
        public void SetTileIndex(int row, int col) {
            tileIndex.x = row;
            tileIndex.y = col;
        }

        /*
            Sets the TileType based on Neighbour Count.
        */
        public void SetTileType() {
            if (Neighbours.Count <= 2) {
                tileType = TileType.CORNER;
            } else if (Neighbours.Count == 3) {
                tileType = TileType.EDGE;
            } else {
                tileType = TileType.MIDDLE;
            }
        }

        /*
            Sets the Color of the Tile. Executed when Mouse is hovering over the tile.
        */
        public void SetTileSpriteColor(Color color) {
            tileSprite.color = color;
        }

        /*
            Sets the Tile Outline color. Is Executed based on the PlayerType color.
        */
        public void SetTileOutlineColor(Color color) {
            tileOutlineSprite.color = color;
        }

        /*
            Sets the PlayerType of the OrbController reference.
        */
        public void SetOrbPlayerType(PlayerType playerType) {
            orbController.SetOrbPlayer(playerType);
        }

        /*
            Fetches the current OrbStatus of OrbController reference attached with the gameobject.
        */
        public OrbStatus GetOrbStatus() {
            return orbController.GetOrbStatus();
        }

        /*
            Fetches the current PlayerType on the Tile.
        */
        public PlayerType GetPlayerType() {
            return orbController.GetOrbPlayerType();
        }

        /*
            Method is Executed when Tile is clicked. Calls the OrbController's onOrbClick Method.
        */
        public void OnTileClick() {
            orbController.OnOrbClick();
        }

        /*
            Fetches the OrbController reference attached with the gameobject.
        */
        public OrbController GetOrbController() {
            return orbController;
        }

        /*
            Invokes the Chain Reaction Mechanism by calling GridService.
        */
        public void InvokeChainReaction() {
            GridService.Instance.InvokeChainReaction(this);
        }
    }

}
