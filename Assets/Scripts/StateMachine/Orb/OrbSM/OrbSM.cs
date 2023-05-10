using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using Controllers;

namespace StateMachine.Orb {
    /*
        OrbSM class. Handles all the Logic of OrbType State Switching & keeps track of Current OrbType State.
    */
    public class OrbSM
    {
        private OrbBase currentOrbType = null;
        private OrbController orbController;
        private OrbNone orbNone;
        private OrbSingle orbSingle;
        private OrbDouble orbDouble;
        private OrbTriple orbTriple;

        public OrbSM() {
            orbNone = new OrbNone(this);
            orbSingle = new OrbSingle(this);
            orbDouble = new OrbDouble(this);
            orbTriple = new OrbTriple(this);
        }

        /*
            Sets OrbController Reference to current Object.
        */
        public void SetOrbController(OrbController _orbController) {
            orbController = _orbController;
        }

        /*
            Gets OrbController reference attached to current Object.
        */
        public OrbController GetOrbController() {
            return orbController;
        }

        /*
            SwitchState Method. Switches State based on OrbType.
        */
        public void SwitchState(OrbType orbType) {
            OrbBase newOrbType = GetOrbBaseType(orbType);
            if (currentOrbType == newOrbType) {
                return;
            } else if (currentOrbType != null) {
                currentOrbType.OnStateExit();
            }
            currentOrbType = newOrbType;
            currentOrbType.OnStateEnter();
        }

        /*
            GetOrbBaseType Method. Returns OrbBase Class based on OrbType.
        */
        private OrbBase GetOrbBaseType(OrbType orbType) {
            if (orbType == OrbType.NONE) {
                return orbNone;
            } else if (orbType == OrbType.SINGLE) {
                return orbSingle;
            } else if (orbType == OrbType.DOUBLE) {
                return orbDouble;
            } else {
                return orbTriple;
            }
        }

        /*
            OrbSMUpdate Method. Executes the OnStateUpdate of current OrbType State.
        */
        public void OrbSMUpdate() {
            if (currentOrbType != null)
                currentOrbType.OnStateUpdate();
        }

        /*
            OnOrbClick Method. Executes the OnOrbClick of the current OrbType State.
        */
        public void OnOrbClick() {
            if (currentOrbType != null)
                currentOrbType.OnOrbClick();
        }

        /*
            GetOrbType Method. Returns the current OrbType State in terms of Enum.
        */
        public OrbType GetOrbType() {
            if (currentOrbType == orbSingle) {
                return OrbType.SINGLE;
            } else if (currentOrbType == orbDouble) {
                return OrbType.DOUBLE;
            } else if (currentOrbType == orbTriple) {
                return OrbType.TRIPLE;
            } else {
                return OrbType.NONE;
            }
        }
    }

}
