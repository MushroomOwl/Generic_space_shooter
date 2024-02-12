using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class HUD : MonoSingleton<HUD>
    {
        [SerializeField] private GameObject BonusInfoPrefab;

        [SerializeField] private Image _HealthIndicator;
        [SerializeField] private Image _EnergyIndicator;

        [SerializeField] private Text _AmmoIndicator;
        [SerializeField] private Text _LivesIndicator;
        [SerializeField] private Text _ScoreIndicator;
        [SerializeField] private Text _KillsIndicator;

        private void Start()
        {
            UpdateHealthIndicator();
            UpdateEnergyIndicator();
            UpdateLivesIndicator();
            UpdateKillsIndicator();
            UpdateScoreIndicator();
            UpdateAmmoIndicator();
        }

        public static void UpdateHealthIndicator()
        {
            _Instance._HealthIndicator.fillAmount = Player.ActiveShip == null ? 0 : Player.ActiveShip.CurrentHealthPercent / 100;
        }
        public static void UpdateEnergyIndicator()
        {
            _Instance._EnergyIndicator.fillAmount = Player.ActiveShip == null ? 0 : Player.ActiveShip.CurrentEnergyPercent / 100;
        }

        public static void UpdateLivesIndicator()
        {
            _Instance._LivesIndicator.text = Commons.IntToTextForUI(Player.Initialized ? Player.NumLives : 0);
        }

        public static void UpdateScoreIndicator()
        {
            if (!Player.Initialized) return;
            _Instance._ScoreIndicator.text = Commons.IntToTextForUI(Player.Initialized ? Player.Score : 0);
        }

        public static void UpdateKillsIndicator()
        {
            if (!Player.Initialized) return;
            _Instance._KillsIndicator.text = Commons.IntToTextForUI(Player.Initialized ? Player.KillCount : 0);
        }

        public static void UpdateAmmoIndicator()
        {
            _Instance._AmmoIndicator.text = Commons.IntToTextForUI(Player.ActiveShip == null ? 0 : Player.ActiveShip.CurrentAmmo);
        }
    }
}