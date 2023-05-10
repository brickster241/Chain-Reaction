using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void SetOrbController(OrbController _orbController) {
        orbController = _orbController;
    }

    public OrbController GetOrbController() {
        return orbController;
    }

    public void SwitchState(OrbType orbType) {
        OrbBase newOrbType = GetOrbType(orbType);
        if (currentOrbType == newOrbType) {
            return;
        } else if (currentOrbType != null) {
            currentOrbType.OnStateExit();
        }
        currentOrbType = newOrbType;
        currentOrbType.OnStateEnter();
    }

    private OrbBase GetOrbType(OrbType orbType) {
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

    public void OrbSMUpdate() {
        if (currentOrbType != null)
            currentOrbType.OnStateUpdate();
    }

    public void OnOrbClick() {
        if (currentOrbType != null)
            currentOrbType.OnOrbClick();
    }

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
