using UnityEngine;
using System.Collections;

namespace S3
{
    public class Enemy_Health : MonoBehaviour
    {
        private Enemy_Master enemyMaster;
        public int enemyHealth = 100; 

        void OnEnable()
        {
            SetInitialReferences();
            enemyMaster.EventEnemyDeductHealth += DeductHealth;
            //enemyMaster.EventEnemyDie += DisableThis;
            //enemyMaster.EventEnemySetNavTarget += SetAttackTarget;
            // enemyMaster.EventEnemyDeductHealth += PauseNavMeshAgent;
        }

        void OnDisable()
        {
            enemyMaster.EventEnemyDeductHealth -= DeductHealth;
            //enemyMaster.EventEnemyDie -= DisableThis;
            //enemyMaster.EventEnemySetNavTarget -= SetAttackTarget;
            // enemyMaster.EventEnemyDeductHealth -= PauseNavMeshAgent;
        }

        void DeductHealth(int healthChange)
        {
            enemyHealth -= healthChange;
            if(enemyHealth <= 0)
            {
                enemyHealth = 0;
                enemyMaster.CallEventEnemyDie();
                Destroy(gameObject, Random.Range(10, 20));
            }
        }

        void SetInitialReferences()
        {
            enemyMaster = GetComponent<Enemy_Master>();
        }

        //void DisableThis()
        //{
        //    this.enabled = false;
        //}
    }
}