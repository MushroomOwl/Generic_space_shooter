using UnityEngine;

namespace Game
{
    [CreateAssetMenu]
    public class LevelProperties : ScriptableObject
    {
        [SerializeField] private string _Title;
        public string Title => _Title;

        [SerializeField] private string _Scene;
        public string Scene => _Scene;

        [SerializeField] private Sprite _Icon;
        public Sprite Icon => _Icon;

        [SerializeField] private LevelProperties _NextLevel;
        public LevelProperties NextLevel => _NextLevel;
    }
}
