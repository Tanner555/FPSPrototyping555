using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;
using S3;

namespace RTSPrototype
{
    public class AllyMember : MonoBehaviour
    {
        public RTSGameMode.ECommanders GeneralCommander;
        public GameObject ThirdPersonGObject;
        [HideInInspector]
        public RTSGameMode gamemode;
        [HideInInspector]
        public FirstPersonController FPController;
        [HideInInspector]
        public Camera WeaponCamera;
        [HideInInspector]
        public Camera FPCamera;
        public Player_Master playerMaster;
        public ParticleSystem DeathSparks;
        public AudioClip DeathSound;
        public List<SwitchAllyComponents> NonPlayerCompSwitches;
        public List<SwitchAllyComponents> PlayerCompSwitches;

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
        public RTSGameMode.EFactions AllyFaction;
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
            playerMaster = GetComponent<Player_Master>();
            gamemode = GameObject.FindObjectOfType<RTSGameMode>();
            FPController = GetComponent<FirstPersonController>();
            Camera[] camArray = GetComponentsInChildren<Camera>();
            if (camArray.Length > 0)
            {
                for (int i = 0; i < camArray.Length; i++)
                {
                    if (camArray[i].gameObject.name == "FirstPersonCharacter")
                    {
                        FPCamera = camArray[i].gameObject.GetComponent<Camera>();
                    }
                    else if (camArray[i].gameObject.name == "WeaponCamera")
                    {
                        WeaponCamera = camArray[i].gameObject.GetComponent<Camera>();
                    }
                }
            }
            else
            {
                Debug.LogWarning("No cameras on player");
            }
            if (AllyFaction == RTSGameMode.EFactions.Faction_Default)
            {
                AllyFaction = RTSGameMode.EFactions.Faction_Allies;
            }
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

        public virtual float GetDamageRate(GameObject instigator)
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

        [System.Serializable]
        public struct SwitchAllyComponents
        {
            public Behaviour MyComponent;
            public bool ShouldEnable;
        }

    }
}