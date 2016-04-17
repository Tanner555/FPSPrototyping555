using UnityEngine;
using System.Collections;
using S3;
using ParadoxNotion;
using NodeCanvas;
using NodeCanvas.BehaviourTrees;

namespace RTSPrototype
{
    public class AllyAIController : MonoBehaviour
    {
        //Raycasting
        public Transform head;
        public LayerMask playerLayer;
        public LayerMask sightLayer;

        //Service Check Rates
        public float SearchEnemyRate = 0.5f;
        public float ShootEnemyRate = 0.5f;

        //BlackBoard Key Getters
        public Vector3 SelfPosition { get { return transform.position; } }
        public Vector3 MoveCommandLocation { get { return _moveCommandLocation; } set { _moveCommandLocation = value; } }
        public Vector3 LocationOfSound { get; set; }
        public Vector3 WanderPoint { get; set; }
        public Vector3 WanderLocation { get; set; }
        public Vector3 TargetedEnemyLocation
        {
            get
            {
                return Enemy != null ? Enemy.transform.position : new Vector3(0, 0, 0);
            }
        }
        public Vector3 Destination { get { return _destination; } set { _destination = value; } }
        public bool IsTargetingEnemy { get; set; }
        public bool MoveCommandActive { get; set; }
        public bool HasHeardSound { get; set; }
        public bool NeedAmmo{ get { return _needAmmo; } set { _needAmmo = value; } }
        public bool WantsToMove { get { return _wantsToMove; } set { _wantsToMove = value; } }
        public bool HasLosToEnemy
        {
            get
            {
                return _hasLosToEnemy;
            }
            set
            {
                _hasLosToEnemy = value;
            }
        }
        public GameObject TargetedEnemy { get; set; }
        public GameObject SelfGameObject { get; set; }
        public GameObject AllyGameObject { get; set; }
        public GameObject PatrolPoint { get; set; }
        public GameObject Enemy
        {
            get
            {
                return _enemy;
            }
            set
            {
                _enemy = value;
            }
        }

        //Blackboard private variables
        private Vector3 _moveCommandLocation = new Vector3(0,0,0);
        private Vector3 _locationOfSound = new Vector3(0, 0, 0);
        private Vector3 _wanderPoint = new Vector3(0, 0, 0);
        private Vector3 _wanderLocation = new Vector3(0, 0, 0);
        private Vector3 _targetedEnemyLocation = new Vector3(0, 0, 0);
        private Vector3 _destination = new Vector3(0, 0, 0);
        private bool _isTargetingEnemy = false;
        private bool _moveCommandActive = false;
        private bool _hasHeardSound = false;
        private bool _needAmmo = false;
        private bool _wantsToMove = false;
        private bool _hasLosToEnemy = false;
        private GameObject _targetedEnemy = null;
        private GameObject _selfGameObject = null;
        private GameObject _allyGameObject = null;
        private GameObject _patrolPoint = null;
        private GameObject _enemy = null;

        //Components
        private NodeCanvas.BehaviourTrees.BehaviourTreeOwner _behaviorTreeAgent;

        //Nav Mesh Agent
        private NavMeshAgent navMesh;
        public bool enableNavMesh
        {
            get { return _enableNavMesh; }
            set
            {
                _enableNavMesh = value;
                SwitchNavMesh();
            }
        }
        private bool _enableNavMesh = true;

        // Use this for initialization
        void Start()
        {
            navMesh = GetComponent<NavMeshAgent>();
            if (navMesh == null)
            {
                Debug.LogError("No nav mesh on ally member!");
            }

            if(head == null)
            {
                head = transform;
            }
  
            _behaviorTreeAgent = GetComponent<NodeCanvas.BehaviourTrees.BehaviourTreeOwner>();
            if(_behaviorTreeAgent == null)
            {
                Debug.LogError("No behavior tree on player!");
            }
            SwitchNavMesh();
            StartServices();
        }

        // Update is called once per frame
        void Update()
        {
           // CheckIfBehaviorTreeShouldBeDisable();
           if(Enemy != null && navMesh.enabled == true)
            {
                transform.LookAt(Enemy.transform);
            }

        }

        public void FindClosestEnemy()
        {
            Vector3 MyLoc = transform.position;
            float BestDistSq = float.MaxValue;
            AllyMember BestPawn = null;
            foreach(var TestPawn in GameObject.FindObjectsOfType<AllyMember>())
            {
                if(TestPawn && TestPawn.isAlive && TestPawn.IsEnemyFor(GetComponent<AllyMember>()))
                {
                    float DistSq = (TestPawn.transform.position - MyLoc).sqrMagnitude;
                    if(DistSq < BestDistSq)
                    {
                        BestDistSq = DistSq;
                        BestPawn = TestPawn;
                    }
                }
            }

            if (BestPawn)
            {
                Enemy = BestPawn.gameObject;
            }

        }

        public bool FindClosestEnemyWithLOS(AllyMember ExcludeEnemy)
        {
            bool bGotEnemy = false;
            Vector3 MyLoc = transform.position;
            float BestDistSq = float.MaxValue;
            AllyMember BestPawn = null;
            foreach (var TestPawn in GameObject.FindObjectsOfType<AllyMember>())
            {
                if(TestPawn && TestPawn != ExcludeEnemy && TestPawn.isAlive && TestPawn.IsEnemyFor(GetComponent<AllyMember>()))
                {
                    if(HasWeaponLOSToEnemy(TestPawn, true) == true)
                    {
                        float DistSq = (TestPawn.transform.position - MyLoc).sqrMagnitude;
                        if(DistSq < BestDistSq)
                        {
                            BestDistSq = DistSq;
                            BestPawn = TestPawn;
                        }
                    }
                }
            }

            if (BestPawn)
            {
                Enemy = BestPawn.gameObject;
                bGotEnemy = true;
            }

                return bGotEnemy;
        }

        public bool HasWeaponLOSToEnemy(AllyMember InEnemyActor,bool bAnyEnemy)
        {
            if (InEnemyActor == null)
                return true;

            bool bHasLos = false;
            //Vector3 _startLocation = transform.position;
            //StartLocation.z equals eye height
            RaycastHit _hit;
            //Vector3 _endLocation = InEnemyActor.transform.position;
            if(Physics.Linecast(head.position, InEnemyActor.transform.position, out _hit, sightLayer))
            {
                if(_hit.transform == InEnemyActor.transform)
                {
                    //Debug.Log("LOS with enemy: " + _hit.transform.name);
                    bHasLos = true;
                }else if(bAnyEnemy == true)
                {
                    // Check the team of us against the team of the actor we hit if we are able. If they dont match good to go.
                    if (_hit.transform.GetComponent<AllyMember>())
                    {
                        AllyMember hitAlly = _hit.transform.GetComponent<AllyMember>();
                        if(hitAlly.FactionPlayerCount != transform.GetComponent<AllyMember>().FactionPlayerCount)
                        {
                            bHasLos = true;
                        }
                    }
                }
            }

            return bHasLos;
        }

        void ShootEnemy()
        {
            AllyMember _myAlly = GetComponent<AllyMember>();

            bool bCanShoot = false;
            if(Enemy == null)
            {
                _myAlly.StopShootingBehavior();
                return;
            }

            AllyMember _enemyAlly = Enemy.GetComponent<AllyMember>();
            if(_enemyAlly.isAlive && _myAlly.CurrentGunAmmoCarried > 0 && _myAlly.CanFire == true)
            {
                RaycastHit _hit;
                if(Physics.Linecast(head.position, Enemy.transform.position, out _hit, sightLayer)){
                    if(_hit.transform == Enemy.transform)
                    bCanShoot = true;
                }
            }

            if (bCanShoot)
            {
                _myAlly.StartShootingBehavior();
            }
            else
            {
                _myAlly.StopShootingBehavior();
            }
            
        }

        void CheckAmmo(AllyMember _ally)
        {
            if(_ally != null)
            {
                if(_ally.getCurrentGun != null)
                {
                    NeedAmmo = _ally.isGunLowOnAmmo(_ally.getCurrentGun);
                }
            }
        }

        //Services
        void AllySearchEnemy()
        {
            //bool LosWEnemy = false;
            //if(Enemy == null && Enemy.GetComponent<AllyMember>())
            //{
            //    FindClosestEnemyWithLOS(Enemy.GetComponent<AllyMember>());
            //}
            //else
            //{
            //    FindClosestEnemyWithLOS(null);
            //}
            if (!FindClosestEnemyWithLOS(null))
            {
                FindClosestEnemy();
            }
        }

        void AllyShootEnemy()
        {
            if (!WantsToMove)
            {
                ShootEnemy();
            }
        }

        void CheckIfBehaviorTreeShouldBeDisable()
        {
            if(Enemy == null)
            {
                _behaviorTreeAgent.StopBehaviour();
            }else if (!_behaviorTreeAgent.isRunning)
            {
                _behaviorTreeAgent.StartBehaviour();
            }
            
        }

        void StartServices()
        {
            InvokeRepeating("AllySearchEnemy",0.1f,SearchEnemyRate);
            InvokeRepeating("AllyShootEnemy", 0.1f, ShootEnemyRate);
            //InvokeRepeating("CheckIfBehaviorTreeShouldBeDisable", 0.1f, 0.1f);

        }

        void OnDisable()
        {
            DisableServices();
        }

        void DisableServices()
        {
            CancelInvoke();
        }

        void SwitchNavMesh()
        {
            navMesh.enabled = _enableNavMesh;
        }
    }
}
