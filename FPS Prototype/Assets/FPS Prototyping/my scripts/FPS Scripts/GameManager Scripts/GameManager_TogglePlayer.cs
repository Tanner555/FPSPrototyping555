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
            //SetInitialReferences();
            //gameManagerMaster.MenuToggleEvent += TogglePlayerController;
            //gameManagerMaster.InventoryUIToggleEvent += TogglePlayerController;
        }

        void Start()
        {
            gameManagerMaster = GetComponent<GameManager_Master>();
            Invoke("SetInitialReferences",0.1f);
            gameManagerMaster.MenuToggleEvent += TogglePlayerController;
            gameManagerMaster.InventoryUIToggleEvent += TogglePlayerController;
            gameManagerMaster.BehaviorUIToggleEvent += TogglePlayerController;
        }

        void OnDisable()
        {
            gameManagerMaster.MenuToggleEvent -= TogglePlayerController;
            gameManagerMaster.InventoryUIToggleEvent -= TogglePlayerController;
            gameManagerMaster.BehaviorUIToggleEvent -= TogglePlayerController;
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