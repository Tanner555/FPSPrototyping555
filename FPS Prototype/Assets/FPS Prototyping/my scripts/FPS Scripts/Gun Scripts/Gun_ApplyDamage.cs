using UnityEngine;
using System.Collections;
using RTSPrototype;

namespace S3
{
    public class Gun_ApplyDamage : MonoBehaviour
    {
        private Gun_Master gunMaster;
        public int damage = 10;

        void OnEnable()
        {
            SetInitialReferences();
            gunMaster.EventShotEnemy += ApplyDamage;
            gunMaster.EventShotDefault += ApplyDamage;
            gunMaster.EventShotAllyMember += ApplyDamageToAlly;
        }
        void OnDisable()
        {
            gunMaster.EventShotEnemy -= ApplyDamage;
            gunMaster.EventShotDefault -= ApplyDamage;
            gunMaster.EventShotAllyMember -= ApplyDamageToAlly;
        }
        void SetInitialReferences()
        {
            gunMaster = GetComponent<Gun_Master>();
        }
        void ApplyDamage(Vector3 hitPosition, Transform hitTransform)
        {
            hitTransform.SendMessage("ProcessDamage",damage,SendMessageOptions.DontRequireReceiver);
        }

        void ApplyDamageToAlly(Vector3 hitPosition, Transform hitTransform)
        {
            AllyMember.ApplyDamageValues dValues = new AllyMember.ApplyDamageValues(transform.root.GetComponent<AllyMember>(), transform.root, damage, hitPosition);
            hitTransform.SendMessage("TakeDamage", dValues, SendMessageOptions.DontRequireReceiver);
        }

    }
}