using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : GenericMonoSingleton<PlayerManager>
{
    [SerializeField] PlayerScriptableObjectList PlayerConfigs;
    [SerializeField] int PlayerCount;
    int turnCount = 0;
    List<PlayerType> Players;
    int currentPlayerIndex = -1;

    private void Start() {
        Players = new List<PlayerType>();
        PlayerCount = Mathf.Min(PlayerCount, PlayerConfigs.playerConfigs.Length);
        for (int i = 0; i < PlayerCount; i++) {
            Players.Add(PlayerConfigs.playerConfigs[i].playerType);
        }
        UpdateTurn();
    }

    public void UpdateTurn() {
        Dictionary<PlayerType, int> playerTypeCount = GridService.Instance.GetPlayerActiveTileCount(PlayerConfigs, PlayerCount);
        if (currentPlayerIndex == -1 || turnCount == 1) {
            currentPlayerIndex = (currentPlayerIndex + 1) % PlayerCount;
            if (currentPlayerIndex == 0) {
                turnCount += 1;
            }
        } else {
            for (int i = 0; i < PlayerCount; i++) {
                currentPlayerIndex = (currentPlayerIndex + 1) % PlayerCount;
                if (currentPlayerIndex == 0)
                    turnCount += 1;
                if (playerTypeCount[PlayerConfigs.playerConfigs[currentPlayerIndex].playerType] > 0)
                    break;
            }
        }
        if (IsGameOver(playerTypeCount)) {
            // CALL UI
        } else {
            GridService.Instance.UpdateGridOutlineColor(Players[currentPlayerIndex]);
        }
    }

    private bool IsGameOver(Dictionary<PlayerType, int> playerTypeCount) {
        if (turnCount <= 1)
            return false;
        int activePlayers = 0;
        for (int i = 0; i < PlayerCount; i++) {
            PlayerType playerType = PlayerConfigs.playerConfigs[i].playerType;
            if (playerTypeCount[playerType] > 0)
                activePlayers += 1;
        }
        if (activePlayers == 1) {
            Debug.Log("GAME OVER.");
            return true;
        } else {
            return false;
        }
    }

    public PlayerScriptableObject GetPlayerConfig(PlayerType playerType) {
        for (int i = 0; i < PlayerConfigs.playerConfigs.Length; i++) {
            if (PlayerConfigs.playerConfigs[i].playerType == playerType) {
                return PlayerConfigs.playerConfigs[i];
            }
        }
        return null;
    }

    public PlayerType GetCurrentPlayerType() {
        return Players[currentPlayerIndex];
    }

}
