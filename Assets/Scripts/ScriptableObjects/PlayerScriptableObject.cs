using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerScriptableObject", menuName = "Scriptable-Objects/PlayerScriptableObject")]
public class PlayerScriptableObject : ScriptableObject {
    public PlayerType playerType;
    public Color PlayerOrbColor;
    public Color PlayerGridColor;
}
