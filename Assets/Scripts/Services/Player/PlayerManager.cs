using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Generics;
using Enums;

namespace Services {
    public class PlayerManager : GenericMonoSingleton<PlayerManager>
    {
        [SerializeField] PlayerScriptableObjectList PlayerConfigs;
        [SerializeField] int PlayerCount;
        int turnCount;
        List<PlayerType> Players;
        int currentPlayerIndex = -1;

        private void Start() {
            Players = new List<PlayerType>();
            turnCount = 0;
            PlayerCount = PlayerPrefs.GetInt("PlayerCount", 2);
            for (int i = 0; i < PlayerCount; i++) {
                Players.Add(PlayerConfigs.playerConfigs[i].playerType);
            }
            UpdateTurn();
        }

        public void UpdateTurn() {
            AudioService.Instance.PlayAudio(SoundType.UPDATE_TURN);
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
            (bool, PlayerType) GameOver = IsGameOver(playerTypeCount);
            if (GameOver.Item1) {
                UIService.Instance.DisplayGameOverUI(GameOver.Item2);
            } else {
                GridService.Instance.UpdateGridOutlineColor(Players[currentPlayerIndex]);
            }
        }

        private (bool, PlayerType) IsGameOver(Dictionary<PlayerType, int> playerTypeCount) {
            if (turnCount <= 1)
                return (false, PlayerType.NONE);
            int activePlayers = 0;
            PlayerType activePlayer = PlayerType.NONE;
            for (int i = 0; i < PlayerCount; i++) {
                PlayerType playerType = PlayerConfigs.playerConfigs[i].playerType;
                if (playerTypeCount[playerType] > 0) {
                    activePlayers += 1;
                    activePlayer = playerType;
                }
            }
            if (activePlayers == 1) {
                return (true, activePlayer);
            } else {
                return (false, PlayerType.NONE);
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

}
