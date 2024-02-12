using UnityEngine;

namespace Game
{
    public class ScaleDownAndDestroy : MonoBehaviour
    {
        [SerializeField] private float _ScaleDownPerSecond = 0.2f;
        [SerializeField] private float _ScaleDownUntil = 0.02f;
        private bool _ProcessStarted = false;

        [SerializeField] private GameObject[] _childrenToScaleDown;

        public void StartProcess()
        {
            _ProcessStarted = true;
        }

        private void Update()
        {
            if (!_ProcessStarted)
            {
                return;
            }

            if (gameObject.transform.localScale.x < _ScaleDownUntil)
            {
                Destroy(gameObject);
                return;
            }

            gameObject.transform.localScale = new Vector3(
                gameObject.transform.localScale.x - _ScaleDownPerSecond * Time.deltaTime,
                gameObject.transform.localScale.y - _ScaleDownPerSecond * Time.deltaTime, 1);

            if (_childrenToScaleDown != null)
            {
                foreach (var child in _childrenToScaleDown)
                {
                    child.transform.localScale = new Vector3(
                        child.transform.localScale.x - _ScaleDownPerSecond * Time.deltaTime,
                        child.transform.localScale.y - _ScaleDownPerSecond * Time.deltaTime, 1);
                }
            }
        }
    }
}
