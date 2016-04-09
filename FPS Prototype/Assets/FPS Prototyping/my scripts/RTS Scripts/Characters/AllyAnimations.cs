using UnityEngine;
using System.Collections;
using S3;
using UnityStandardAssets.Characters.FirstPerson;

namespace RTSPrototype
{
    public class AllyAnimations : MonoBehaviour
    {
        public Transform ThirdPersonModel;
        private FirstPersonController FPSController;
        private Animator anim;
        private AllyMember _ally;

        //Capture Speed Vars
        private float nextCaptureTime;
        private float captureInterval = 0.1f;
        private float playerSpeed = 0.0f;
        private Vector3 lastPosition = new Vector3(0, 0, 0);

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
            CapturePlayerSpeed();
            ApplySpeedToAnimation();
            
        }

        void SwitchCompsOnCommandSwitch()
        {

        }

        void ApplySpeedToAnimation()
        {
            if (anim != null && anim.enabled == true)
            {
                //Much more expensive, but may be more effective
                if (_ally != null && !_ally.PartyManager.AllyIsCurrentPlayer(_ally)) { 
                anim.SetFloat("Speed", playerSpeed);
                }
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