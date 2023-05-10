using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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

    public void SetOrbController(OrbController _orbController) {
        orbController = _orbController;
    }

    public OrbController GetOrbController() {
        return orbController;
    }

    public void SwitchState(OrbStatus orbStatus) {
        OrbStatusBase newOrbStatus = GetOrbStatus(orbStatus);
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

    private void ResetOrb() {
        orbController.transform.DORotate(new Vector3(0, 0, 360f), 1f, RotateMode.FastBeyond360);
        orbController.transform.DOMove(orbController.transform.parent.position, 1f);
    }

    private OrbStatusBase GetOrbStatus(OrbStatus _orbStatus) {
        if (_orbStatus == OrbStatus.STABLE) {
            return orbStableStatus;
        } else if (_orbStatus == OrbStatus.UNSTABLE) {
            return orbUnstableStatus;
        } else {
            return null;
        }
    }

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
