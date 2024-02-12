using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private Text _Score;
        [SerializeField] private Text _Time;
        [SerializeField] private Text _Kills;

        public void Init()
        {
            gameObject.SetActive(false);
            GameManager.GameResumed.AddListener(() => gameObject.SetActive(false));
            GameManager.GamePaused.AddListener(ShowPauseMenu);
        }

        private void ShowPauseMenu()
        {
            gameObject.SetActive(true);
            _Score.text = Commons.IntToTextForUI(Player.Initialized ? Player.Score : 0);
            _Time.text = Commons.RoundFloatToTextForUI(LevelController.TimeSinceStart);
            _Kills.text = Commons.IntToTextForUI(Player.Initialized ? Player.KillCount : 0);
        }

        public void ResumeButtonClick()
        {
            GameManager.ResumeTime();
        }

        public void MainMenuButtonClick()
        {
            GameManager.LoadMainMenu();
        }
    }
}