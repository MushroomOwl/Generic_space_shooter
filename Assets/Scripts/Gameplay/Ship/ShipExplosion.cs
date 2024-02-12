using UnityEngine;

namespace Game
{
    public class ShipExplosion : TempEntity
    {
        //[SerializeField] private ParticleSystem _Random;
        //[SerializeField] private ParticleSystem _Radial;

        //public void Init(Sprite sprite)
        //{
        //    var shape = _Random.shape;
        //    shape.sprite = sprite;
        //    shape.texture = sprite.texture;

        //    shape = _Radial.shape;
        //    shape.sprite = sprite;
        //    shape.texture = sprite.texture;
        //}


        [SerializeField] private SpriteRenderer _Renderer;
        [SerializeField] private ParticleSystem _Random;
        [SerializeField] private ParticleSystem _Radial;

        public void Init(Sprite sprite)
        {
            _Renderer.sprite = sprite;

            var shape = _Random.shape;
            shape.texture = sprite.texture;

            shape = _Radial.shape;
            shape.texture = sprite.texture;
        }

    }
}