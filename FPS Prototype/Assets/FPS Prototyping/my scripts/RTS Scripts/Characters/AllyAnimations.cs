using UnityEngine;
using System.Collections;
using S3;
using UnityStandardAssets.Characters.FirstPerson;

namespace RTSPrototype
{
    public class AllyAnimations : MonoBehaviour
    {
        private FirstPersonController FPSController;
        private Animator anim;

        //Capture Speed Vars
        private float nextCaptureTime;
        private float captureInterval = 0.1f;
        private float playerSpeed = 0.0f;
        private Vector3 lastPosition = new Vector3(0, 0, 0);

        public delegate void GeneralAnimHandler();
        public event GeneralAnimHandler EventSwitchCompOnCommandSwitch;

        void OnEnable()
        {
            SetInitialReferences();
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
            CapturePlayerSpeed();
            ApplySpeedToAnimation();
            
        }

        void SwitchCompsOnCommandSwitch()
        {

        }

        void ApplySpeedToAnimation()
        {
            if (anim != null)
            {
                anim.SetFloat("Speed", playerSpeed);
            }
        }

        void CapturePlayerSpeed()
        {
            if (transform == null)
                return;

            if (Time.time > nextCaptureTime)
            {
                nextCaptureTime = Time.time + captureInterval;
                playerSpeed = (transform.position - lastPosition).magnitude / captureInterval;
                lastPosition = transform.position;
                //gunMaster.CallEventSpeedCaptured(playerSpeed);
            }
        }

        void SetInitialReferences()
        {
            FPSController = GetComponent<FirstPersonController>();
            anim = GetComponentInChildren<Animator>();
            if(anim == null)
            {
                Debug.Log("Animator could not be found!");
            }
        }

    }
}