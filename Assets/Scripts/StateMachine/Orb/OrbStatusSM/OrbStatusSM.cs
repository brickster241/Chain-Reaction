using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Enums;
using Controllers;

namespace StateMachine.Orb {
    /*
        OrbStatusSM class. Handles all the Logic of OrbStatus State Switching & keeps track of Current OrbStatus of Orb.
    */
    public class OrbStatusSM
    {
        private OrbStatusBase currentOrbStatus = null;
        private OrbController orbController;
        private OrbStableStatus orbStableStatus;
        private OrbUnstableStatus orbUnstableStatus;

        public OrbStatusSM() {
            orbStableStatus = new OrbStableStatus(this);
            orbUnstableStatus = new OrbUnstableStatus(this);
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
            SwitchState Method. Switches State based on OrbStatus Enum.
        */
        public void SwitchState(OrbStatus orbStatus) {
            OrbStatusBase newOrbStatus = GetOrbBaseStatus(orbStatus);
            if (currentOrbStatus == newOrbStatus) {
                return;
            } else if (currentOrbStatus != null) {
                currentOrbStatus.OnStateExit();
            }
            currentOrbStatus = newOrbStatus;
            if (currentOrbStatus != null) {
                currentOrbStatus.OnStateEnter();
            } else {
                ResetOrb();
            }
        }

        /*
            Resets the Rotation & Position of the Orb.
        */
        private void ResetOrb() {
            orbController.transform.DORotate(new Vector3(0, 0, 360f), 1f, RotateMode.FastBeyond360);
            orbController.transform.DOMove(orbController.transform.parent.position, 1f);
        }

        /*
            GetOrbBaseStatus Method. Returns OrbStatusBase Class based on OrbStatus Enum.
        */
        private OrbStatusBase GetOrbBaseStatus(OrbStatus _orbStatus) {
            if (_orbStatus == OrbStatus.STABLE) {
                return orbStableStatus;
            } else if (_orbStatus == OrbStatus.UNSTABLE) {
                return orbUnstableStatus;
            } else {
                return null;
            }
        }

        /*
            GetOrbStatus Method. Returns the current OrbStatus State in terms of Enum.
        */
        public OrbStatus GetOrbStatus() {
            if (currentOrbStatus == orbUnstableStatus) {
                return OrbStatus.UNSTABLE;
            } else if (currentOrbStatus == orbStableStatus) {
                return OrbStatus.STABLE;
            } else {
                return OrbStatus.NONE;
            }
        }
    }

}
