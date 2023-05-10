using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbNone : OrbBase
{
    public OrbNone(OrbSM _orbSM) : base(_orbSM) {}

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        OrbController orbController = orbSM.GetOrbController();
        orbController.FirstOrb.gameObject.SetActive(false);
        orbController.SecondOrb.gameObject.SetActive(false);
        orbController.ThirdOrb.gameObject.SetActive(false);
    }

    public override void OnOrbClick()
    {
        base.OnOrbClick();
        TileType tileType = orbSM.GetOrbController().transform.parent.gameObject.GetComponent<TileController>().tileType;
        if (tileType == TileType.CORNER) {
            orbSM.GetOrbController().SwitchOrbStatus(OrbStatus.UNSTABLE);
        } else {
            orbSM.GetOrbController().SwitchOrbStatus(OrbStatus.STABLE);
        }
        orbSM.SwitchState(OrbType.SINGLE);
    }
}
