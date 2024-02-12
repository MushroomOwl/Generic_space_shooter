using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class BonusUI : MonoBehaviour
    {
        [SerializeField] private Bonus _Bonus;
        [SerializeField] private Slider _Slider;
        [SerializeField] private Image _BonusIcon;
        [SerializeField] private bool initialized;

        public void Init(Bonus bonus, GameObject panel)
        {
            _Bonus = bonus;
            gameObject.transform.SetParent(panel.transform);
            _Bonus.OnDestroy.AddListener(() => Destroy(gameObject));

            _BonusIcon.sprite = _Bonus.Icon;

            initialized = true;
        }

        void Update()
        {
            if (!_Bonus && initialized)
            {
                Destroy(gameObject);
            }

            _Slider.value = _Bonus.TimeLeftPercent;
        }
    }
}
