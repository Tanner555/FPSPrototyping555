using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;
using S3;

namespace RTSPrototype
{
    public class AllyMember : MonoBehaviour
    {
        //Inspector Set Variables
        public RTSGameMode.EFactions AllyFaction;
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
        [HideInInspector]
        public Player_Master playerMaster;
        [HideInInspector]
        public Player_Inventory pInventory;
        public GameObject DeathSparks;
        public AudioClip DeathSound;
        public List<SwitchAllyComponents> NonPlayerCompSwitches;
        public List<SwitchAllyComponents> PlayerCompSwitches;

        //Properties
        [HideInInspector]
        public bool isTargetingEnemy
        {
            get; set;
        }
        public bool isCurrentPlayer
        {
            get { return PartyManager ? PartyManager.AllyIsCurrentPlayer(this) : false; }
        }
        [HideInInspector]
        public AllyMember targetedEnemy { get; set; }

        public int AllyHealth {
            get
            {
                try
                {
                    return playerMaster.GetComponent<Player_Health>().playerHealth;
                }
                catch
                {
                    return 0;
                }
            }
        }
        public int AllyMaxHealth
        {
            get
            {
                try
                {
                    return playerMaster.GetComponent<Player_Health>().playerMaxHealth;
                }
                catch
                {
                    return 0;
                }
            }
        }
        public int CurrentGunAmmoCarried
        {
            get
            {
                try
                {
                    return GetAmmoTypeFromGunMaster(getCurrentGun).ammoCurrentCarried;
                }
                catch
                {
                    return 0;
                }
            }
        }
        
        public int CurrentGunMaxAmmoCarried
        {
            get
            {
                try
                {
                    return GetAmmoTypeFromGunMaster(getCurrentGun).ammoMaxQuantity;
                }
                catch
                {
                    return 0;
                }
            }
        }
        private int baseEnemyDamageMultiplier
        {
            get
            {
                return 1;
            }
        }

        //AllyAttributes

        public bool isAlive { get { return AllyHealth > 0; } }
        public bool CanFire
        {
            get
            {
                try
                {
                    return getCurrentGun.GetComponent<Gun_Ammo>().currentAmmo > 0 && isCurrentItemGun;
                }
                catch
                {
                    return false;
                }
            }
        }
        public bool IsExeShootBehavior { get; set; }
        public bool IsTargettingEnemy { get; set; }
        public bool WantsFreeMovement { get; set; }
        //Gun Properties
        public float lowAmmoThreshold = 15.0f;
        public bool isCurrentItemGun
        {
            get
            {
                try
                {
                    return pInventory.CurrentHeldItem.GetComponent<Gun_Master>();
                }
                catch
                {
                    return false;
                }
            }
        }
        public Gun_Master getCurrentGun
        {
            get
            {
                try
                {
                    return pInventory.CurrentHeldItem.GetComponent<Gun_Master>();
                }
                catch
                {
                    return null;
                }
            }
        }
        public List<Gun_Master> getAllGuns
        {
            get
            {
                List<Gun_Master> guns = new List<Gun_Master>();
                if (pInventory != null)
                {
                    foreach (var item in pInventory.MyInventory)
                    {
                        if (item.GetComponent<Gun_Master>())
                        {
                            guns.Add(item.GetComponent<Gun_Master>());
                        }
                    }
                }
                return guns;
            }
        }

        public bool HasGunInInventory { get { return getAllGuns.Count > 0; } }

        public bool HasGunWithAmmo
        {
            get
            {
                foreach(var _gun in getAllGuns)
                {
                    if(_gun.GetComponent<Gun_Ammo>().currentAmmo > 0)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public bool EquipGunSucceedsProperty { get { return EquipGunWMostAmmoSucceeds(); } }
        
        //Faction Properties
        public PartyManager PartyManager
        {
            get
            {
                foreach(var pManager in GameObject.FindObjectsOfType<PartyManager>())
                {
                    if (pManager.GeneralCommander == GeneralCommander)
                        return pManager;
                }
                return null;
            }
        }
        public int FactionPlayerCount
        {
            get
            {
                try
                {
                    return gamemode.GetAllyFactionPlayerCount(this);
                }
                catch
                {
                    return 0;
                }
            }
        }
        public int GeneralPlayerCount
        {
            get
            {
                try
                {
                    return gamemode.GetAllyGeneralPlayerCount(this);
                }
                catch
                {
                    return 0;
                }
            }
        }


        //Keep track of kills and points for gamemode
        public int Kills { get { return _Kills; } set { _Kills = value; } }
        public int Points { get { return _Points; } set { _Points = value; } }
        public int Deaths { get { return _Deaths; } set { _Deaths = value; } }
        private int _Kills;
        private int _Points;
        private int _Deaths;
        //Other private variables
        private bool ExecutingShootingBehavior;
        private bool wantsFreedomToMove;
        private float freeMoveThreshold;
        private float DefaultShootDelay;

        protected virtual void OnEnable()
        {
            SetInitialReferences();
            ResetAllyStats();
            playerMaster.EventAllyMemberDeath += AllyOnDeath;
        }

        protected virtual void OnDisable()
        {
            playerMaster.EventAllyMemberDeath -= AllyOnDeath;
        }

        //Use this for initialization
        protected virtual void Start()
        {
           
        }

        // Update is called once per frame
        protected virtual void Update()
        {

        }

        public virtual int GetDamageMultiplier(ApplyDamageValues _damagevalues)
        {
            return baseEnemyDamageMultiplier;
        }

        private void AllyAttemptGunShootSwitch(bool shouldShoot)
        {
            if (CanFire && getCurrentGun != null)
            {
                if (CurrentGunAmmoCarried <= 0)
                {
                    getCurrentGun.GetComponent<Gun_StandardInput>().AttemptingGunShoot = false;
                    return;
                }
                else
                {
                    getCurrentGun.GetComponent<Gun_StandardInput>().AttemptingGunShoot = shouldShoot;
                }
            }
        }

        private void AllyAttemptGunReloadSwitch(bool shouldReload)
        {
            if (getCurrentGun != null)
            {
                getCurrentGun.GetComponent<Gun_StandardInput>().AttemptingGunReload = shouldReload;
            }
        }

        private void AllyAttemptGunBurstFireSwitch(bool shouldBurst)
        {
            if (CanFire && getCurrentGun != null)
            {
                getCurrentGun.GetComponent<Gun_StandardInput>().AttemptingActivateBurstFire = shouldBurst;
            }
        }

        public virtual void TakeDamage(ApplyDamageValues _damageValues)
        {
            if(_damageValues.mydamage > 0)
            {
                int _damage = _damageValues.mydamage * _damageValues.allyInstigator.GetDamageMultiplier(_damageValues);
                playerMaster.CallEventPlayerHealthDeduction(_damage);
                if(AllyHealth <= 0)
                {
                    playerMaster.CallEventAllyMemberDeath(this, _damageValues.allyInstigator);
                }
            }

        }

        protected void AllyOnDeath(AllyMember pendingDeath, AllyMember _instigator)
        {
            //handle visual cue for death scenario
            if(DeathSparks != null && DeathSound != null)
            {
                //Spawn death sparks and sound
                Instantiate(DeathSparks, transform.position, Quaternion.identity);
                AudioSource.PlayClipAtPoint(DeathSound, transform.position,0.5f);
            }
            //if gamemode, find allies and exclude this ally
            if(gamemode != null)
            {
                foreach (var general in gamemode.GeneralMembers)
                {
                    if(general.GeneralCommander == GeneralCommander)
                    {
                        AllyMember firstAlly = general.FindPartyMembers(true, this);
                        if(firstAlly != null)
                        {
                            general.SetAllyInCommand(firstAlly);
                        }
                        else
                        {
                            //Call Game Over Event
                            //gamemode.CallGameOverEvent(generalCommander)
                        }
                        //Found General Commander, no more searching
                        break;
                    }
                }
                //Add to death count
                Deaths += 1;
                //Check rewards for instigator
                if(_instigator != null)
                {
                    //Killed enemy, give reward
                    if (IsEnemyFor(_instigator))
                    {
                        _instigator.Points += gamemode.GetPendingReward(_instigator, RTSGameMode.ERTSRewardTypes.Reward_Kill);
                        _instigator.Kills += 1;
                    }
                    else
                    {
                        //Friendly Kill, give punishment
                        _instigator.Points += gamemode.GetPendingPunishment(_instigator, RTSGameMode.ERTSPunishmentTypes.Punishment_KilledAnAlly);
                    }
                    //Handle targetting for enemy
                    //enemyAlly->isTargetingEnemy = false;
                }
                //Update GameModeStats in the end
                gamemode.UpdateGameModeStats();
            }
            //Handle actor being destroyed
            Destroy(this.gameObject);
        }

        public RaycastHit PerformRaycastHit(Vector3 WorldLocation, Vector3 WorldDirection)
        {
            RaycastHit hit;
            Physics.Raycast(WorldLocation, WorldDirection, out hit);
            return hit;
        }

        public void StartShootingBehavior()
        {
            AllyAttemptGunBurstFireSwitch(true);
            AllyAttemptGunShootSwitch(true);
        }

        public void StopShootingBehavior()
        {
            AllyAttemptGunBurstFireSwitch(false);
            AllyAttemptGunShootSwitch(false);
        }

        public bool IsEnemyFor(AllyMember player)
        {
            return player.AllyFaction != AllyFaction;
        }

        public void ToggleFreeMovement()
        {

        }

        public void ResetAllyStats()
        {
            _Kills = 0;
            _Points = 0;
            _Deaths = 0;

        }

        public bool isGunLowOnAmmo(Gun_Master gun)
        {
            if (gun == null)
                return false;

            try
            {
                var ammoType = GetAmmoTypeFromGunMaster(gun);
                int _currentAmmo = ammoType.ammoCurrentCarried;
                int _maxAmmo = ammoType.ammoMaxQuantity;

                if ((_currentAmmo / _maxAmmo) < (lowAmmoThreshold * 0.01))
                    return true;
            }
            catch
            {
                Debug.LogError("Gun requested did not meet all required specifications.");
            }
            return false;
        }

        public Player_AmmoBox.AmmoTypes GetAmmoTypeFromGunMaster(Gun_Master gun)
        {
            try
            {
                if (gun == null)
                    return null;

                    foreach (var typeOfAmmo in GetComponent<Player_AmmoBox>().typesOfAmmunition)
                {
                    if (typeOfAmmo.ammoName == gun.GetComponent<Gun_Ammo>().ammoName)
                    {
                        return typeOfAmmo;
                    }
                }
            }
            catch
            {
                Debug.LogError("Get Ammo Type request did not work!");
            }
            return null;
        }

        public Gun_Master FindGunWMostAmmo()
        {
            List<string> gunMostNameList = new List<string>();
            try
            {
                //finds gun with the highest ammo count
                //does not factor threshold
                int lastMostAmmo = 0;
                foreach (var ammoTypes in GetComponent<Player_AmmoBox>().typesOfAmmunition)
                {
                    if(ammoTypes.ammoCurrentCarried > lastMostAmmo)
                    {
                        lastMostAmmo = ammoTypes.ammoCurrentCarried;
                        gunMostNameList.Add(ammoTypes.ammoName);
                    }
                }
                if(gunMostNameList.Count > 0 && lastMostAmmo > 0)
                {
                    //Look inside of inventory for matching guns and return
                    //the gun if a match is found
                    for (int i = gunMostNameList.Count - 1; i > 0; i--)
                    {
                        foreach(var gun in getAllGuns)
                        {
                            if(gun.GetComponent<Gun_Ammo>().ammoName == gunMostNameList[i])
                            {
                                return gun;
                            }
                        }
                    }
                }
            }
            catch
            {
                Debug.LogError("Find Gun request did not meet all required specifications.");
            }
            return null;
        }

        bool EquipGunWMostAmmoSucceeds()
        {
            var _gunWAmmo = FindGunWMostAmmo();
            if(_gunWAmmo != null)
            {
                int _gunIndex = pInventory.MyInventory.IndexOf(_gunWAmmo.transform);
                pInventory.ActivateInventoryItem(_gunIndex);
                return true;
            }
            return false;
        }

        [System.Serializable]
        public struct SwitchAllyComponents
        {
            public Behaviour MyComponent;
            public bool ShouldEnable;
        }

        [HideInInspector]
        public struct ApplyDamageValues
        {
            public AllyMember allyInstigator;
            public Transform mytransform;
            public int mydamage;
            public Vector3 hitPosition;

            public ApplyDamageValues(AllyMember _instigator, Transform _transform, int _damage, Vector3 _hitPos)
            {
                allyInstigator = _instigator;
                mytransform = _transform;
                mydamage = _damage;
                hitPosition = _hitPos;
            }
        }

        void SetInitialReferences()
        {
            playerMaster = GetComponent<Player_Master>();
            pInventory = GetComponent<Player_Inventory>();
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

    }
}