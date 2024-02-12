using UnityEngine;

namespace Game
{
    public class DestAsteroid : Destructable
    {
        public enum AsteroidSize
        {
            Small, // Do not split
            Medium, // Split into 4 small
            Large, // Split into 2 medium
        }

        private const float _FragmentShift = 0.3F;

        private float[][] _SmallFragmentShifts;

        [SerializeField] private float _SegmentThrowForceModifier = 30;

        [SerializeField] private AsteroidSize _Size;
        [SerializeField] private GameObject _Explosion;

        protected override void Start()
        {
            base.Start();
            EventOnDeath.AddListener(SpawnExplosion);
            EventOnDeath.AddListener(SpawnFragments);

            _SmallFragmentShifts = new float[4][];
            _SmallFragmentShifts[0] = new float[2] { _FragmentShift, _FragmentShift };
            _SmallFragmentShifts[1] = new float[2] { _FragmentShift, -_FragmentShift }; ;
            _SmallFragmentShifts[2] = new float[2] { -_FragmentShift, _FragmentShift }; ;
            _SmallFragmentShifts[3] = new float[2] { -_FragmentShift, -_FragmentShift }; ;
        }

        private void SpawnExplosion()
        {
            GameObject explosion = Instantiate(_Explosion, transform);
            explosion.transform.parent = null;
        }

        private void SpawnFragments()
        {
            if (_Size == AsteroidSize.Small)
            {
                return;
            }

            if (_Size == AsteroidSize.Medium)
            {
                for (int i = 0; i < 4; i++)
                {
                    DestAsteroid fragmentPrefab = AsteroidList.GetSmallAsteroidPrefab();

                    if (!fragmentPrefab)
                    {
                        continue;
                    }

                    // Prefabs are made scaled so we need this little trick here
                    // So prefabs will not appear smaller then they already are
                    Vector3 currentScale = transform.localScale;
                    transform.localScale = Vector3.one;

                    GameObject fragment = Instantiate(fragmentPrefab.gameObject, transform);
                    fragment.transform.position = new Vector3(fragment.transform.position.x + _SmallFragmentShifts[i][0], fragment.transform.position.y + _SmallFragmentShifts[i][1], fragment.transform.position.z);
                    Rigidbody2D fragmentRigid = fragment.GetComponent<Rigidbody2D>();
                    fragmentRigid.AddForce(new Vector2(_SmallFragmentShifts[i][0] * _SegmentThrowForceModifier, _SmallFragmentShifts[i][1] * _SegmentThrowForceModifier), ForceMode2D.Force);
                    fragment.transform.parent = null;

                    transform.localScale = currentScale;
                }
                return;
            }
        }
    }
}
