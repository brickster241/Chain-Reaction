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
    private TileService tileService = null;

    private void Start() {
        orbSM = new OrbSM();
        orbStatusSM = new OrbStatusSM();
        orbSM.SetOrbService(this);
        orbStatusSM.SetOrbService(this);
        DisableOrb();
    }

    public Color GetOrbColor() {
        return FirstOrb.color;
    }

    public void SetTileService(TileService _tileService) {
        tileService = _tileService;
    }

    public TileService GetTileService() {
        return tileService;
    }

    public void DisableOrb() {
        SetOrbPlayer(PlayerType.NONE);
        SwitchOrbState(OrbType.NONE);
        SwitchOrbStatus(OrbStatus.NONE);
    }

    public void SetOrbPlayer(PlayerType playerType) {
        orbPlayer = playerType;
        if (orbPlayer == PlayerType.BLUE) {
            FirstOrb.color = Color.blue;
            SecondOrb.color = Color.blue;
            ThirdOrb.color = Color.blue;
        } else if (orbPlayer == PlayerType.RED) {
            FirstOrb.color = Color.red;
            SecondOrb.color = Color.red;
            ThirdOrb.color = Color.red;
        } else if (orbPlayer == PlayerType.GREEN){
            FirstOrb.color = Color.green;
            SecondOrb.color = Color.green;
            ThirdOrb.color = Color.green;
        } else if (orbPlayer == PlayerType.YELLOW){
            FirstOrb.color = Color.yellow;
            SecondOrb.color = Color.yellow;
            ThirdOrb.color = Color.yellow;
        } else {
            FirstOrb.color = Color.white;
            SecondOrb.color = Color.white;
            ThirdOrb.color = Color.white;
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

    public OrbType GetOrbType() {
        return orbSM.GetOrbType();
    }

    public OrbStatus GetOrbStatus() {
        return orbStatusSM.GetOrbStatus();
    }

}
