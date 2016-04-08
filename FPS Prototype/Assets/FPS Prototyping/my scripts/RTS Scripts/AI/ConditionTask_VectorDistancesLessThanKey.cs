using NodeCanvas.Framework;
using ParadoxNotion.Design;
using ParadoxNotion;
using UnityEngine;

namespace RTSPrototype{

	[Category("RTSPrototype")]
	[Description("See if Distance of a vector is less than another vector.")]
	public class ConditionTask_VectorDistancesLessThanKey : ConditionTask{

        [RequiredField]
        public BBParameter<Vector3> FromPosition = new Vector3(0, 0, 0);
        [RequiredField]
        public BBParameter<Vector3> ToPosition = new Vector3(0,0,0);
        [RequiredField]
        public BBParameter<float> distance = 1;
        public CompareMethod checkType = CompareMethod.LessThan;

        [SliderField(0, 0.1f)]
        public float floatingPoint = 0.05f;

        protected override string info
        {
            get { return "Distance" + OperationTools.GetCompareString(checkType) + distance + " from " + FromPosition + " to " + ToPosition; }
        }

        protected override string OnInit(){
			return null;
		}

		protected override bool OnCheck(){
            return OperationTools.Compare(Vector3.Distance(FromPosition.value, ToPosition.value), distance.value, checkType, floatingPoint);
        }

        public override void OnDrawGizmosSelected()
        {
            if (agent != null)
            {
                Gizmos.DrawWireSphere(FromPosition.value, distance.value);
            }
        }
    }
}