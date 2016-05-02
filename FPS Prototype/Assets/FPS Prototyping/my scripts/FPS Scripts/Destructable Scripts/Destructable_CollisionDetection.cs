using UnityEngine;
using System.Collections;

namespace S3
{
    public class Destructable_CollisionDetection : MonoBehaviour
    {
        private Destructable_Master destructibleMaster;
        private Rigidbody myRigidbody;
        public float thresholdMass = 50;
        public float thresholdSpeed = 6;
        
        // Use this for initialization
        void Start()
        {
            SetInitialReferences();
        }

        void OnCollisionEnter(Collision col)
        {
            if(col.contacts.Length > 0)
            {
                if(col.contacts[0].otherCollider.GetComponent<Rigidbody>() != null)
                {
                    CollisionCheck(col.contacts[0].otherCollider.GetComponent<Rigidbody>());
                }
                else
                {
                    SelfSpeedCheck();
                }
            }
        }

        void SetInitialReferences()
        {
            destructibleMaster = GetComponent<Destructable_Master>();
            if(GetComponent<Rigidbody>() != null)
            {
                myRigidbody = GetComponent<Rigidbody>();
            }
        }

        void CollisionCheck(Rigidbody otherRigidbody)
        {
            if(otherRigidbody.mass > thresholdMass && otherRigidbody.velocity.sqrMagnitude > (thresholdSpeed * thresholdSpeed))
            {
                int _damage = (int)otherRigidbody.mass;
                destructibleMaster.CallEventDeductHealth(_damage);
            }
            else
            {
                SelfSpeedCheck();
            }
        }

        void SelfSpeedCheck()
        {
            if(myRigidbody.velocity.sqrMagnitude > (thresholdSpeed * thresholdSpeed))
            {
                int _damage = (int)myRigidbody.mass;
                destructibleMaster.CallEventDeductHealth(_damage);
            }
        }
    }
}