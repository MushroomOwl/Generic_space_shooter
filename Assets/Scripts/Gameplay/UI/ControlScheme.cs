using UnityEngine;

namespace Game
{
    public enum ControlMode
    {
        Keyboard,
        Mobile
    }

    [CreateAssetMenu(fileName = "ControlScheme")]
    internal class ControlScheme : ScriptableObject
    {
        [SerializeField] private ControlMode _mode;
        public ControlMode Mode => _mode;
    }
}
