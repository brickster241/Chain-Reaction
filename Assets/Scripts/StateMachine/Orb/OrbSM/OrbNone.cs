using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using Controllers;

namespace StateMachine.Orb {
    /*
        OrbNone class. Handles all the Logic while Orb is in None OrbType State.
    */
    public class OrbNone : OrbBase
    {
        public OrbNone(OrbSM _orbSM) : base(_orbSM) {}

        /*
            OnStateEnter Function. Executed when Orb first enters the NONE OrbType State.
            Disables all three Orbs.
        */
        public override void OnStateEnter()
        {
            base.OnStateEnter();
            OrbController orbController = orbSM.GetOrbController();
            orbController.FirstOrb.gameObject.SetActive(false);
            orbController.SecondOrb.gameObject.SetActive(false);
            orbController.ThirdOrb.gameObject.SetActive(false);
        }

        /*
            OnOrbClick Method. Executed when Orb is in NONE OrbType State and Orb is Clicked.
        */
        public override void OnOrbClick()
        {
            base.OnOrbClick();
            TileType tileType = orbSM.GetOrbController().transform.parent.gameObject.GetComponent<TileController>().tileType;
            if (tileType == TileType.CORNER) {
                orbSM.GetOrbController().SwitchOrbStatus(OrbStatus.UNSTABLE);
            } else {
                orbSM.GetOrbController().SwitchOrbStatus(OrbStatus.STABLE);
            }
            orbSM.SwitchState(OrbType.SINGLE);
        }
    }

}
