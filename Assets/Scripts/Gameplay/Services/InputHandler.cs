using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class InputHandler : MonoSingleton<InputHandler>
    {
        [SerializeField] private ControlScheme _controlScheme;

        private SpaceShip _TargetShip;

        private VJoystick _MobileJoystick;
        private PointerClickHold _MobileFirePrimary;
        private PointerClickHold _MobileFireSecondary;
        private Button _PauseButton;

        private bool _controlsConfigured;

        public void ConfigureControls()
        {
            if (!LevelGUI.Initialized)
            {
                return;
            }

            _MobileJoystick = LevelGUI.VJoystick;
            _MobileFirePrimary = LevelGUI.PrimaryFireButton;
            _MobileFireSecondary = LevelGUI.SecondaryFireButton;
            _PauseButton = LevelGUI.PauseButton;

            if (_controlScheme.Mode == ControlMode.Keyboard)
            {
                LevelGUI.DisableMobileControls();
            }
            else
            {
                LevelGUI.EnableMobileControls();
                _PauseButton.onClick.AddListener(GameManager.TogglePause);
            }

            _controlsConfigured = true;
        }

        private void Update()
        {
            if (_TargetShip == null) return;

            if (_controlScheme.Mode == ControlMode.Keyboard)
            {
                ControlKeyboard();
                return;
            }

            if (_controlScheme.Mode == ControlMode.Mobile)
            {
                ControlMobile();
                return;
            }
        }

        private void ControlKeyboard()
        {
            if (!_controlsConfigured)
            {
                return;
            }

            float thrust = 0;
            if (Input.GetKey(KeyCode.UpArrow)) thrust = 1;
            if (Input.GetKey(KeyCode.DownArrow)) thrust = -1;

            float torque = 0;
            if (Input.GetKey(KeyCode.RightArrow)) torque = -1;
            if (Input.GetKey(KeyCode.LeftArrow)) torque = 1;

            if (Input.GetKey(KeyCode.Z))
            {
                _TargetShip.Fire(TurretMode.Primary);
            }

            if (Input.GetKey(KeyCode.X))
            {
                _TargetShip.Fire(TurretMode.Secondary);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameManager.TogglePause();
            }

            _TargetShip.ThrustControl = thrust;
            _TargetShip.TorqueControl = torque;
        }

        private void ControlMobile()
        {
            if (!_controlsConfigured)
            {
                return;
            }

            Vector3 direction = _MobileJoystick.Value;

            var d1 = Vector2.Dot(direction, Vector2.up);
            var d2 = Vector2.Dot(direction, Vector2.left);

            if (_MobileFirePrimary.IsHold)
            {
                _TargetShip.Fire(TurretMode.Primary);
            }

            if (_MobileFireSecondary.IsHold)
            {
                _TargetShip.Fire(TurretMode.Secondary);
            }

            _TargetShip.ThrustControl = d1;
            _TargetShip.TorqueControl = d2;
        }

        public static void SetTargetToControl(SpaceShip target)
        {
            _Instance._TargetShip = target;
        }
    }
}

