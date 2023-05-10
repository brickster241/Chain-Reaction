using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;


namespace Scriptables {
    /*
        PlayerScriptableObject Class. ScriptableObject class to define Player Configurations.
    */
    [CreateAssetMenu(fileName = "PlayerScriptableObject", menuName = "Scriptable-Objects/PlayerScriptableObject")]
    public class PlayerScriptableObject : ScriptableObject {
        public PlayerType playerType;
        public Color PlayerOrbColor;
        public Color PlayerGridColor;
        public string PlayerWinText;
    }

}
