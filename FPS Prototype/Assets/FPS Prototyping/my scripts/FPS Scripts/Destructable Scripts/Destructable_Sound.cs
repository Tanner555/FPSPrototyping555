using UnityEngine;
using System.Collections;

namespace S3
{
    public class Destructable_Sound : MonoBehaviour
    {
        private Destructable_Master destructableMaster;
        public float explosionVolume = 0.5f;
        public AudioClip explodingSound;

        void OnEnable()
        {
            SetInitialReferences();
            destructableMaster.EventDestroyMe += PlayExplosionSound;
        }

        void OnDisable()
        {
            destructableMaster.EventDestroyMe -= PlayExplosionSound;
        }

        void SetInitialReferences()
        {
            destructableMaster = GetComponent<Destructable_Master>();

        }

        void PlayExplosionSound()
        {
            if(explodingSound != null)
            {
                AudioSource.PlayClipAtPoint(explodingSound, transform.position, explosionVolume);
            }
        }
    }
}