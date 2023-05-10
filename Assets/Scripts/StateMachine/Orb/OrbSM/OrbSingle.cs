using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using Controllers;

namespace StateMachine.Orb {
    /*
        OrbSingle class. Handles all the Logic while Orb is in SINGLE OrbType State.
    */
    public class OrbSingle : OrbBase
    {  
        public OrbSingle(OrbSM _orbSM) : base(_orbSM) {}

        /*
            OnStateEnter Function. Executed when Orb first enters the SINGLE OrbType State.
            Enables First Orb.
        */
        public override void OnStateEnter()
        {
            base.OnStateEnter();
            OrbController orbController = orbSM.GetOrbController();
            orbController.FirstOrb.gameObject.SetActive(true);
            orbController.SecondOrb.gameObject.SetActive(false);
            orbController.ThirdOrb.gameObject.SetActive(false);
        }

        /*
            OnOrbClick Method. Executed when Orb is in SINGLE OrbType State and Orb is Clicked.
        */
        public override void OnOrbClick()
        {
            base.OnOrbClick();
            TileType tileType = orbSM.GetOrbController().transform.parent.gameObject.GetComponent<TileController>().tileType;
            if (tileType == TileType.CORNER) {
                Color orbColor = orbSM.GetOrbController().GetOrbColor();
                orbSM.GetOrbController().DisableOrb();
                orbSM.GetOrbController().GetTileController().InvokeChainReaction(orbColor);
            } else if (tileType == TileType.EDGE){
                orbSM.GetOrbController().SwitchOrbStatus(OrbStatus.UNSTABLE);
                orbSM.SwitchState(OrbType.DOUBLE);
            } else {
                orbSM.GetOrbController().SwitchOrbStatus(OrbStatus.STABLE);
                orbSM.SwitchState(OrbType.DOUBLE);
            }
            
        }
    }
}
