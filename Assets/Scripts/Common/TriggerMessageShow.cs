using UnityEngine;

namespace Game
{
    public class TriggerMessageShow : ColliderTriggerEvent
    {
        [TextArea]
        [SerializeField] private string _MessageText;

        private void Awake()
        {
            _TriggerEnterEvent.AddListener(() => LevelGUI.ShowMessage(_MessageText));
            _TriggerExitEvent.AddListener(() => LevelGUI.HideMessage());
        }
    }
}

