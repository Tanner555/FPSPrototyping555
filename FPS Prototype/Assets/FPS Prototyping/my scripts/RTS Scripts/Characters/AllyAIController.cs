using UnityEngine;
using System.Collections;

public class AllyAIController : MonoBehaviour {

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
    private bool _enableNavMesh = false;

	// Use this for initialization
	void Start () {
        navMesh = GetComponent<NavMeshAgent>();
        if(navMesh == null)
        {
            Debug.LogError("No nav mesh on ally member!");
        }

        SwitchNavMesh();
	}
	
	// Update is called once per frame
	void Update () {

	}

    void SwitchNavMesh()
    {
        navMesh.enabled = _enableNavMesh;
    }
}
