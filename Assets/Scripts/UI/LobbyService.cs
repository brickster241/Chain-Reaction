using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LobbyService : GenericMonoSingleton<LobbyService>
{
    [SerializeField] TextMeshProUGUI PlayerCountText;

    private void Start() {
        UpdatePlayerCountText();
    }

    public void OnButtonClick(int count) {
        PlayerPrefs.SetInt("PlayerCount", count);
        UpdatePlayerCountText();
    }

    public void OnPlayButtonClick() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void UpdatePlayerCountText() {
        PlayerCountText.text = "NO. OF PLAYERS : " + PlayerPrefs.GetInt("PlayerCount", 2);
    }
}
