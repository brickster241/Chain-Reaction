using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine.Orb {
    /*
        OrbStatusBase class. Base class inherited by all OrbStatus States.
    */
    public class OrbStatusBase
    {
        protected OrbStatusSM orbStatusSM;

        public OrbStatusBase(OrbStatusSM _orbStatusSM) {
            orbStatusSM = _orbStatusSM;
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
    }
}

