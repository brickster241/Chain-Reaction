using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers;
using Enums;

namespace StateMachine.Orb {
    /*
        OrbDouble class. Handles all the Logic while Orb is in DOUBLE OrbType State.
    */
    public class OrbDouble : OrbBase
    {
        public OrbDouble(OrbSM _orbSM) : base(_orbSM) {}

        /*
            OnStateEnter Function. Executed when Orb first enters the DOUBLE OrbType State.
            Enables First & Second Orbs.
        */
        public override void OnStateEnter()
        {
            base.OnStateEnter();
            OrbController orbController = orbSM.GetOrbController();
            orbController.FirstOrb.gameObject.SetActive(true);
            orbController.SecondOrb.gameObject.SetActive(true);
            orbController.ThirdOrb.gameObject.SetActive(false);
        }

        /*
            OnOrbClick Method. Executed when Orb is in DOUBLE OrbType State and Orb is Clicked.
        */
        public override void OnOrbClick()
        {
            base.OnOrbClick();
            TileType tileType = orbSM.GetOrbController().transform.parent.gameObject.GetComponent<TileController>().tileType;
            if (tileType == TileType.EDGE){
                Color orbColor = orbSM.GetOrbController().GetOrbColor();
                orbSM.GetOrbController().DisableOrb();
                orbSM.GetOrbController().GetTileController().InvokeChainReaction(orbColor);
            } else {
                orbSM.GetOrbController().SwitchOrbStatus(OrbStatus.UNSTABLE);
                orbSM.SwitchState(OrbType.TRIPLE);
            }
            
        }
    }

}
