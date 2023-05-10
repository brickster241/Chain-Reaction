using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using StateMachine.Orb;

namespace Controllers {
    /*
        OrbController class. Attached to every Orb present on the Tile.
        Handles All attributes + State Switching of Orb. 
    */
    public class OrbController : MonoBehaviour
    {
        public SpriteRenderer FirstOrb;
        public SpriteRenderer SecondOrb;
        public SpriteRenderer ThirdOrb;
        private PlayerType orbPlayer = PlayerType.NONE;
        private OrbSM orbSM;
        private OrbStatusSM orbStatusSM;
        private TileController tileController = null;

        /*
            Initializes OrbState Machine for OrbType & OrbStatus.
        */
        private void Start() {
            orbSM = new OrbSM();
            orbStatusSM = new OrbStatusSM();
            orbSM.SetOrbController(this);
            orbStatusSM.SetOrbController(this);
            DisableOrb();
        }

        /*
            Returns the Color of the Orb. Used in Chain Reaction.
        */
        public Color GetOrbColor() {
            return FirstOrb.color;
        }

        /*
            Sets the TileController reference associated with the Orb.
        */
        public void SetTileController(TileController _tileController) {
            tileController = _tileController;
        }

        /*
            Fetches the TileController reference associated with the Orb.
        */
        public TileController GetTileController() {
            return tileController;
        }

        /*
            Disables the Orb. Switches OrbColor, OrbType & OrbStatus to NONE.
        */
        public void DisableOrb() {
            SetOrbPlayer(PlayerType.NONE);
            SwitchOrbState(OrbType.NONE);
            SwitchOrbStatus(OrbStatus.NONE);
        }
        
        /*
            Sets the Color of the Orb based on PlayerType.
        */
        public void SetOrbPlayer(PlayerType playerType) {
            orbPlayer = playerType;
            if (orbPlayer == PlayerType.BLUE) {
                FirstOrb.color = Color.blue;
                SecondOrb.color = Color.blue;
                ThirdOrb.color = Color.blue;
            } else if (orbPlayer == PlayerType.RED) {
                FirstOrb.color = Color.red;
                SecondOrb.color = Color.red;
                ThirdOrb.color = Color.red;
            } else if (orbPlayer == PlayerType.GREEN){
                FirstOrb.color = Color.green;
                SecondOrb.color = Color.green;
                ThirdOrb.color = Color.green;
            } else if (orbPlayer == PlayerType.YELLOW){
                FirstOrb.color = Color.yellow;
                SecondOrb.color = Color.yellow;
                ThirdOrb.color = Color.yellow;
            } else {
                FirstOrb.color = Color.white;
                SecondOrb.color = Color.white;
                ThirdOrb.color = Color.white;
            }
        }

        /*
            Gets the PlayerType of the Orb.
        */
        public PlayerType GetOrbPlayerType() {
            return orbPlayer;
        }

        /*
            Switches OrbStatus to NONE / STABLE / UNSTABLE.
        */
        public void SwitchOrbStatus(OrbStatus _orbStatus) {
            orbStatusSM.SwitchState(_orbStatus);
        }

        /*
            Switches OrbType to NONE / SINGLE / DOUBLE / TRIPLE.
        */
        public void SwitchOrbState(OrbType orbType) {
            orbSM.SwitchState(orbType);
        }

        /*
            OnOrbClick Method. Is Executed when the Tile associated is Clicked.
        */
        public void OnOrbClick() {
            orbSM.OnOrbClick();
        }

        /*
           Returns the current OrbType of the Orb.  
        */
        public OrbType GetOrbType() {
            return orbSM.GetOrbType();
        }

        /*
            Returns the current OrbStatus of the Orb.
        */
        public OrbStatus GetOrbStatus() {
            return orbStatusSM.GetOrbStatus();
        }

    }

}

