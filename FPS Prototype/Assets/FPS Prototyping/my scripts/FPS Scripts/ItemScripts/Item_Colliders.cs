﻿using UnityEngine;
using System.Collections;

namespace S3
{
    public class Item_Colliders : MonoBehaviour
    {
        private Item_Master itemMaster;
        public Collider[] colliders;
        public PhysicMaterial myPhysicMaterial;

        void OnEnable()
        {
            SetInitialReferences();
            itemMaster.EventObjectThrow += EnableColliders;
            itemMaster.EventObjectPickup += DisableColliders;
            Invoke("CheckIfStartsInInventory", 0.25f);
        }

        void OnDisable()
        {
            itemMaster.EventObjectThrow -= EnableColliders;
            itemMaster.EventObjectPickup -= DisableColliders;
        }

        //void Start()
        //{
        //    CheckIfStartsInInventory();
        //}

        void SetInitialReferences()
        {
            itemMaster = GetComponent<Item_Master>();
        }

        void CheckIfStartsInInventory()
        {
            if (transform.root.CompareTag(GameManager_References._playerTag))
            {
                DisableColliders();
            }
        }

        void EnableColliders()
        {
            if(colliders.Length > 0)
            {
                foreach(Collider col in colliders)
                {
                    col.enabled = true;

                    if(myPhysicMaterial != null)
                    {
                        col.material = myPhysicMaterial;
                    }
                }
            }
        }

        void DisableColliders()
        {
            if (colliders.Length > 0)
            {
                foreach (Collider col in colliders)
                {
                    col.enabled = false;
                }
            }
        }
    }
}