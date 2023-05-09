using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIService : GenericMonoSingleton<UIService>
{
    public bool isUIVisible = false;
    [SerializeField] GameObject GameUI;
    [SerializeField] TextMeshProUGUI UIText;
    [SerializeField] Button PauseButton;
    [SerializeField] Button RestartButton;
    [SerializeField] Button OuterRestartButton;
    [SerializeField] Button BackButton;
    [SerializeField] Button BackToMainMenuButton;

    private void Start() {
        isUIVisible = false;
    }

    public void DisplayGameOverUI(PlayerType playerType) {
        isUIVisible = true;
        PlayerScriptableObject playerConfig = PlayerManager.Instance.GetPlayerConfig(playerType);
        UIText.text = playerConfig.PlayerWinText + " WINS !!";
        UIText.color = playerConfig.PlayerOrbColor;
        BackButton.gameObject.SetActive(false);
        PauseButton.gameObject.SetActive(false);
        OuterRestartButton.gameObject.SetActive(false);
        GameUI.SetActive(true);
    }

    public void OnBackButtonClick() {
        isUIVisible = false;
        PauseButton.gameObject.SetActive(true);
        OuterRestartButton.gameObject.SetActive(true);
        GameUI.SetActive(false);
    }

    public void OnRestartButtonClick() {
        DOTween.Clear();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnMainMenuButtonClick() {
        DOTween.Clear();
        SceneManager.LoadScene(0);
    }

    public void OnPauseButtonClick() {
        OuterRestartButton.gameObject.SetActive(false);
        PauseButton.gameObject.SetActive(false);
        isUIVisible = true;
        UIText.text = "PAUSED";
        UIText.color = Color.red;
        BackButton.gameObject.SetActive(true);
        GameUI.SetActive(true);
    }
}
