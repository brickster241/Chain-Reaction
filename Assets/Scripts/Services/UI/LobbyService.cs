using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Enums;
using Generics;

namespace Services {
    /*
        LobbyService MonoSingleton Class. Handles all the UI operations in Lobby Scene.
    */
    public class LobbyService : GenericMonoSingleton<LobbyService>
    {
        [SerializeField] TextMeshProUGUI PlayerCountText;

        private void Start() {
            UpdatePlayerCountText();
        }

        /*
            Updates the Player Preferences of No. of Players.
        */
        public void OnButtonClick(int count) {
            AudioService.Instance.PlayAudio(SoundType.BUTTON_CLICK);
            PlayerPrefs.SetInt("PlayerCount", count);
            UpdatePlayerCountText();
        }

        /*
            Loads the Gameplay Scene.
        */
        public void OnPlayButtonClick() {
            AudioService.Instance.PlayAudio(SoundType.BUTTON_CLICK);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        /*
            Updates No. of Players UI Text based on PlayerPrefs.
        */
        private void UpdatePlayerCountText() {
            PlayerCountText.text = "NO. OF PLAYERS : " + PlayerPrefs.GetInt("PlayerCount", 2);
        }
    }

}
