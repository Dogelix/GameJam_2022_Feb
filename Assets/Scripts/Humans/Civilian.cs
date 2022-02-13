using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Civilian : MonoBehaviour, IHuman
{
    [SerializeField]
    private bool _showGizmos = true;

    [SerializeField]
    private float _overlapSphereRadius = 5f;

    [SerializeField]
    private bool _alerted = false;

    public float multiplyBy;

    private HordeManager _hordeManager;
    private Human _human;

    private Transform _startPosition;
    
    private void Awake() 
    {
        _hordeManager = GameObject.FindGameObjectWithTag("HordeManager").GetComponent<HordeManager>();
        _human = gameObject.GetComponent<Human>();
    }

    public void Action(Transform target)
    {
        // /throw new System.NotImplementedException();
    }

    private void FixedUpdate() 
    {
        if(_alerted)
        {
            GetComponent<WanderingAI>().enabled = false;
            _human._isMoving = true;
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, _overlapSphereRadius);
            if(hitColliders.Select(e => e.gameObject).Any(s => s.GetComponent<Zombie>() != null))
            {
                Debug.Log(transform.name + " has seen the horde");
                RunFrom();
            }
            else
            {
                _alerted = false;
                _human._isMoving = false;
            }
        }
        else
        {
            GetComponent<WanderingAI>().enabled = true;
        }
    }

    public void RunFrom()
    {

        // store the starting transform
        _startPosition = transform;
        
        //temporarily point the object to look away from the player
        transform.rotation = Quaternion.LookRotation(transform.position - _hordeManager.CenterOfHorde);

        //Then we'll get the position on that rotation that's multiplyBy down the path (you could set a Random.range
        // for this if you want variable results) and store it in a new Vector3 called runTo
        Vector3 runTo = transform.position + transform.forward * multiplyBy;
        //Debug.Log("runTo = " + runTo);
        
        //So now we've got a Vector3 to run to and we can transfer that to a location on the NavMesh with samplePosition.
        
        NavMeshHit hit;    // stores the output in a variable called hit

        // 5 is the distance to check, assumes you use default for the NavMesh Layer name
        NavMesh.SamplePosition(runTo, out hit, 5, 1 << NavMesh.GetNavMeshLayerFromName("Walkable")); 
        Debug.Log("hit = " + hit + " hit.position = " + hit.position);

        // reset the transform back to our start transform
        transform.position = _startPosition.position;
        transform.rotation = _startPosition.rotation;

        // And get it to head towards the found NavMesh position
        _human.Target = hit.position;
    }

    private void OnDrawGizmos() 
    {
        if(_showGizmos)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _overlapSphereRadius);
        }    
    }
}
