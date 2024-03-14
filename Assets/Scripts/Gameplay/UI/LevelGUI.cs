using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class LevelGUI : MonoSingleton<LevelGUI>
    {
        [SerializeField] private LevelFinishedMenu _LvlFinishedM;
        [SerializeField] private LevelFailedMenu _LvlFailedM;
        [SerializeField] private PauseMenu _PauseM;

        [SerializeField] private GameObject _MobileControls;

        [SerializeField] private Button _PauseButton;
        [SerializeField] private PointerClickHold _PrimaryFireButton;
        [SerializeField] private PointerClickHold _SecondaryFireButton;
        [SerializeField] private VJoystick _VJoystick;

        [SerializeField] private MessageBoard _Message;

        public static void ShowMessage(string text)
        {
            _Instance._Message?.ShowMessage(text);
        }

        public static void HideMessage()
        {
            _Instance._Message?.HideMessage();
        }

        public static void EnableMobileControls()
        {
            _Instance._MobileControls.SetActive(true);
        }

        public static void DisableMobileControls()
        {
            _Instance._MobileControls.SetActive(false);
        }

        public static LevelFinishedMenu LvlFinishedM => _Instance._LvlFinishedM;
        public static LevelFailedMenu LvlFailedM => _Instance._LvlFailedM;
        public static PauseMenu PauseM => _Instance._PauseM;

        public static Button PauseButton => _Instance._PauseButton;
        public static PointerClickHold PrimaryFireButton => _Instance._PrimaryFireButton;
        public static PointerClickHold SecondaryFireButton => _Instance._SecondaryFireButton;
        public static VJoystick VJoystick => _Instance._VJoystick;
    }
}
