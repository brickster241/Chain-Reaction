using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers;

namespace StateMachine.Orb {
    /*
        OrbTriple class. Handles all the Logic while Orb is in TRIPLE OrbType State.
    */
    public class OrbTriple : OrbBase
    {
        public OrbTriple(OrbSM _orbSM) : base(_orbSM) {}

        /*
            OnStateEnter Function. Executed when Orb first enters the TRIPLE OrbType State.
            Enables All Orbs.
        */
        public override void OnStateEnter()
        {
            base.OnStateEnter();
            OrbController orbController = orbSM.GetOrbController();
            orbController.FirstOrb.gameObject.SetActive(true);
            orbController.SecondOrb.gameObject.SetActive(true);
            orbController.ThirdOrb.gameObject.SetActive(true);
        }

        /*
            OnOrbClick Method. Executed when Orb is in TRIPLE OrbType State and Orb is Clicked.
        */
        public override void OnOrbClick()
        {
            base.OnOrbClick();
            orbSM.GetOrbController().GetTileController().InvokeChainReaction();
        }
    }
}
