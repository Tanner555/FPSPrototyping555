using UnityEngine;
using System.Collections;

namespace S3
{
    public class Destructable_Degenerate : MonoBehaviour
    {
        private Destructable_Master destructibleMaster;
        private bool isHealthLow = false;
        public float degenRate = 1;
        private float nextDegenTime;
        public int healthLoss = 5;

        void OnEnable()
        {
            SetInitialReferences();
            destructibleMaster.EventHealthLow += HealthLow;
        }

        void OnDisable()
        {
            destructibleMaster.EventHealthLow -= HealthLow;
        }

        // Update is called once per frame
        void Update()
        {
            CheckIfHealthShouldDegenerate();
        }

        void SetInitialReferences()
        {
            destructibleMaster = GetComponent<Destructable_Master>();
        }

        void HealthLow()
        {
            isHealthLow = true;
        }

        void CheckIfHealthShouldDegenerate()
        {
            if (isHealthLow)
            {
                if(Time.time > nextDegenTime)
                {
                    nextDegenTime = Time.time + degenRate;
                    destructibleMaster.CallEventDeductHealth(healthLoss);
                }
            }
        }
    }
}