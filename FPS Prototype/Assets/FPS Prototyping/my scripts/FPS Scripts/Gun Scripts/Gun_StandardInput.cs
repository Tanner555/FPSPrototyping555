using UnityEngine;
using System.Collections;

namespace S3
{
    public class Gun_StandardInput : MonoBehaviour
    {
        private Gun_Master gunMaster;
        private float nextAttack;
        public float attackRate = 0.5f;
        private Transform myTransform;
        public bool isAutomatic;
        public bool hasBurstFire;
        private bool isBurstFireActive;
        public string attackButtonName;
        public string reloadButtonName;
        public string burstFireButtonName;
        //For Ally Input Center
        public bool AttemptingGunShoot = false;
        public bool AttemptingGunReload = false;
        public bool AttemptingActivateBurstFire = false;

        // Use this for initialization
        void Start()
        {
            SetInitialReferences();
        }

        // Update is called once per frame
        void Update()
        {
            CheckIfWeaponShouldAttack();
            CheckForBurstFireToggle();
            CheckForReloadRequest();
        }

        void SetInitialReferences()
        {
            gunMaster = GetComponent<Gun_Master>();
            myTransform = transform;
            gunMaster.isGunLoaded = true;

        }

        void CheckIfWeaponShouldAttack()
        {
            if(Time.time > nextAttack && Time.timeScale > 0 && myTransform.root.CompareTag(GameManager_References._playerTag))
            {
                if(isAutomatic && !isBurstFireActive)
                {
                    if (/*Input.GetButton(attackButtonName)*/AttemptingGunShoot)
                    {
                        AttemptAttack();
                    }
                }
                else if (isAutomatic && isBurstFireActive)
                {
                    if (/*Input.GetButtonDown(attackButtonName)*/AttemptingGunShoot)
                    {
                        StartCoroutine(RunBurstFire());
                    }
                }
                else if (!isAutomatic)
                {
                    if (/*Input.GetButtonDown(attackButtonName)*/AttemptingGunShoot)
                    {
                        AttemptAttack();
                    }
                }
            }
        }

        void AttemptAttack()
        {
            nextAttack = Time.time + attackRate;
            if (gunMaster.isGunLoaded)
            {
                gunMaster.CallEventPlayerInput();
            }
            else
            {
                gunMaster.CallEventGunNotUsable();
            }
        }

        void CheckForReloadRequest()
        {
            if(/*Input.GetButtonDown(reloadButtonName)*/AttemptingGunReload && Time.timeScale > 0 && myTransform.root.CompareTag(GameManager_References._playerTag))
            {
                gunMaster.CallEventRequestReload();
                AttemptingGunReload = false;
            }
        }

        void CheckForBurstFireToggle()
        {
            if (/*Input.GetButtonDown(burstFireButtonName)*/AttemptingActivateBurstFire && Time.timeScale > 0 && myTransform.root.CompareTag(GameManager_References._playerTag))
            {
                isBurstFireActive = !isBurstFireActive;
                gunMaster.CallEventToggleBurstFire();
                AttemptingActivateBurstFire = false;
            }
        }

        IEnumerator RunBurstFire()
        {
            AttemptAttack();
            yield return new WaitForSeconds(attackRate);
            AttemptAttack();
            yield return new WaitForSeconds(attackRate);
            AttemptAttack();
        }
    }
}