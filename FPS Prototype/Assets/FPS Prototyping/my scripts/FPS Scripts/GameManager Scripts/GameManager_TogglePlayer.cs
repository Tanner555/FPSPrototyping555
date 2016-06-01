using UnityEngine;
using System.Collections;
using RTSPrototype;
using UnityStandardAssets.Characters.FirstPerson;
using IGBPI;

namespace S3
{
    public class GameManager_TogglePlayer : MonoBehaviour
    {
        public FirstPersonController playerController;
        private GameManager_Master gameManagerMaster;
        private IGBPI_Manager_Master behaviorMaster;
        private RTSGameMode gamemode;
        private PartyManager partymanager;

        void OnEnable()
        {
            //SetInitialReferences();
            //gameManagerMaster.MenuToggleEvent += TogglePlayerController;
            //gameManagerMaster.InventoryUIToggleEvent += TogglePlayerController;
        }

        void Start()
        {
            Invoke("SetInitialReferences", 0.1f);
             gameManagerMaster = GetComponent<GameManager_Master>();
            behaviorMaster = FindObjectOfType<IGBPI_Manager_Master>();
            gameManagerMaster.MenuToggleEvent += TogglePlayerController;
            gameManagerMaster.InventoryUIToggleEvent += TogglePlayerController;
            behaviorMaster.EventToggleBehaviorUI += TogglePlayerController;
        }

        void OnDisable()
        {
            gameManagerMaster.MenuToggleEvent -= TogglePlayerController;
            gameManagerMaster.InventoryUIToggleEvent -= TogglePlayerController;
            behaviorMaster.EventToggleBehaviorUI -= TogglePlayerController;
        }

        void SetInitialReferences()
        {
            try {
                gamemode = FindObjectOfType<RTSGameMode>();
                partymanager = gamemode.GeneralInCommand;
                }
            catch
            {
                gamemode = null;
                partymanager = null;
                Debug.Log("No partymanager or gamemode in the scene!");
            }
            if(gamemode == null)
            {
                Debug.LogError("No gamemode");
            }
            if(partymanager == null)
            {
                Debug.LogError("No partymanager");
            }
        }
        //TODO: Fix GUI issues when paused
        void TogglePlayerController()
        {
            if (partymanager != null)
            {
                if (partymanager.AllyInCommand != null)
                {
                    var fpsCon = partymanager.AllyInCommand.transform.GetComponent<FirstPersonController>();
                    fpsCon.enabled = !fpsCon.enabled;
                }
            }
            else if (playerController != null)
            {
                playerController.enabled = !playerController.enabled;
            }
        }

        void TogglePlayerController(bool _set)
        {
            if (partymanager != null)
            {
                if (partymanager.AllyInCommand != null)
                {
                    var fpsCon = partymanager.AllyInCommand.transform.GetComponent<FirstPersonController>();
                    fpsCon.enabled = !fpsCon.enabled;
                }
            }
            else if (playerController != null)
            {
                playerController.enabled = !playerController.enabled;
            }
        }
    }
}