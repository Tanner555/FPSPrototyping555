using UnityEngine;
using System.Collections;
using IGBPI;

namespace S3
{
    public class GameManager_ToggleCursor : MonoBehaviour
    {
        private GameManager_Master gameManagerMaster;
        private IGBPI_Manager_Master behaviorMaster;
        private bool isCursorLocked = true; 

        void OnEnable()
        {
            SetInitialReferences();
            gameManagerMaster.MenuToggleEvent += ToggleCursorState;
            gameManagerMaster.InventoryUIToggleEvent += ToggleCursorState;
            behaviorMaster.EventToggleBehaviorUI += ToggleCursorState;
        }

        void OnDisable()
        {
            gameManagerMaster.MenuToggleEvent -= ToggleCursorState;
            gameManagerMaster.InventoryUIToggleEvent -= ToggleCursorState;
            behaviorMaster.EventToggleBehaviorUI -= ToggleCursorState;
        }

        // Update is called once per frame
        void Update()
        {
            CheckIfCursorShouldBeLocked();
        }

        void SetInitialReferences()
        {
            gameManagerMaster = GetComponent<GameManager_Master>();
            behaviorMaster = GameObject.FindObjectOfType<IGBPI_Manager_Master>();
        }

        void ToggleCursorState(bool _set)
        {
            isCursorLocked = !isCursorLocked;
        }

        void ToggleCursorState()
        {
            isCursorLocked = !isCursorLocked;
        }

        void CheckIfCursorShouldBeLocked()
        {
            if (isCursorLocked)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
}