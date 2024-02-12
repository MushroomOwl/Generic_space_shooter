using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class LevelSelectPanel : MonoBehaviour
    {
        [SerializeField] private LevelProperties _LevelProps;
        [SerializeField] private Text _Title;
        [SerializeField] private Image _Icon;

        public void Init(LevelProperties props)
        {
            _LevelProps = props;
            _Title.text = _LevelProps.Title;
            _Icon.sprite = _LevelProps.Icon;
        }

        public void LoadLevel()
        {
            GameManager.LoadLevel(_LevelProps.Scene);
        }
    }
}
