using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbSingle : OrbBase
{
    public OrbSingle(OrbSM _orbSM) : base(_orbSM) {}

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        OrbController orbController = orbSM.GetOrbController();
        orbController.FirstOrb.gameObject.SetActive(true);
        orbController.SecondOrb.gameObject.SetActive(false);
        orbController.ThirdOrb.gameObject.SetActive(false);
    }

    public override void OnOrbClick()
    {
        base.OnOrbClick();
        TileType tileType = orbSM.GetOrbController().transform.parent.gameObject.GetComponent<TileController>().tileType;
        if (tileType == TileType.CORNER) {
            orbSM.GetOrbController().GetTileController().InvokeChainReaction();
        } else if (tileType == TileType.EDGE){
            orbSM.GetOrbController().SwitchOrbStatus(OrbStatus.UNSTABLE);
            orbSM.SwitchState(OrbType.DOUBLE);
        } else {
            orbSM.GetOrbController().SwitchOrbStatus(OrbStatus.STABLE);
            orbSM.SwitchState(OrbType.DOUBLE);
        }
        
    }
}
