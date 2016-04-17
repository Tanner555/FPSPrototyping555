using UnityEngine;
using System.Collections;
using S3;
using UnityStandardAssets.Characters.FirstPerson;
using System;

namespace RTSPrototype
{
    public class AllyAnimations : MonoBehaviour
    {
        public Transform ThirdPersonModel;
        public Transform FindLookTransform;
        private FirstPersonController FPSController;
        private Animator anim;
        private AllyMember _ally;

        //Capture Speed Vars
        private float nextCaptureTime = 0.2f;
        private float captureInterval = 0.1f;
        private float nextApplyTime = 0.0f;
        private float applyInterval = 0.2f;
        private float playerSpeed = 0.0f;
        private float playerTurning = 0.0f;
        private Vector3 lastPosition = new Vector3(0, 0, 0);
        private Vector3 lastRotation = new Vector3(0,0,0);

        public delegate void GeneralAnimHandler();
        public event GeneralAnimHandler EventSwitchCompOnCommandSwitch;

        void OnEnable()
        {
            Invoke("SetInitialReferences", 0.1f);
            EventSwitchCompOnCommandSwitch += SwitchCompsOnCommandSwitch;
        }

        void OnDisable()
        {
            EventSwitchCompOnCommandSwitch -= SwitchCompsOnCommandSwitch;
        }

        // Use this for initialization
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            CapturePlayerSpeedAndTurn();
            ApplySpeedAndTurnToAnimation();
            
        }

        void SwitchCompsOnCommandSwitch()
        {
            
        }

       void ApplySpeedAndTurnToAnimation()
        {
            if (Time.time > nextApplyTime)
            {
                nextApplyTime = Time.time + applyInterval;
                if (anim != null && anim.enabled == true)
                {
                    //Much more expensive, but may be more effective
                    if (_ally != null && !_ally.PartyManager.AllyIsCurrentPlayer(_ally))
                    {
                        anim.SetFloat("Speed", playerSpeed);
                        anim.SetFloat("Turning", playerTurning);
                    }
                }
            }

        }


        void CapturePlayerSpeedAndTurn()
        {
            if (transform == null)
                return;

            if (Time.time > nextCaptureTime)
            {
                //Capture Speed
                nextCaptureTime = Time.time + captureInterval;
                //Find Look will look at the current position from the last given pos.
                if (FindLookTransform != null)
                {
                    //Capture Turning
                    FindLookTransform.position = lastPosition;
                    FindLookTransform.LookAt(transform.position);
                    playerTurning = FindLookTransform.rotation.eulerAngles.y;
                }
                playerSpeed = Mathf.Clamp((transform.position - lastPosition).magnitude / captureInterval, 0, 10);
                lastPosition = transform.position;
                //Capture Turning
                //playerTurning = Mathf.Clamp((transform.rotation.eulerAngles.y - lastRotation.y) / captureInterval, -1, 1);
                //lastRotation = transform.rotation.eulerAngles;

                //gunMaster.CallEventSpeedCaptured(playerSpeed);
            }
        }

        void SetInitialReferences()
        {
            try {
                _ally = GetComponent<AllyMember>();
                if (_ally != null && !_ally.PartyManager.AllyIsCurrentPlayer(_ally))
                {
                    FPSController = GetComponent<FirstPersonController>();
                    if (ThirdPersonModel != null)
                        anim = ThirdPersonModel.GetComponent<Animator>();

                    if (anim == null)
                    {
                        Debug.Log("Animator could not be found!");
                    }
                }
            }
            catch
            {
                Debug.LogError("Party Manager could not be set!");
            }
        }

    }
}