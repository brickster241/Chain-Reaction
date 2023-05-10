using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Enums;
using Generics;
using Scriptables;

namespace Services {
    /*
        UIService MonoSingleton Class. Handles all the UI Operations of Gameplay Scene.
    */
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

        /*
            Displays UI when Game is Complete.
        */
        public void DisplayGameOverUI(PlayerType playerType) {
            AudioService.Instance.PlayAudio(SoundType.GAME_COMPLETE);
            isUIVisible = true;
            PlayerScriptableObject playerConfig = PlayerManager.Instance.GetPlayerConfig(playerType);
            UIText.text = playerConfig.PlayerWinText + " WINS !!";
            UIText.color = playerConfig.PlayerOrbColor;
            BackButton.gameObject.SetActive(false);
            PauseButton.gameObject.SetActive(false);
            OuterRestartButton.gameObject.SetActive(false);
            GameUI.SetActive(true);
        }

        /*
            Returns back to the Game. Disables the UI.
        */
        public void OnBackButtonClick() {
            AudioService.Instance.PlayAudio(SoundType.BUTTON_CLICK);
            isUIVisible = false;
            PauseButton.gameObject.SetActive(true);
            OuterRestartButton.gameObject.SetActive(true);
            GameUI.SetActive(false);
        }

        /*
            Restarts the Game with same no. of Players.
        */
        public void OnRestartButtonClick() {
            AudioService.Instance.PlayAudio(SoundType.BUTTON_CLICK);
            DOTween.Clear();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        /*
            Loads the Lobby Scene.
        */
        public void OnMainMenuButtonClick() {
            AudioService.Instance.PlayAudio(SoundType.BUTTON_CLICK);
            DOTween.Clear();
            SceneManager.LoadScene(0);
        }

        /*
            Enables the UI & Pauses the Game.
        */
        public void OnPauseButtonClick() {
            AudioService.Instance.PlayAudio(SoundType.BUTTON_CLICK);
            OuterRestartButton.gameObject.SetActive(false);
            PauseButton.gameObject.SetActive(false);
            isUIVisible = true;
            UIText.text = "PAUSED";
            UIText.color = Color.red;
            BackButton.gameObject.SetActive(true);
            GameUI.SetActive(true);
        }
    }
}
