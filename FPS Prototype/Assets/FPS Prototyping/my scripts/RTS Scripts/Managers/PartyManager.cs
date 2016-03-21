using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RTSPrototype
{
    public class PartyManager : MonoBehaviour
    {
        public RTSGameMode.ECommanders GeneralCommander;

        [HideInInspector]
        public RTSGameMode.EFactions GeneralFaction;
        [HideInInspector]
        public RTSGameMode gamemode;
        public Vector3 FollowAllyCameraOffset;
        [HideInInspector]
        public AllyMember AllyInCommand;
        [HideInInspector]
        public List<AllyMember> PartyMembers;
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
            
        }

        // Update is called once per frame
        void Update()
        {

        }

        public AllyMember FindPartyMembers(bool pendingAllyLeave, AllyMember allyToLeave)
        {
            PartyMembers.Clear();
            AllyMember[] Allies = GameObject.FindObjectsOfType<AllyMember>();
            foreach(var ally in Allies)
            {
                if (pendingAllyLeave)
                {

                }
                else
                {

                }
            }
            return null;
        }

        public void SetAllyInCommand(AllyMember setToCommand)
        {
            
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

        }

        public void PossessAllySubtract()
        {

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
    }
}