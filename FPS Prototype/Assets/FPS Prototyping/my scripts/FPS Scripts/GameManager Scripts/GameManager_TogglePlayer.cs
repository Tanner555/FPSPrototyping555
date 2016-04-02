using UnityEngine;
using System.Collections;
using RTSPrototype;
using UnityStandardAssets.Characters.FirstPerson;

namespace S3
{
    public class GameManager_TogglePlayer : MonoBehaviour
    {
        public FirstPersonController playerController;
        private GameManager_Master gameManagerMaster;
        private RTSGameMode gamemode;
       
        void OnEnable()
        {
            SetInitialReferences();
            gameManagerMaster.MenuToggleEvent += TogglePlayerController;
            gameManagerMaster.InventoryUIToggleEvent += TogglePlayerController;
        }

        void OnDisable()
        {
            gameManagerMaster.MenuToggleEvent -= TogglePlayerController;
            gameManagerMaster.InventoryUIToggleEvent -= TogglePlayerController;
        }

        void SetInitialReferences()
        {
            gameManagerMaster = GetComponent<GameManager_Master>();
            gamemode = GetComponent<RTSGameMode>();
        }

        void TogglePlayerController()
        {
            if (playerController != null)
            {
                playerController.enabled = !playerController.enabled;
            }
        }
    }
}