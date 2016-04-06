using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using RTSPrototype;

namespace S3
{
    public class Player_Health : MonoBehaviour
    {
        private GameManager_Master gameManagerMaster;
        private Player_Master playerMaster;
        public int playerHealth = 100;
        public int playerMaxHealth = 100;
        public Text healthText;

        void OnEnable()
        {
            SetInitialReferences();
            SetUI();
            playerMaster.EventPlayerHealthDeduction += DeductHealth;
            playerMaster.EventPlayerHealthIncrease += IncreaseHealth;
        }

        void OnDisable()
        {
            playerMaster.EventPlayerHealthDeduction -= DeductHealth;
            playerMaster.EventPlayerHealthIncrease -= IncreaseHealth;
        }

        void Start()
        {
            //StartCoroutine(TestHealthDeduction());
        }

        void SetInitialReferences()
        {
            gameManagerMaster = GameObject.Find("GameManager").GetComponent<GameManager_Master>();
            playerMaster = GetComponent<Player_Master>();
            playerHealth = playerMaxHealth;
        }
        
        IEnumerator TestHealthDeduction()
        {
            yield return new WaitForSeconds(4);
            DeductHealth(100);
        }

        void DeductHealth(int healthChange)
        {
            playerHealth -= healthChange;
            if(playerHealth <= 0)
            {
                playerHealth = 0;
               // gameManagerMaster.CallEventGameOver();
            }

            SetUI();
        }

        void IncreaseHealth(int healthChange)
        {
            playerHealth += healthChange;
            if(playerHealth > playerMaxHealth)
            {
                playerHealth = playerMaxHealth;
            }

            SetUI();
        }

        void SetUI()
        {
            if(healthText != null)
            {
                healthText.text = playerHealth.ToString();
            }
        }
    }
}