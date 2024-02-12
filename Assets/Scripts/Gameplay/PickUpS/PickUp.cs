using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(CircleCollider2D))]
    public abstract class PickUp : TempEntity
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision == null) return;

            SpaceShip ship = collision.transform.root.GetComponent<SpaceShip>();

            if (ship == null) return;

            if (ship != Player.ActiveShip) return;

            OnPickEffect(ship);
            Destroy(gameObject);
        }

        protected abstract bool OnPickEffect(SpaceShip ship);
    }
}
