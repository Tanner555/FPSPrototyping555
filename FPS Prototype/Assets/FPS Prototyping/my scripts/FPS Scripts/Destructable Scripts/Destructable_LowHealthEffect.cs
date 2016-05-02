using UnityEngine;
using System.Collections;

namespace S3
{
    public class Destructable_LowHealthEffect : MonoBehaviour
    {
        private Destructable_Master destructibleMaster;
        public GameObject[] lowHealthEffectGO;

        void OnEnable()
        {
            SetInitialReferences();
            destructibleMaster.EventHealthLow += TurnOnLowHealthEffect;
        }

        void OnDisable()
        {
            destructibleMaster.EventHealthLow -= TurnOnLowHealthEffect;
        }

        void SetInitialReferences()
        {
            destructibleMaster = GetComponent<Destructable_Master>();
        }

        void TurnOnLowHealthEffect()
        {
            if(lowHealthEffectGO.Length > 0)
            {
                for (int i = 0; i < lowHealthEffectGO.Length; i++)
                {
                    lowHealthEffectGO[i].SetActive(true);
                }
            }
        }
    }
}