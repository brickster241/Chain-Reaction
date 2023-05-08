using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbService : MonoBehaviour
{
    public SpriteRenderer FirstOrb;
    public SpriteRenderer SecondOrb;
    public SpriteRenderer ThirdOrb;
    private OrbSM orbSM;

    private void Start() {
        orbSM = new OrbSM();
        orbSM.SetOrbService(this);
    }

}
