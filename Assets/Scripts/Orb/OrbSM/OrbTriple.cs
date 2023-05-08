using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbTriple : OrbBase
{
    public OrbTriple(OrbSM _orbSM) : base(_orbSM) {}

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        OrbService orbService = orbSM.GetOrbService();
        orbService.FirstOrb.gameObject.SetActive(true);
        orbService.SecondOrb.gameObject.SetActive(true);
        orbService.ThirdOrb.gameObject.SetActive(true);
    }

    public override void OnOrbClick()
    {
        base.OnOrbClick();
        Debug.Log("Invoke Chain Reaction");
        orbSM.GetOrbService().SwitchOrbStatus(OrbStatus.STABLE);
        orbSM.SwitchState(OrbType.NONE);
        
    }
}
