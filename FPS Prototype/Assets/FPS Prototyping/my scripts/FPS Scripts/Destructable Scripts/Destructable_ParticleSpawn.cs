using UnityEngine;
using System.Collections;

namespace S3
{
    public class Destructable_ParticleSpawn : MonoBehaviour
    {

        private Destructable_Master destructableMaster;
        public GameObject explosionEffect;

        void OnEnable()
        {
            SetInitialReferences();
            destructableMaster.EventDestroyMe += SpawnExplosion;
        }

        void OnDisable()
        {
            destructableMaster.EventDestroyMe -= SpawnExplosion;
        }

        void SetInitialReferences()
        {
            destructableMaster = GetComponent<Destructable_Master>();

        }

        void SpawnExplosion()
        {
            if(explosionEffect != null)
            {
                Instantiate(explosionEffect, transform.position, Quaternion.identity);
            }
        }
    }
}