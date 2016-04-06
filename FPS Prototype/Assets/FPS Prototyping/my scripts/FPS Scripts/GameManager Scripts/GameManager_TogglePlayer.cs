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
        private PartyManager partymanager;
       
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
            try {
                gamemode = GetComponent<RTSGameMode>();
                partymanager = GameObject.FindObjectOfType<PartyManager>();
                }
            catch
            {
                gamemode = null;
                partymanager = null;
                Debug.Log("No partymanager or gamemode in the scene!");
            }
        }

        void TogglePlayerController()
        {
            if(partymanager != null)
            {
                if (partymanager.AllyInCommand != null)
                {
                    var fpsCon = partymanager.AllyInCommand.transform.GetComponent<FirstPersonController>();
                    fpsCon.enabled = !fpsCon.enabled;
                }
            }else if (playerController != null)
            {
                playerController.enabled = !playerController.enabled;
            }
        }
    }
}