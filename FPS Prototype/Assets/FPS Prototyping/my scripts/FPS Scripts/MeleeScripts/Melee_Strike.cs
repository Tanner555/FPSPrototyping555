using UnityEngine;
using System.Collections;

namespace S3
{
    public class Melee_Strike : MonoBehaviour
    {
        private Melee_Master meleeMaster;
        private float nextSwingTime;
        public float damage = 25f;

        // Use this for initialization
        void Start()
        {
            SetInitialReferences();

        }

        //TODO: Send appropriate damage message for allymembers
        void OnCollisionEnter(Collision _collision)
        {
            if(_collision.gameObject != GameManager_References._player && meleeMaster.isInUse && Time.time > nextSwingTime)
            {
                nextSwingTime = Time.time + meleeMaster.swingRate;
                _collision.transform.SendMessage("ProcessDamage", damage, SendMessageOptions.DontRequireReceiver);
                meleeMaster.CallEventHit(_collision, _collision.transform);
            }
        }

        void SetInitialReferences()
        {
            meleeMaster = GetComponent<Melee_Master>();
        }
    }
}