using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace RTSPrototype{

    [Category("RTSPrototype")]
	public class ActionTask_FindPointNearEnemy : ActionTask{

		//protected override string OnInit(){
		//	return null;
		//}

		protected override void OnExecute() {
            if (agent.transform.GetComponent<AllyMember>())
            {
                AllyMember _ally = agent.transform.GetComponent<AllyMember>();
                GameObject _enemy = _ally.GetComponent<AllyAIController>().Enemy;
                if(_ally && _enemy)
                {
                    float SearchRadius = 20.0f;
                    Vector3 SearchOrigin = _enemy.transform.position;/* + 60.0f * (_ally.transform.position - _enemy.transform.position).normalized;*/
                    Vector3 randomPoint = SearchOrigin + Random.insideUnitSphere * SearchRadius;
                    NavMeshHit navHit;
                    if(NavMesh.SamplePosition(randomPoint, out navHit, 1.0f, NavMesh.AllAreas))
                    {
                        
                        blackboard.SetValue("Destination", randomPoint);
                    }

                }
            }

			EndAction(true);
		}

        //protected override void OnUpdate(){

        //}

        //protected override void OnStop(){

        //}

        //protected override void OnPause(){

        //}
    }
}