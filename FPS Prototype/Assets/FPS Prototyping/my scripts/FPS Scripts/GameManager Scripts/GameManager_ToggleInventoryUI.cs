using UnityEngine;
using System.Collections;

namespace S3
{
    public class GameManager_ToggleInventoryUI : MonoBehaviour
    {
        [Tooltip("Does this game mode have an inventory? Set to true if that is the case")]
        public bool hasInventory;
        public GameObject inventoryUI;
        public string toggleInventoryButton;
        private GameManager_Master gameManagerMaster;
        //For behaviors
        public string toggleBehaviorButton;
        public bool BehaviorUIIsActive
        {
            get {
                return gameManagerMaster != null ? gameManagerMaster.isBehaviorUIOn : false;
                }
            set {
                if(gameManagerMaster != null)
                gameManagerMaster.isBehaviorUIOn = value;
                }
        }

        // Use this for initialization
        void Start()
        {
            SetInitialReferences();
        }

        // Update is called once per frame
        void Update()
        {
            CheckForInventoryUIToggleRequest();
        }

        void SetInitialReferences()
        {
            gameManagerMaster = GetComponent<GameManager_Master>();
            if(toggleInventoryButton == "")
            {
                Debug.LogWarning("Please type in the name of the button used to toggle the inventory");
                this.enabled = false;
            }
            BehaviorUIIsActive = false;
        }

        void CheckForInventoryUIToggleRequest()
        {
            if(Input.GetButtonUp(toggleInventoryButton) && !gameManagerMaster.isMenuOn && !gameManagerMaster.isGameOver && hasInventory && !BehaviorUIIsActive)
            {
                ToggleInventoryUI();
            }
            if(Input.GetButtonUp(toggleBehaviorButton) && !gameManagerMaster.isMenuOn && !gameManagerMaster.isGameOver && !gameManagerMaster.isInventoryUIOn)
            {
                ToggleBehaviorUI();
            }
        }

        public void ToggleInventoryUI()
        {
            if (inventoryUI != null)
            {
                inventoryUI.SetActive(!inventoryUI.activeSelf);
                gameManagerMaster.isInventoryUIOn = !gameManagerMaster.isInventoryUIOn;
                gameManagerMaster.CallEventInventoryUIToggle();
            }
        }

        void ToggleBehaviorUI()
        {
            BehaviorUIIsActive = !BehaviorUIIsActive;
            gameManagerMaster.CallEventBehaviorUIToggle();
        }
            
    }
}