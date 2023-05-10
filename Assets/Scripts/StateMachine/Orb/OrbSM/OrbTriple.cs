using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbTriple : OrbBase
{
    public OrbTriple(OrbSM _orbSM) : base(_orbSM) {}

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        OrbController orbController = orbSM.GetOrbController();
        orbController.FirstOrb.gameObject.SetActive(true);
        orbController.SecondOrb.gameObject.SetActive(true);
        orbController.ThirdOrb.gameObject.SetActive(true);
    }

    public override void OnOrbClick()
    {
        base.OnOrbClick();
        orbSM.GetOrbController().GetTileController().InvokeChainReaction();
    }
}
