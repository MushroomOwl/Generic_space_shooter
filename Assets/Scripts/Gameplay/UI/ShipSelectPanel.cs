using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class ShipSelectPanel : MonoBehaviour
    {
        [SerializeField] private ShipProperties _ShipProps;
        [SerializeField] private Text _Title;
        [SerializeField] private Image _Icon;
        [SerializeField] private Image _Hull;
        [SerializeField] private Image _Turn;
        [SerializeField] private Image _Speed;

        public void Init(ShipProperties props)
        {
            float maxHull = 0;
            float maxTurn = 0;
            float maxSpeed = 0;

            foreach (ShipProperties sprop in ShipList.Player)
            {
                if (maxHull < sprop.MaxHP)
                {
                    maxHull = sprop.MaxHP;
                }
                if (maxTurn < sprop.MaxAngularVelocity)
                {
                    maxTurn = sprop.MaxAngularVelocity;
                }
                if (maxSpeed < sprop.MaxLinearVelocity)
                {
                    maxSpeed = sprop.MaxLinearVelocity;
                }
            }

            _ShipProps = props;
            _Title.text = _ShipProps.Name;
            _Icon.sprite = _ShipProps.Icon;

            _Hull.fillAmount = _ShipProps.MaxHP / maxHull;
            _Turn.fillAmount = _ShipProps.MaxAngularVelocity / maxTurn;
            _Speed.fillAmount = _ShipProps.MaxLinearVelocity / maxSpeed;
        }

        public void SelectShipClick()
        {
            Player.SetPlayerShip(_ShipProps);
            MainMenuHandler.SwitchToLevelSelect();
        }
    }
}
