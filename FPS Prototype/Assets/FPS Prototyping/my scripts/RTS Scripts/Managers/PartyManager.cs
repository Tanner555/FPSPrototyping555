using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using System.Collections.Generic;
using UnityEngine.UI;
using S3;

namespace RTSPrototype
{
    public class PartyManager : MonoBehaviour
    {
        public RTSGameMode.ECommanders GeneralCommander;

        public RTSGameMode.EFactions GeneralFaction;
        [HideInInspector]
        public RTSGameMode gamemode;
        [HideInInspector]
        public GameManager_Master gameManagerMaster;
        public Vector3 FollowAllyCameraOffset;
        [HideInInspector]
        public Camera AllyCommandWeaponCamera;
        [HideInInspector]
        public Camera AllyCommandFPCamera;
        public AllyMember AllyInCommand;
        public List<AllyMember> PartyMembers = new List<AllyMember>();
        [HideInInspector]
        public bool isInOverview;
        private int PartyKills;
        private int PartyPoints;
        private int PartyDeaths;

        protected virtual void OnEnable()
        {
            isInOverview = false;

        }

        protected virtual void OnDisable()
        {
            
        }

        // Use this for initialization
        void Start()
        {
            gamemode = GameObject.FindObjectOfType<RTSGameMode>();
            if(gamemode == null)
            {
                Debug.LogWarning("RTS GameMode does not exist!");
            }
            gameManagerMaster = GameObject.FindObjectOfType<GameManager_Master>();
            if(gameManagerMaster == null)
            {
                Debug.LogWarning("Gamemanager does not exist!");
            }
            //turn mouse cursor on here
            //Cursor.lockState = CursorLockMode.None;
            AllyMember firstAlly = FindPartyMembers(false, null);
            SetAllyInCommand(firstAlly);
        }

        // Update is called once per frame
        void Update()
        {
            PartyInputSetup();
        }

        public AllyMember FindPartyMembers(bool pendingAllyLeave, AllyMember allyToLeave)
        {
            PartyMembers.Clear();
            AllyMember[] Allies = GameObject.FindObjectsOfType<AllyMember>();
            foreach(var ally in Allies)
            {
                if (pendingAllyLeave)
                {
                    if(ally != allyToLeave)
                    {
                        if(ally.GeneralCommander == this.GeneralCommander)
                        {
                            PartyMembers.Add(ally);
                        }
                    }
                }
                else
                {
                    if (ally.GeneralCommander == this.GeneralCommander)
                    {
                        PartyMembers.Add(ally);
                    }
                }
            }

            if(PartyMembers.Count <= 0)
            {
                Debug.LogWarning("No partyMembers in Scene!");
                return null;
            }
            else
            {
                AllyMember firstallyfound = PartyMembers[0];
                return firstallyfound;
            }
        }

        public void SetAllyInCommand(AllyMember setToCommand)
        {
            if (gameManagerMaster == null)
                return;

            bool isMenuOn = gameManagerMaster.isMenuOn;
            bool isInventoryOn = gameManagerMaster.isInventoryUIOn;
            if (PartyMembers.Contains(setToCommand) && !isMenuOn && !isInventoryOn)
            {
                //Before Ally Set
                AllyCommandWeaponCamera = setToCommand.WeaponCamera;
                AllyCommandFPCamera = setToCommand.FPCamera; 
                if (AllyCommandWeaponCamera && AllyCommandFPCamera)
                {
                    Transform wCamTransform = AllyCommandWeaponCamera.gameObject.transform;
                    Vector3 wCamLocation = wCamTransform.localPosition;
                    Quaternion wCamRotation = wCamTransform.localRotation;
                    Vector3 locationAOffset = FollowAllyCameraOffset + wCamLocation;
                    //this.transform.parent = setToCommand.transform;
                    //this.transform.localPosition = locationAOffset;
                    // this.transform.localRotation = wCamRotation;
                }
                if (AllyInCommand != null)
                {
                    //Set previous allyincommand's attributes
                    //AllyInCommand.gameObject.GetComponent<Player_Master>().CallEventHandsEmpty();
                    AllyInCommand.GetComponent<AllyInputCenter>().ClearGunCommands();
                }
                //After Ally Set
                AllyInCommand = setToCommand;
                if (gamemode)
                {
                    if(this.GeneralCommander == RTSGameMode.ECommanders.Commander_01)
                    {
                        //Get Camera Viewpoint of AllyInCommand
                    }
                }
                SetPartyComponents();
            }
        }

        public void AllyCommandJump()
        {

        }

        public void AllyCommandStopJumping()
        {

        }

        public void AllyCommandFire()
        {

        }

        public void CheckSelectionRaycast()
        {

        }

        public void AllyCommandMoveForward(float Value)
        {

        }

        public void AllyCommandMoveRight(float Value)
        {

        }

        public void AllyCommandToggleFreeMovement()
        {

        }

        public void PossessAllyAdd()
        {
            if(AllyInCommand && PartyMembers.Count > 0)
            {
                int allyCommandIndex = PartyMembers.IndexOf(AllyInCommand);
                if (allyCommandIndex + 1 > 0 && allyCommandIndex + 1 < PartyMembers.Count)
                {
                    SetAllyInCommand(PartyMembers[allyCommandIndex + 1]);
                }
                else if(PartyMembers.Count > 0)
                {
                    SetAllyInCommand(PartyMembers[0]);
                }
            }
        }

        public void PossessAllySubtract()
        {
            if(AllyInCommand && PartyMembers.Count > 0)
            {
                int allyCommandIndex = PartyMembers.IndexOf(AllyInCommand);
                int endIndex = PartyMembers.Count - 1;
                if (allyCommandIndex - 1 > -1 && allyCommandIndex - 1 < PartyMembers.Count)
                {
                    SetAllyInCommand(PartyMembers[allyCommandIndex - 1]);
                }else if (endIndex > 0 && endIndex < PartyMembers.Count)
                {
                    SetAllyInCommand(PartyMembers[endIndex]);
                }
            }
        }

        //Kills and Points Getters
        public int GetPartyKillCount()
        {
            return 0;
        }

        public int GetPartyPointsScored()
        {
            return 0;
        }

        public int GetPartyDeathCount()
        {
            return 0;
        }

        public void SetPartyComponents()
        {
            //Commander is the current player's Commander
            bool isCurrentPlayerCommander = this.GeneralCommander == RTSGameMode.ECommanders.Commander_01;
            foreach(var ally in PartyMembers)
            {
                if(ally != AllyInCommand || !isCurrentPlayerCommander)
                {
                    foreach (var compSwitch in ally.NonPlayerCompSwitches)
                    {
                            compSwitch.MyComponent.enabled = compSwitch.ShouldEnable;
                    }
                    //Item_Master itemMaster = ally.transform.GetComponentInChildren<Item_Master>();
                    //if(itemMaster != null)
                    //{
                    //   // itemMaster.gameObject.SetActive(false);
                    //}
                }
                else
                {
                    foreach (var compSwitch in ally.PlayerCompSwitches)
                    {
                        if (compSwitch.MyComponent != ally.FPController)
                        {
                            compSwitch.MyComponent.enabled = compSwitch.ShouldEnable;
                        }
                    }
                    //Item_Master itemMaster = ally.transform.GetComponentInChildren<Item_Master>();
                    //if (itemMaster != null)
                    //{
                    //    itemMaster.gameObject.SetActive(true);
                    //}

                    Invoke("SetCommandDelayComponents", 0.05f);
                }
            }
        }

        private void SetCommandDelayComponents()
        {
            if (AllyInCommand)
            {
                AllyInCommand.FPController.enabled = true;
            }
        }

        private void PartyInputSetup()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                PossessAllySubtract();
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                PossessAllyAdd();
            }
        }

    }
}