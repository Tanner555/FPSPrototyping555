using UnityEngine;
using System.Collections;

namespace RTSPrototype
{
    public class PartyInputCenter : MonoBehaviour
    {
        private PartyManager _partyManager;

        // Use this for initialization
        void Start()
        {
            _partyManager = GetComponent<PartyManager>();
            if(_partyManager == null)
            {
                Debug.Log("No partymanager attached to general!");
            }
        }

        // Update is called once per frame
        void Update()
        {
            PartyInputSetup();
        }

        #region PartyInputSetup
        private void PartyInputSetup()
        {
            try
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    _partyManager.PossessAllySubtract();
                }
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    _partyManager.PossessAllyAdd();
                }
            }
            catch
            {
                Debug.LogError("No partymanager could be found!");
            }
        }
        #endregion
    }
}