﻿using UnityEngine;
using System.Collections;
using S3;

namespace RTSPrototype
{
    public class AllyInputCenter : MonoBehaviour
    {

        public string buttonPickup;
        public string throwButtonName;
        public string attackButtonName;
        public string reloadButtonName;
        public string burstFireButtonName;
        private Player_DetectItem detectItem;
        private Player_Inventory inventory;

        // Use this for initialization
        void Start()
        {
            detectItem = GetComponent<Player_DetectItem>();
            inventory = GetComponent<Player_Inventory>();
            CheckForCompErrors();
        }

        // Update is called once per frame
        void Update()
        {
            StandardInputSetup();
        }

        void StandardInputSetup()
        {
            //Down Commands
            if (Input.GetButtonDown(buttonPickup) && detectItem.enabled == true)
            {
                detectItem.CheckForItemPickupAttempt();
            }

            if (Input.GetButtonDown(throwButtonName) && inventory.enabled == true)
            {
                AttemptItemThrow();
            }

            if (Input.GetButton(attackButtonName))
            {
                AttemptGunShoot(true);
            }

            if (Input.GetButtonDown(burstFireButtonName))
            {
                AttemptGunActivateBurstFire(true);
            }

            if (Input.GetButtonDown(reloadButtonName))
            {
                AttemptGunReload(true);
            }
            //Up Commands
            if (Input.GetButtonUp(attackButtonName))
            {
                AttemptGunShoot(false);
            }

            if (Input.GetButtonUp(burstFireButtonName))
            {
               // AttemptGunReload(false);
            }

            if (Input.GetButtonUp(reloadButtonName))
            {
              //  AttemptGunActivateBurstFire(false);
            }
        }

        #region ButtonDownCommands
        void AttemptGunReload(bool buttonDown)
        {
            var input = GetGunInput();
            if(input != null && input.enabled == true)
            {
                input.AttemptingGunReload = buttonDown;
            }
            //else
            //{
            //    input.AttemptingGunReload = false;
            //}
        }

        void AttemptGunShoot(bool buttonDown)
        {
            var input = GetGunInput();
            if (input != null && input.enabled == true)
            {
                input.AttemptingGunShoot = buttonDown;
            }
            //else
            //{
            //    input.AttemptingGunShoot = false;
            //}
        }

        void AttemptGunActivateBurstFire(bool buttonDown)
        {
            var input = GetGunInput();
            if (input != null && input.enabled == true)
            {
                input.AttemptingActivateBurstFire = buttonDown;
            }
            //else
            //{
            //    input.AttemptingActivateBurstFire = false;
            //}
        }

        void AttemptItemThrow()
        {
            if (inventory.CurrentHeldItem != null)
            {
                Item_Throw itemThrow = inventory.CurrentHeldItem.GetComponent<Item_Throw>();
                if (itemThrow != null)
                {
                    itemThrow.CheckForThrowInput();
                }
            }
        }

        #endregion

        public void ClearGunCommands()
        {
            AttemptGunShoot(false);
            AttemptGunReload(false);
            AttemptGunActivateBurstFire(false);
        }

        Gun_StandardInput GetGunInput()
        {
            Gun_StandardInput gunInput = null;
            try
            {
                gunInput = inventory.CurrentHeldItem.GetComponent<Gun_StandardInput>();
            }
            catch
            {
                gunInput = null;
            }
            return gunInput;
        }

        void CheckForCompErrors()
        {
            if(detectItem == null)
            {
                Debug.Log("No detect item on ally");
            }
            if(inventory == null)
            {
                Debug.Log("No inventory on ally");
            }
        }
    }
}