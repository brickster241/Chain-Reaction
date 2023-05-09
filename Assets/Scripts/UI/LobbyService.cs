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
        AudioService.Instance.PlayAudio(SoundType.BUTTON_CLICK);
        PlayerPrefs.SetInt("PlayerCount", count);
        UpdatePlayerCountText();
    }

    public void OnPlayButtonClick() {
        AudioService.Instance.PlayAudio(SoundType.BUTTON_CLICK);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void UpdatePlayerCountText() {
        PlayerCountText.text = "NO. OF PLAYERS : " + PlayerPrefs.GetInt("PlayerCount", 2);
    }
}
