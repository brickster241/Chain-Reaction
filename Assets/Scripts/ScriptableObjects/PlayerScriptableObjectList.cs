using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scriptables {
    /*
        PlayerScriptableObjectList class. Used to Create Nested Scriptable Object.
    */
    [CreateAssetMenu(fileName = "PlayerScriptableObjectList", menuName = "Scriptable-Objects/PlayerScriptableObjectList")]
    public class PlayerScriptableObjectList : ScriptableObject {
        public PlayerScriptableObject[] playerConfigs;
    }
}
