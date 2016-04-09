using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using RTSPrototype;

namespace S3
{
    public class Player_Inventory : MonoBehaviour
    {
        public Transform inventoryPlayerParent;
        public Transform inventoryUIParent;
        public GameObject uiButton;

        private Player_Master playerMaster;
        private AllyMember _ally;
        private GameManager_ToggleInventoryUI inventoryUIScript;
        private float timeToPlaceInHands = 0.1f;
        public Transform CurrentHeldItem { get { return currentlyHeldItem; } }
        private Transform currentlyHeldItem;
        private int counter;
        private string buttonText;
        public List<Transform> MyInventory { get { return listInventory; } }
        private List<Transform> listInventory = new List<Transform>();

        void OnEnable()
        {
            SetInitialReferences();
            DeactivateAllInventoryItems();
            UpdateInventoryListAndUI();
            CheckIfHandsEmpty();

            playerMaster.EventInventoryChanged += UpdateInventoryListAndUI;
            playerMaster.EventInventoryChanged += CheckIfHandsEmpty;
            playerMaster.EventHandsEmpty += ClearHands;
        }

        void OnDisable()
        {
            playerMaster.EventInventoryChanged -= UpdateInventoryListAndUI;
            playerMaster.EventInventoryChanged -= CheckIfHandsEmpty;
            playerMaster.EventHandsEmpty -= ClearHands; 
        }

        void SetInitialReferences()
        {
            inventoryUIScript = GameObject.Find("GameManager").GetComponent<GameManager_ToggleInventoryUI>();
            playerMaster = GetComponent<Player_Master>();
            _ally = GetComponent<AllyMember>();
            if(_ally == null || _ally.PartyManager == null)
            {
                Debug.Log("Ally or Partymanager could not be set!");
            }
        }
  
        void UpdateInventoryListAndUI()
        {
            counter = 0;
            listInventory.Clear();
            listInventory.TrimExcess();

            ClearInventoryUI();

            foreach(Transform child in inventoryPlayerParent)
            {
                if (child.CompareTag("Item"))
                {
                    listInventory.Add(child);
                    //UI only updates if ally is current player
                    if (_ally.PartyManager.AllyIsCurrentPlayer(_ally))
                    {
                        GameObject go = Instantiate(uiButton) as GameObject;
                        buttonText = child.name;
                        go.GetComponentInChildren<Text>().text = buttonText;
                        int index = counter;
                        go.GetComponent<Button>().onClick.AddListener(delegate { ActivateInventoryItem(index); });
                        go.GetComponent<Button>().onClick.AddListener(inventoryUIScript.ToggleInventoryUI);
                        go.transform.SetParent(inventoryUIParent, false);
                        //for temp use, set active components
                        go.GetComponent<Image>().enabled = true;
                        go.GetComponent<Button>().enabled = true;
                        go.GetComponentInChildren<Text>().enabled = true;
                        //end
                        counter++;
                    }
                }
            }
        }

        void CheckIfHandsEmpty()
        {
            if(currentlyHeldItem == null && listInventory.Count > 0)
            {
                StartCoroutine(PlaceItemInHands(listInventory[listInventory.Count - 1]));
            }
        }

        void ClearHands()
        {
            currentlyHeldItem = null;
        }

        void ClearInventoryUI()
        {
            if (_ally.PartyManager.AllyIsCurrentPlayer(_ally))
            {
                foreach (Transform child in inventoryUIParent)
                {
                    Destroy(child.gameObject);
                }
            }
        }

        public void ActivateInventoryItem(int inventoryIndex)
        {
            DeactivateAllInventoryItems();
            StartCoroutine(PlaceItemInHands(listInventory[inventoryIndex]));
        }

        void DeactivateAllInventoryItems()
        {
            foreach(Transform child in inventoryPlayerParent)
            {
                if (child.CompareTag("Item"))
                {
                    child.gameObject.SetActive(false);
                }
            }
        }

        IEnumerator PlaceItemInHands(Transform itemTransform)
        {
            yield return new WaitForSeconds(timeToPlaceInHands);
            currentlyHeldItem = itemTransform;
            currentlyHeldItem.gameObject.SetActive(true);
        }
    }
}