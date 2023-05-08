using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbService : MonoBehaviour
{
    public SpriteRenderer FirstOrb;
    public SpriteRenderer SecondOrb;
    public SpriteRenderer ThirdOrb;
    private OrbSM orbSM;
    private OrbStatusSM orbStatusSM;

    [SerializeField] OrbType orbType;
    [SerializeField] OrbStatus orbStatus;

    private void Start() {
        orbSM = new OrbSM();
        orbStatusSM = new OrbStatusSM();
        orbSM.SetOrbService(this);
        orbStatusSM.SetOrbService(this);
    }

    public void SwitchOrbStatus(OrbStatus _orbStatus) {
        orbStatusSM.SwitchState(_orbStatus);
    }

}
