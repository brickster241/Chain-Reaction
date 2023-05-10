using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbDouble : OrbBase
{
    public OrbDouble(OrbSM _orbSM) : base(_orbSM) {}

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        OrbController orbController = orbSM.GetOrbController();
        orbController.FirstOrb.gameObject.SetActive(true);
        orbController.SecondOrb.gameObject.SetActive(true);
        orbController.ThirdOrb.gameObject.SetActive(false);
    }

    public override void OnOrbClick()
    {
        base.OnOrbClick();
        TileType tileType = orbSM.GetOrbController().transform.parent.gameObject.GetComponent<TileController>().tileType;
        if (tileType == TileType.EDGE){
            orbSM.GetOrbController().GetTileController().InvokeChainReaction();
        } else {
            orbSM.GetOrbController().SwitchOrbStatus(OrbStatus.UNSTABLE);
            orbSM.SwitchState(OrbType.TRIPLE);
        }
        
    }
}
