using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HordeManager : MonoBehaviour
{   
    [SerializeField]
    private bool _showGizmos = true;

    [SerializeField]
    private GameObject[] _zombieTypes;

    private List<Zombie> _zombies;

    private Vector3 _currentTarget;

    private Vector3 _centerOfHorde;

    public Vector3 CenterOfHorde => _centerOfHorde;

    private void Awake() 
    {
        _zombies = new List<Zombie>();
        GetZombiesInHorde();
    }

    public void MoveZombies(Vector3 moveLocation)
    {
        _currentTarget = moveLocation;
        _zombies.ForEach(e => e.Target = moveLocation);
    }

    private void GetZombiesInHorde()
    {
        _zombies = gameObject.GetComponentsInChildren<Zombie>().ToList();
    }

    public void RemoveZombie(Zombie zombie)
    {
        _zombies.Remove(zombie);
    }

    public void AddZombie(Vector3 spawnLocation)
    {
        var zombie = Instantiate(_zombieTypes[Random.Range(0, _zombieTypes.Count())], spawnLocation, Quaternion.identity, transform);
        _zombies.Add(zombie.GetComponent<Zombie>());
        zombie.GetComponent<Zombie>().Target = _currentTarget;
    }

    private void FixedUpdate() 
    {
        _centerOfHorde = VectorUtils.GetCenterPoint(_zombies.Select(e => e.transform.position).ToList());
    }

    private void OnDrawGizmos() 
    {
        if(_showGizmos)
        {
            // Draw a yellow sphere at the center of the horde
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(_centerOfHorde, 1);
        }
    }
}
