using UnityEngine;
using System.Collections;
using RTSPrototype;
using System.Linq;

namespace S3
{
    public class Player_DetectItem : MonoBehaviour
    {
        [Tooltip("What layer is being used for items.")]
        public LayerMask layerToDetect;
        [Tooltip("What transform will the ray be fired from?")]
        public Transform rayTransformPivot;
        [Tooltip("The editor input button that will be used for picking up items")]
        public string buttonPickup;
        private AllyMember _ally;

        private Transform itemAvailableForPickup;
        private RaycastHit hit;
        private float detectRange = 3.0f;//3;
        private float detectRadius = 0.7f;//0.7f;
        private bool itemInRange;

        private float labelWidth = 200;
        private float labelHeight = 50;

        //Service vars
        private float checkRate = 1.0f;
        private float nextCheck = 0.0f;
        private float overlapRadius = 4.0f;

        void Start()
        {
            _ally = GetComponent<AllyMember>();
            if(_ally == null)
            {
                Debug.Log("No allymember!");
            }
        }

        void Update()
        {
            //Cast usual ray if current player, else use overlap sphere.
            if (_ally.PartyManager.AllyIsCurrentPlayer(_ally))
            {
                CastRayForDetectingItems();
            }
            else
            {
                ServiceOverlapSphere();
            }
            //CheckForItemPickupAttempt();
        }

        public void CheckForItemPickupAttempt()
        {
            if (/*Input.GetButtonDown(buttonPickup) &&*/ Time.timeScale > 0 && itemInRange && itemAvailableForPickup.root.tag != GameManager_References._playerTag)
            {
                itemAvailableForPickup.GetComponent<Item_Master>().CallEventPickupAction(rayTransformPivot);

            }
        }

        void ServiceOverlapSphere()
        {
            if (Time.time > nextCheck)
            {
                nextCheck = Time.time + checkRate;
                CastOverlapSphereForDetectingItems();
            }
        }

        void CastOverlapSphereForDetectingItems()
        {
            Transform closerTrans = null;
            var closerDistance = Mathf.Infinity;

            var _colliders = Physics.OverlapSphere(transform.position, overlapRadius, layerToDetect).ToList();

            if (_colliders.Count > 0)
            {
                foreach (var _col in _colliders)
                {
                    var dist = Vector3.Distance(transform.position, _col.transform.position);
                    if (dist < closerDistance)
                    {
                        closerDistance = dist;
                        closerTrans = _col.transform;
                    }
                }
                if (closerTrans != null)
                {
                    itemAvailableForPickup = closerTrans;
                    itemInRange = true;
                }
            }
            else
            {
                itemInRange = false;
            }

        }

        void CastRayForDetectingItems()
        {
            if (Physics.SphereCast(rayTransformPivot.position, detectRadius, rayTransformPivot.forward, out hit, detectRange, layerToDetect))
            {
                itemAvailableForPickup = hit.transform;
                itemInRange = true;
            }
            else
            {
                itemInRange = false;
            }
        }

        void OnGUI()
        {
            //If current player, then use GUI method
            if (_ally.PartyManager.AllyIsCurrentPlayer(_ally))
            {
                if (itemInRange && itemAvailableForPickup != null)
                {
                    GUI.Label(new Rect(Screen.width / 2 - labelWidth / 2, Screen.height / 2, labelWidth, labelHeight), itemAvailableForPickup.name);
                }
            }
        }
            
    }
}
