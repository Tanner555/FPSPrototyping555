using NodeCanvas.Framework;
using ParadoxNotion.Design;
using ParadoxNotion;
using NodeCanvas;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using S3;
using System.Linq;

namespace RTSPrototype{

	[Category("RTSPrototype")]
	[Description("If Ally can find a weapon nearest to him, return true.")]
	public class ConditionTask_FindNearestWeaponSucceeds : ConditionTask{

        //[RequiredField]
        //public BBParameter<List<GameObject>> weapons;



        [BlackboardOnly]
        public BBParameter<Vector3> _weaponPosition;
        [BlackboardOnly]
        public BBParameter<GameObject> _weapon;

        protected override string info
        {
            get { return "Get Closer from '" + "weapons" + "' as " + _weaponPosition; }
        }

        protected override string OnInit(){
			return null;
		}

		protected override bool OnCheck(){
            var weapons = GameObject.FindObjectsOfType<Gun_Master>().ToList();

            if (weapons.Count == 0)
            {
                return false;
            }

            var closerDistance = Mathf.Infinity;
            Gun_Master closerGO = null;
            foreach (var go in weapons)
            {
                var dist = Vector3.Distance(agent.transform.position, go.transform.position);
                if (dist < closerDistance)
                {
                    closerDistance = dist;
                    closerGO = go;
                }
            }

            _weaponPosition.value = closerGO.transform.position;
            _weapon.value = closerGO.gameObject;

            return true;
		}
	}
}