using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbBase
{
    protected OrbSM orbSM;

    public OrbBase(OrbSM _orbSM) {
        orbSM = _orbSM;
    }

    public virtual void OnStateEnter() {}

    public virtual void OnStateUpdate() {}

    public virtual void OnStateExit() {}

    public virtual void OnOrbClick() {}
}
