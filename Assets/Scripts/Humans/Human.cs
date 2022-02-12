using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Human : MonoBehaviour, ICharacter
{   
    private bool _dead = false;
    private IHuman _iHuman;
    private HordeManager _hordeManager;
    private bool _isMoving = false;
    private int _hp = 5;
    private Vector3 _target;
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;

    public int HP { get => _hp; set => _hp = value; }
    public Vector3 Target { get => _target; set { _target = value; } }

    private void Awake() 
    {
        _hordeManager = GameObject.FindGameObjectWithTag("HordeManager").GetComponent<HordeManager>();
        _animator = gameObject.GetComponent<Animator>(); 
        _navMeshAgent = gameObject.GetComponent<NavMeshAgent>();   
    }

    public void Die()
    {
        int randomDeathId = Random.Range(1, 3);

        _animator.SetInteger("Death.DeathType_int", randomDeathId);

        if(!_dead)
        {
            _dead = true;
            _hordeManager.AddZombie(transform.position);
        }

        Destroy(gameObject);
    }

    public void Attack(Transform target)
    {
        //throw new System.NotImplementedException();
    }


    public void Walk()
    {
        if(Vector3.Distance(transform.position, _target) > 1)
        {
            _navMeshAgent.SetDestination(_target);
            _animator.SetBool("Static_b", false);
            _animator.SetFloat("Speed_f", 0.45f);
        }
        else
        {
            _animator.SetBool("Static_b", true);
            _animator.SetFloat("Speed_f", 0f);
        }
    }

    private void FixedUpdate() 
    {
        Walk();
        if(_hp <= 0) Die();
    }

    public void Hit(int damage)
    {
        _hp -= damage;
    }
}
