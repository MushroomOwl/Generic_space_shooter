using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class LevelFailedMenu : MonoBehaviour
    {
        [SerializeField] private Text _Score;
        [SerializeField] private Text _Time;
        [SerializeField] private Text _Kills;

        public void Init()
        {
            gameObject.SetActive(false);
            GameManager.GameLevelFailed.AddListener(ShowLevelFailedMenu);
        }

        private void ShowLevelFailedMenu()
        {
            gameObject.SetActive(true);
            _Score.text = Commons.IntToTextForUI(Player.Initialized ? Player.Score : 0);
            _Time.text = Commons.RoundFloatToTextForUI(LevelController.TimeSinceStart);
            _Kills.text = Commons.IntToTextForUI(Player.Initialized ? Player.KillCount : 0);
        }

        public void RestartButtonClick()
        {
            GameManager.LoadLevel(LevelController.LevelProps.name);
        }

        public void MainMenuButtonClick()
        {
            GameManager.LoadMainMenu();
        }
    }
}