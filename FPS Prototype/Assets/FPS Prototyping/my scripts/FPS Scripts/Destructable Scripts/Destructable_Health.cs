using UnityEngine;
using System.Collections;

namespace S3
{
    public class Destructable_Health : MonoBehaviour
    {
        private Destructable_Master destructableMaster;
        public int health;
        private int startingHealth;
        private bool isExploding = false;

        void OnEnable()
        {
            SetInitialReferences();
            destructableMaster.EventDeductHealth += DeductHealth;
        }

        void OnDisable()
        {
            destructableMaster.EventDeductHealth -= DeductHealth;
        }

        void SetInitialReferences()
        {
            destructableMaster = GetComponent<Destructable_Master>();
            startingHealth = health;
        }

        void DeductHealth(int healthToDeduct)
        {
            health -= healthToDeduct;
            CheckIfHealthLow();
            if(health <= 0 && !isExploding)
            {
                isExploding = true;
                destructableMaster.CallEventDestroyMe();

            }
        }

        void CheckIfHealthLow()
        {
            if(health <= startingHealth / 2)
            {
                destructableMaster.CallEventHealthLow();
            }
        }
    }
}