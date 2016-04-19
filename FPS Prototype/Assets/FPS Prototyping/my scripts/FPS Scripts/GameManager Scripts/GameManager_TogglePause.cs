using UnityEngine;
using System.Collections;
using IGBPI;

namespace S3 { 
public class GameManager_TogglePause : MonoBehaviour {
        private GameManager_Master gameManagerMaster;
        private IGBPI_Manager_Master behaviorMaster;
        private bool isPaused;

        void OnEnable()
        {
            SetInitialReferences();
            gameManagerMaster.MenuToggleEvent += TogglePause;
            gameManagerMaster.InventoryUIToggleEvent += TogglePause;
            behaviorMaster.EventToggleBehaviorUI += TogglePause;
        }

        void OnDisable()
        {
            gameManagerMaster.MenuToggleEvent -= TogglePause;
            gameManagerMaster.InventoryUIToggleEvent -= TogglePause;
            behaviorMaster.EventToggleBehaviorUI -= TogglePause;
        }

        void SetInitialReferences()
        {
            gameManagerMaster = GetComponent<GameManager_Master>();
            behaviorMaster = GameObject.FindObjectOfType<IGBPI_Manager_Master>();
        }

        void TogglePause(bool _set)
        {
            if (isPaused)
            {
                Time.timeScale = 1;
                isPaused = false;
            }
            else
            {
                Time.timeScale = 0;
                isPaused = true;
            }
        }

        void TogglePause()
        {
            if (isPaused)
            {
                Time.timeScale = 1;
                isPaused = false;
            }
            else
            {
                Time.timeScale = 0;
                isPaused = true;
            }
        }
        
  }
}