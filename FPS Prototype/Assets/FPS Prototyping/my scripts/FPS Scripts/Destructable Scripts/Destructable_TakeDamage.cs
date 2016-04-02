using UnityEngine;
using System.Collections;

namespace S3
{
    public class Destructable_TakeDamage : MonoBehaviour
    {
        private Destructable_Master destructableMaster;
        // Use this for initialization
        void Start()
        {
            SetInitialReferences();
        }

        void SetInitialReferences()
        {
            destructableMaster = GetComponent<Destructable_Master>();
            
        }

        public void ProcessDamage(int damage)
        {
            destructableMaster.CallEventDeductHealth(damage);
        }
    }
}