using UnityEngine;
using System.Collections;

namespace S3
{
    public class GameManager_Master : MonoBehaviour
    {
        public delegate void GameManagerEventHandler();
        public event GameManagerEventHandler MenuToggleEvent;
        public event GameManagerEventHandler InventoryUIToggleEvent;
        public event GameManagerEventHandler RestartToggleEvent;
        public event GameManagerEventHandler GoToMenuSceneEvent;
        public event GameManagerEventHandler GameOverEvent;

        public bool isGameOver;
        public bool isInventoryUIOn;
        public bool isMenuOn;

        public void CallEventMenuToggle()
        {
            if(MenuToggleEvent != null)
            {
                MenuToggleEvent();
            }
        }

        public void CallEventInventoryUIToggle()
        {
            if(InventoryUIToggleEvent != null)
            {
                InventoryUIToggleEvent();
            }
        }

        public void CallEventRestartLevel()
        {
            if (RestartToggleEvent != null)
            {
                RestartToggleEvent();
            }
        }

        public void CallEventGoToMenuScene()
        {
            if (GoToMenuSceneEvent != null)
            {
                GoToMenuSceneEvent();
            }
        }

        public void CallEventGameOver()
        {
            if (GameOverEvent != null)
            {
                GameOverEvent();
            }
        }
    }
}