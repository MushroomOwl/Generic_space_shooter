using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class MainMenuHandler : MonoSingleton<MainMenuHandler>
    {
        [SerializeField] private GameObject _ShipSelectPanelsAnchor;
        [SerializeField] private GameObject _LevelSelectPanelsAnchor;

        [SerializeField] private ShipSelectPanel _ShipSelectPanelPrefab;
        [SerializeField] private LevelSelectPanel _LevelSelectPanelPrefab;

        public void Start()
        {
            foreach (ShipProperties props in ShipList.Player)
            {
                Instantiate(_ShipSelectPanelPrefab, _ShipSelectPanelsAnchor.transform).Init(props);
            }

            foreach (LevelProperties props in LevelList.Levels)
            {
                Instantiate(_LevelSelectPanelPrefab, _LevelSelectPanelsAnchor.transform).Init(props);
            }
        }

        public void ExitGame()
        {
            GameManager.ExitGame();
        }

        public static void SwitchToLevelSelect()
        {
            _Instance._ShipSelectPanelsAnchor.SetActive(false);
            _Instance._LevelSelectPanelsAnchor.SetActive(true);
        }
    }
}
