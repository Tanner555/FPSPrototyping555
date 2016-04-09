using NodeCanvas.Framework;
using ParadoxNotion.Design;
using ParadoxNotion;
using NodeCanvas;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using S3;

namespace RTSPrototype{

	[Category("RTSPrototype")]
	[Description("Returns true if Ally successfully puts gun into their inventory.")]
	public class GrabGunAttemptSucceeds : ConditionTask{

        [RequiredField]
        public BBParameter<GameObject> _weapon;

        protected override string OnInit(){
			return null;
		}

		protected override bool OnCheck(){
            if (_weapon == null)
            {
                Debug.Log("_Weapon Key is null!");
                return false;
            }

            Player_DetectItem detectItem;
            Gun_Master _gunMaster;
            AllyMember _ally;
            try {
                detectItem = agent.transform.GetComponent<Player_DetectItem>();
                _gunMaster = _weapon.value.transform.GetComponent<Gun_Master>();
                _ally = agent.transform.GetComponent<AllyMember>();
                if (detectItem == null)
                    Debug.Log("Null Comp:Detect Item");
                if(_gunMaster == null)
                    Debug.Log("Null Comp:Gun Master");
                if(_ally == null)
                    Debug.Log("Null Comp:Ally Member");
            }
            catch
            {
                Debug.Log("Error_Catch:Comps could not be set up!");
                return false;
            }
            //agent.transform.LookAt(_weapon.value.transform);
            if(detectItem != null && detectItem.enabled == true)
            {
                detectItem.CheckForItemPickupAttempt();
                if (_ally.getAllGuns.Contains(_gunMaster))
                {
                    return true;
                }
            }
            else
            {
                return false;
            }

            return false;
		}
	}
}