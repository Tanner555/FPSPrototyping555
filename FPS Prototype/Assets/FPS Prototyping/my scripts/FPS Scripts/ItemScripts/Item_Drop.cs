using UnityEngine;
using System.Collections;

namespace S3
{
    public class Item_Drop : MonoBehaviour
    {
        private Item_Master itemMaster;
        public string dropButtonName;
        private Transform myTransform;
        private bool shouldDrop = false;

        // Use this for initialization
        void Start()
        {
            SetInitialReferences();
        }

        // Update is called once per frame
        void Update()
        {
            CheckForDropInput();
        }

        void SetInitialReferences()
        {
            itemMaster = GetComponent<Item_Master>();
            myTransform = transform;
        }

        void CheckForDropInput()
        {
            if(shouldDrop && Time.timeScale > 0 && myTransform.root.CompareTag(GameManager_References._playerTag))
            {
                shouldDrop = false;
                myTransform.parent = null;
                itemMaster.CallEventObjectThrow();
            }
        }

        public void TimeToDrop()
        {
            shouldDrop = true;
        }
    }
}