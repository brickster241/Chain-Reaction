using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbNone : OrbBase
{
    public OrbNone(OrbSM _orbSM) : base(_orbSM) {}

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        OrbService orbService = orbSM.GetOrbService();
        orbService.FirstOrb.gameObject.SetActive(false);
        orbService.SecondOrb.gameObject.SetActive(false);
        orbService.ThirdOrb.gameObject.SetActive(false);
    }

    public override void OnOrbClick()
    {
        base.OnOrbClick();
        TileType tileType = orbSM.GetOrbService().transform.parent.gameObject.GetComponent<TileService>().tileType;
        if (tileType == TileType.CORNER) {
            // CHANGE ORB STATUS TO UNSTABLE
        }
        orbSM.SwitchState(OrbType.SINGLE);
    }
}
