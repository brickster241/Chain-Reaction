using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerScriptableObjectList", menuName = "Scriptable-Objects/PlayerScriptableObjectList")]
public class PlayerScriptableObjectList : ScriptableObject {
    public PlayerScriptableObject[] playerConfigs;
}

