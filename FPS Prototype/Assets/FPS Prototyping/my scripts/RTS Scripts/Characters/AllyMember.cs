using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using S3;

namespace RTSPrototype
{
    public class AllyMember : Player_Master
    {
        public RTSGameMode.ECommanders GeneralCommander;
        public GameObject ThirdPersonGObject;
        [HideInInspector]
        public RTSGameMode gamemode
        {
            get
            {
                RTSGameMode GM = GameObject.FindObjectOfType<RTSGameMode>();
                return (GM != null) ? GM : null;
            }
        }
        public ParticleSystem DeathSparks;
        public AudioClip DeathSound;
        [HideInInspector]
        public bool isTargetingEnemy
        {
            get; set;
        }
        [HideInInspector]
        public AllyMember targetedEnemy { get; set; }

        public float AllyHealth { get; set; }
        public float AllyMaxHealth { get; set; }
        public int CurrentAmmo { get; set; }
        public int MaxAmmo
        {
            get; set;
        }
        public float baseEnemyDamage
        {
            get; set;
        }

        //AllyAttributes

        public bool isAlive { get; set; }
        public bool CanFire { get; set; }
        public bool IsExeShootBehavior { get; set; }
        public bool IsTargettingEnemy { get; set; }
        public bool WantsFreeMovement { get; set; }
        public RTSGameMode.EFactions AllyFaction { get; set; }
        public PartyManager PartyManager { get; set; }
        public int FactionPlayerCount { get; set; }
        public int GeneralPlayerCount { get; set; }


        //Keep track of kills and points for gamemode
        public int Kills { get { return _Kills; } set { _Kills = value; } }
        public int Points { get { return _Points; } set { _Points = value; } }
        public int Deaths { get { return _Deaths; } set { _Deaths = value; } }
        private int _Kills;
        private int _Points;
        private int _Deaths;
        private bool AllyCanFire;
        private bool ExecutingShootingBehavior;
        private bool wantsFreedomToMove;
        private float freeMoveThreshold;
        private float DefaultShootDelay;

        protected virtual void OnEnable()
        {

        }

        protected virtual void OnDisable()
        {

        }

        //Use this for initialization
        protected virtual void Start()
        {

        }

        // Update is called once per frame
        protected virtual void Update()
        {

        }

        protected override float GetDamageRate(GameObject instigator)
        {
            return baseEnemyDamage;
        }

        public void AllyFire()
        {

        }

        public void AllyMoveForward(float Val)
        {

        }

        public void AllyMoveRight(float Val)
        {

        }

        public RaycastHit PerformRaycastHit(Vector3 WorldLocation, Vector3 WorldDirection)
        {
            RaycastHit hit;
            Physics.Raycast(WorldLocation, WorldDirection, out hit);
            return hit;
        }

        public void StartShootingBehavior()
        {

        }

        public void StopShootingBehavior()
        {

        }

        public bool IsEnemyFor(AllyMember player)
        {
            return true;
        }

        public void ToggleFreeMovement()
        {

        }

        public void ResetAllyStats()
        {
           
        }


    }
}