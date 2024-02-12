using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class InputHandler : MonoSingleton<InputHandler>
    {
        public enum ControlMode
        {
            Keyboard,
            Mobile
        }

        [SerializeField] private ControlMode _ControlMode;

        private SpaceShip _TargetShip;

        private VJoystick _MobileJoystick;
        private PointerClickHold _MobileFirePrimary;
        private PointerClickHold _MobileFireSecondary;
        private Button _PauseButton;

        private void Start()
        {
            if (_MobileJoystick == null)
            {
                return;
            }

            if (_ControlMode == ControlMode.Keyboard)
            {
                _MobileJoystick.gameObject.SetActive(false);

                _MobileFirePrimary.gameObject.SetActive(false);
                _MobileFireSecondary.gameObject.SetActive(false);

                _PauseButton.gameObject.SetActive(false);
            }
            else
            {
                _MobileJoystick.gameObject.SetActive(true);

                _MobileFirePrimary.gameObject.SetActive(true);
                _MobileFireSecondary.gameObject.SetActive(true);

                _PauseButton.gameObject.SetActive(true);
            }
        }

        public void ConfigureControls()
        {
            if (LevelGUI.Initialized)
            {
                _MobileJoystick = LevelGUI.VJoystick;
                _MobileFirePrimary = LevelGUI.PrimaryFireButton;
                _MobileFireSecondary = LevelGUI.SecondaryFireButton;
                _PauseButton = LevelGUI.PauseButton;
            }
        }

        private void Update()
        {
            if (_TargetShip == null) return;

            if (_ControlMode == ControlMode.Keyboard)
            {
                ControlKeyboard();
                return;
            }

            if (_ControlMode == ControlMode.Mobile)
            {
                ControlMobile();
                return;
            }
        }

        private void ControlKeyboard()
        {
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
            Vector3 direction = _MobileJoystick.Value;

            var d1 = Vector2.Dot(direction, _TargetShip.transform.up);
            var d2 = Vector2.Dot(direction, _TargetShip.transform.right);

            if (_MobileFirePrimary.IsHold)
            {
                _TargetShip.Fire(TurretMode.Primary);
            }

            if (_MobileFireSecondary.IsHold)
            {
                _TargetShip.Fire(TurretMode.Secondary);
            }

            _TargetShip.ThrustControl = Mathf.Max(0, d1);
            _TargetShip.TorqueControl = -d2;
        }

        public static void SetTargetToControl(SpaceShip target)
        {
            _Instance._TargetShip = target;
        }
    }
}

