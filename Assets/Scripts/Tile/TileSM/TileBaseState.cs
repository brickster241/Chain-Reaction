using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBaseState
{
    protected TileSM tileSM;

    public TileBaseState(TileSM _tileSM) {
        tileSM = _tileSM;
    }

    public virtual void OnStateEnter() {}

    public virtual void OnStateUpdate() {}
    
    public virtual void OnStateExit() {}
}
