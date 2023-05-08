using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : GenericMonoSingleton<PlayerManager>
{
    [SerializeField] PlayerType[] Players;
    int currentPlayerIndex = -1;

    private void Start() {
        UpdateTurn();
    }

    public void UpdateTurn() {
        currentPlayerIndex = (currentPlayerIndex + 1) % Players.Length;
        GridService.Instance.UpdateGridOutlineColor(Players[currentPlayerIndex]);
    }

    public PlayerType GetCurrentPlayerType() {
        return Players[currentPlayerIndex];
    }

}
