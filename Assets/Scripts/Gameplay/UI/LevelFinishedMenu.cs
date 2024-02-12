using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class LevelFinishedMenu : MonoBehaviour
    {
        [SerializeField] private Text _Score;
        [SerializeField] private Text _Time;
        [SerializeField] private Text _Kills;
        [SerializeField] private Button _NextButton;

        public void Init()
        {
            gameObject.SetActive(false);
            GameManager.GameLevelFinished.AddListener(ShowLevelFinishedMenu);
        }

        private void ShowLevelFinishedMenu()
        {
            if (LevelController.LevelProps.NextLevel == null)
            {
                _NextButton.gameObject.SetActive(false);
            }

            gameObject.SetActive(true);
            _Score.text = Commons.IntToTextForUI(Player.Initialized ? Player.Score : 0);
            _Time.text = Commons.RoundFloatToTextForUI(LevelController.TimeSinceStart);
            _Kills.text = Commons.IntToTextForUI(Player.Initialized ? Player.KillCount : 0);
        }

        public void NextButtonClick()
        {
            if (LevelController.LevelProps.NextLevel != null)
            {
                GameManager.LoadLevel(LevelController.LevelProps.NextLevel.name);
            }
        }

        public void MainMenuButtonClick()
        {
            GameManager.LoadMainMenu();
        }
    }
}