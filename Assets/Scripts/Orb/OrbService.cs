using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbService : MonoBehaviour
{
    public SpriteRenderer FirstOrb;
    public SpriteRenderer SecondOrb;
    public SpriteRenderer ThirdOrb;
    private PlayerType orbPlayer = PlayerType.NONE;
    private OrbSM orbSM;
    private OrbStatusSM orbStatusSM;

    private void Start() {
        orbSM = new OrbSM();
        orbStatusSM = new OrbStatusSM();
        orbSM.SetOrbService(this);
        orbStatusSM.SetOrbService(this);
        DisableOrb();
    }

    public void DisableOrb() {
        SwitchOrbState(OrbType.NONE);
        SwitchOrbStatus(OrbStatus.NONE);
    }

    public void SetOrbPlayer(PlayerType playerType) {
        orbPlayer = playerType;
        if (orbPlayer == PlayerType.NONE) {
            FirstOrb.color = Color.white;
            SecondOrb.color = Color.white;
            ThirdOrb.color = Color.white;
        } else if (orbPlayer == PlayerType.RED) {
            FirstOrb.color = Color.red;
            SecondOrb.color = Color.red;
            ThirdOrb.color = Color.red;
        } else {
            FirstOrb.color = Color.green;
            SecondOrb.color = Color.green;
            ThirdOrb.color = Color.green;
        }
    }

    public PlayerType GetOrbPlayerType() {
        return orbPlayer;
    }

    public void SwitchOrbStatus(OrbStatus _orbStatus) {
        orbStatusSM.SwitchState(_orbStatus);
    }

    public void SwitchOrbState(OrbType orbType) {
        orbSM.SwitchState(orbType);
    }

    public void OnOrbClick() {
        orbSM.OnOrbClick();
    }

}
