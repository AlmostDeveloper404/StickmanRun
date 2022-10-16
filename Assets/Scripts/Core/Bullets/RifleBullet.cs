using UnityEngine;

namespace Main
{
    public class RifleBullet : Bullet
    {
        protected override void OnTriggerEnter(Collider collider)
        {
            ITakeDamage damagable = collider.GetComponent<ITakeDamage>();

            if (damagable as MonoBehaviour)
            {
                damagable.TakeDamage(Damage);
            }

            DropArea dropArea = collider.GetComponentInParent<DropArea>();
            if (dropArea)
            {
                WaveParticles.transform.position = transform.position;
                WaveParticles.Play();
                gameObject.SetActive(false);
            }
        }
    }
}

