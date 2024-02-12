using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Camera))]
    public class FollowingCameraHandler : MonoBehaviour
    {
        private Camera _Camera;
        private Transform _Target;

        [SerializeField] private float _InterpolationLinear;
        [SerializeField] private float _InterpolationAngular;
        [SerializeField] private float _CameraZOffset;
        [SerializeField] private float _ForwardOffset;
        [SerializeField] private float _SmoothTime;

        private bool _IsTeleported;

        private Vector2 _Velocity;

        private void Awake()
        {
            _Camera = GetComponent<Camera>();
        }

        private void FixedUpdate()
        {
            if (_Camera == null || _Target == null)
            {
                return;
            }

            Vector2 targetPos = _Target.position + _Target.transform.up * _ForwardOffset;
            Vector2 smoothPos = Vector2.SmoothDamp(_Camera.transform.position, targetPos, ref _Velocity, _SmoothTime);

            _Camera.transform.position = new Vector3(targetPos.x, targetPos.y, _CameraZOffset);

            if (_IsTeleported)
            {
                _Camera.transform.position = new Vector3(targetPos.x, targetPos.y, _CameraZOffset);
                _IsTeleported = false;
            }
            else
            {
                _Camera.transform.position = new Vector3(smoothPos.x, smoothPos.y, _CameraZOffset);
            }

            if (_InterpolationAngular > 0)
            {
                _Camera.transform.rotation = Quaternion.Slerp(_Camera.transform.rotation, _Target.rotation,
                    _InterpolationAngular * Time.deltaTime);
            }
        }

        public void SetTarget(Transform newTarget)
        {
            _Target = newTarget;
        }
    }
}
