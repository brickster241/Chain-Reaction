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
            // CHANGE ORB STATUS TO STABLE.
            Debug.Log("Invoke Chain Reaction.");
            orbSM.SwitchState(OrbType.NONE);
        } else {
            // CHANGE ORB STATUS TO VIBRATE
            orbSM.SwitchState(OrbType.TRIPLE);
        }
        
    }
}
