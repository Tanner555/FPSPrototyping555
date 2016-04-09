using UnityEngine;
using System.Collections;

namespace S3
{
    public class Item_Master : MonoBehaviour
    {
        private Player_Master playerMaster;

        public delegate void GeneralEventHandler();
        public event GeneralEventHandler EventObjectThrow;
        public event GeneralEventHandler EventObjectPickup;

        public delegate void PickupActionEventHandler(Transform item);
        public event PickupActionEventHandler EventPickupAction;

        //TODO: Fix Item Master References!

        void OnEnable()
        {
            
        }

        void Start()
        {
            SetInitialReferences();
        }

        public void CallEventObjectThrow()
        {
            if(EventObjectThrow != null)
            {
                EventObjectThrow();
            }
            if (playerMaster != null)
            {
                playerMaster.CallEventHandsEmpty();
                playerMaster.CallEventInventoryChanged();
            }
            playerMaster = null;
        }

        public void CallEventObjectPickup()
        {
            if (EventObjectPickup != null)
            {
                EventObjectPickup();
            }
            if (transform.root.GetComponent<Player_Master>())
            {
                playerMaster = transform.root.GetComponent<Player_Master>();
                playerMaster.CallEventInventoryChanged();
            }
        }

        public void CallEventPickupAction(Transform item)
        {
            if(EventPickupAction != null)
            {
                EventPickupAction(item);
            }
        }

        void SetInitialReferences()
        {
            //if(GameManager_References._player != null)
            //{
            //    playerMaster = GameManager_References._player.GetComponent<Player_Master>();
            //}
            if (transform.root.GetComponent<Player_Master>())
            {
                playerMaster = transform.root.GetComponent<Player_Master>();
            }
            else
            {
                playerMaster = null;
            }
        }
    }
}