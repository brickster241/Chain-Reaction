using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbDouble : OrbBase
{
    public OrbDouble(OrbSM _orbSM) : base(_orbSM) {}

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        OrbService orbService = orbSM.GetOrbService();
        orbService.FirstOrb.gameObject.SetActive(true);
        orbService.SecondOrb.gameObject.SetActive(true);
        orbService.ThirdOrb.gameObject.SetActive(false);
    }

    public override void OnOrbClick()
    {
        base.OnOrbClick();
        TileType tileType = orbSM.GetOrbService().transform.parent.gameObject.GetComponent<TileService>().tileType;
        if (tileType == TileType.EDGE){
            Debug.Log("Invoke Chain Reaction.");
            orbSM.GetOrbService().SwitchOrbStatus(OrbStatus.STABLE);
            orbSM.SwitchState(OrbType.NONE);
        } else {
            orbSM.GetOrbService().SwitchOrbStatus(OrbStatus.UNSTABLE);
            orbSM.SwitchState(OrbType.TRIPLE);
        }
        
    }
}
