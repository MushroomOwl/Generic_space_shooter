using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameManager : MonoSingleton<GameManager>
    {
        enum GameState
        {
            LevelPaused,
            LevelInProgress,
            LevelFinished,
            LevelFailed,
            Menu,
        }

        private GameState _State = GameState.LevelInProgress;

        private UnityEvent _GamePaused = new UnityEvent();
        public static UnityEvent GamePaused => _Instance._GamePaused;

        private UnityEvent _GameResumed = new UnityEvent();
        public static UnityEvent GameResumed => _Instance._GameResumed;

        private UnityEvent _GameLevelFinished = new UnityEvent();
        public static UnityEvent GameLevelFinished => _Instance._GameLevelFinished;

        private UnityEvent _GameLevelFailed = new UnityEvent();
        public static UnityEvent GameLevelFailed => _Instance._GameLevelFailed;

        private const string _MainMenuSceneName = "MainMenu";

        static public void StopTime()
        {
            switch (Instance._State)
            {
                case GameState.LevelInProgress:
                    Time.timeScale = 0;
                    Instance._GamePaused?.Invoke();
                    Instance._State = GameState.LevelPaused;
                    break;
                case GameState.LevelFinished:
                    Time.timeScale = 0;
                    break;
                case GameState.LevelFailed:
                    Time.timeScale = 0;
                    break;
                default:
                    return;
            }
        }

        static public void FinishLevel()
        {
            switch (Instance._State)
            {
                case GameState.LevelInProgress:
                    Instance._State = GameState.LevelFinished;
                    StopTime();
                    Instance._GameLevelFinished?.Invoke();
                    break;
                default:
                    return;
            }
        }

        static public void FailLevel()
        {
            switch (Instance._State)
            {
                case GameState.LevelInProgress:
                    Instance._State = GameState.LevelFailed;
                    StopTime();
                    Instance._GameLevelFailed?.Invoke();
                    break;
                default:
                    return;
            }
        }

        static public void ResumeTime()
        {
            switch (Instance._State)
            {
                case GameState.LevelPaused:
                    Time.timeScale = 1;
                    Instance._GameResumed?.Invoke();
                    Instance._State = GameState.LevelInProgress;
                    break;
                default:
                    return;
            }
        }

        static public void TogglePause()
        {
            if (Instance._State == GameState.LevelPaused)
            {
                ResumeTime();
            }
            else
            {
                StopTime();
            }
        }

        static public void LoadLevel(string name)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(name);
        }

        static public void LoadMainMenu()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(_MainMenuSceneName);
        }

        static public void ExitGame()
        {
            Application.Quit();
        }
    }
}