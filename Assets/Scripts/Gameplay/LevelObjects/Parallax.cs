using UnityEngine;

namespace Game
{
    public class ParallaxBackground : MonoBehaviour
    {
        [SerializeField] private GameObject[] _Layer;
        private Material[] _LayerMaterial;
        private Transform[] _LayerTransform;

        [Range(0.0f, 4.0f)]
        [SerializeField] private float[] _Factor;
        [Range(0.0f, 10.0f)]
        [SerializeField] private float[] _Scale;

        private Transform _CameraTransform;

        private Vector2 _InitialOffset;

        public void SetCamera(FollowingCameraHandler camera)
        {
            _CameraTransform = camera.transform;
        }

        void Start()
        {
            _InitialOffset = Random.insideUnitCircle;

            _LayerMaterial = new Material[_Layer.Length];
            _LayerTransform = new Transform[_Layer.Length];

            for (int i = 0; i < _Layer.Length; i++)
            {
                _LayerMaterial[i] = _Layer[i].GetComponent<MeshRenderer>().material;
                _LayerTransform[i] = _Layer[i].transform;

                _LayerMaterial[i].mainTextureScale = Vector2.one * _Scale[i];
            }
        }

        void Update()
        {
            if (_Layer == null || _CameraTransform == null)
            {
                return;
            }

            for (int i = 0; i < _Layer.Length; i++)
            {
                if (_Layer[i] == null)
                {
                    continue;
                }

                Vector2 offset = _InitialOffset;

                offset.x += _LayerTransform[i].position.x / _LayerTransform[i].localScale.x / _Factor[i];
                offset.y += _LayerTransform[i].position.y / _LayerTransform[i].localScale.y / _Factor[i];

                _LayerMaterial[i].mainTextureOffset = offset;

                SyncToCameraPosition(ref _LayerTransform[i]);
            }
        }

        void SyncToCameraPosition(ref Transform layerTransform)
        {
            if (_CameraTransform == null || layerTransform == null)
            {
                return;
            }

            layerTransform.position = new Vector3(_CameraTransform.position.x, _CameraTransform.position.y, layerTransform.position.z);
        }
    }
}