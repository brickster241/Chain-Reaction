using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OrbStatusSM
{
    private OrbStatusBase currentOrbStatus = null;
    private OrbService orbService;
    private OrbStableStatus orbStableStatus;
    private OrbUnstableStatus orbUnstableStatus;

    public OrbStatusSM() {
        orbStableStatus = new OrbStableStatus(this);
        orbUnstableStatus = new OrbUnstableStatus(this);
    }

    public void SetOrbService(OrbService _orbService) {
        orbService = _orbService;
    }

    public OrbService GetOrbService() {
        return orbService;
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
        orbService.transform.DORotate(new Vector3(0, 0, 360f), 1f, RotateMode.FastBeyond360);
        orbService.transform.DOMove(orbService.transform.parent.position, 1f);
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
}
