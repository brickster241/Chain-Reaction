using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbStatusBase
{
    protected OrbStatusSM orbStatusSM;

    public OrbStatusBase(OrbStatusSM _orbStatusSM) {
        orbStatusSM = _orbStatusSM;
    }

    public virtual void OnStateEnter() {}

    public virtual void OnStateUpdate() {}

    public virtual void OnStateExit() {}
}
