using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour, ICharacter
{
    private GameObject _absoluteParent;
    private bool _isMoving = false;
    private int _hp = 5;
    private Vector3 _target;
    private NavMeshAgent _navMeshAgent;

    public bool Moving
    {   
        set
        {
            _isMoving = value;
        }
    }

    public int HP { get => _hp; set => _hp = value; }
    public Vector3 Target { get => _target; set { _target = value; } }

    private Animator _animator;

    public void Attack()
    {
        throw new System.NotImplementedException();
    }

    public void Die()
    {
        int randomDeathId = Random.Range(1, 3);

        if(randomDeathId == 1){
            _animator.SetTrigger("Death1");
        }
        else{
            _animator.SetTrigger("Death2");
        }

        gameObject.transform.parent.GetComponent<HordeManager>().RemoveZombie(this);
    }


    private void Awake() 
    {
        _absoluteParent = GameObjectFinderUtils.FindParentWithTag(gameObject, "Player");
        _animator = gameObject.GetComponent<Animator>(); 
        _navMeshAgent = gameObject.GetComponent<NavMeshAgent>();   
    }

    private void FixedUpdate() 
    {
        Walk();
        if(_hp <= 0) Die();
    }

    public void Walk()
    {
        if(Vector3.Distance(transform.position, _target) > 1)
        {
            _navMeshAgent.destination = _target;
            _animator.SetBool("Walking", true);
        }
        else
        {
            _animator.SetBool("Walking", false);
        }
    }
}
