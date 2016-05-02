using UnityEngine;
using System.Collections;

namespace S3
{
    public class Destructable_PlayerInventoryUpdate : MonoBehaviour
    {
        private Destructable_Master destructibleMaster;
        //private Player_Master playerMaster;

        void OnEnable()
        {
            SetInitialReferences();
            destructibleMaster.EventDestroyMe += ForcePlayerInventoryUpdate;
        }

        void OnDisable()
        {
            destructibleMaster.EventDestroyMe -= ForcePlayerInventoryUpdate;
        }

        // Use this for initialization
        void Start()
        {
            SetInitialReferences();
        }

        void SetInitialReferences()
        {
            if(GetComponent<Item_Master>() == null)
            {
                Destroy(this);
            }

            //if(GameManager_References._player != null)
            //{
            //    playerMaster = GameManager_References._player.GetComponent<Player_Master>();
            //}

            destructibleMaster = GetComponent<Destructable_Master>();
        }

        void ForcePlayerInventoryUpdate()
        {
            if (transform.root.GetComponent<Player_Master>())
            {
                transform.root.GetComponent<Player_Master>().CallEventInventoryChanged();
            }
            //if(playerMaster != null)
            //{
            //    playerMaster.CallEventInventoryChanged();
            //}
        }
    }
}