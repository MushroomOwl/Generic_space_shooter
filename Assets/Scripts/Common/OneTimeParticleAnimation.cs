using UnityEngine;

namespace Game
{
    public class OneTimeParticleAnimation : MonoBehaviour
    {
        [SerializeField] private ParticleSystem particles;

        private void Update()
        {
            if (particles == null || !particles.IsAlive())
            {
                Destroy(gameObject);
            }
        }
    }
}
