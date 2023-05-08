using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbSingle : OrbBase
{
    public OrbSingle(OrbSM _orbSM) : base(_orbSM) {}

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        OrbService orbService = orbSM.GetOrbService();
        orbService.FirstOrb.gameObject.SetActive(true);
        orbService.SecondOrb.gameObject.SetActive(false);
        orbService.ThirdOrb.gameObject.SetActive(false);
    }

    public override void OnOrbClick()
    {
        base.OnOrbClick();
        TileType tileType = orbSM.GetOrbService().transform.parent.gameObject.GetComponent<TileService>().tileType;
        if (tileType == TileType.CORNER) {
            orbSM.GetOrbService().GetTileService().InvokeChainReaction();
        } else if (tileType == TileType.EDGE){
            orbSM.GetOrbService().SwitchOrbStatus(OrbStatus.UNSTABLE);
            orbSM.SwitchState(OrbType.DOUBLE);
        } else {
            orbSM.GetOrbService().SwitchOrbStatus(OrbStatus.STABLE);
            orbSM.SwitchState(OrbType.DOUBLE);
        }
        
    }
}
