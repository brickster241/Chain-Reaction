using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine.Orb {
    /*
        OrbBase class. Base class inherited by OrbType States.
    */
    public class OrbBase
    {
        protected OrbSM orbSM;

        public OrbBase(OrbSM _orbSM) {
            orbSM = _orbSM;
        }

        /*
            OnStateEnter Function. Executed when Orb enters this state.
        */
        public virtual void OnStateEnter() {}

        /*
            OnStateUpdate Function. Executed every frame while Orb stays in this state.
        */
        public virtual void OnStateUpdate() {}

        /*
            OnStateExit Function. Executed when Orb exits the State.
        */
        public virtual void OnStateExit() {}

        /*
            OnOrbClick Function. Executed when Orb is Clicked while in this state.
        */
        public virtual void OnOrbClick() {}
    }
}
