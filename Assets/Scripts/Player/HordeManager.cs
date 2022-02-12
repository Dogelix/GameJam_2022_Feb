using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HordeManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _zombieTypes;

    private List<Zombie> _zombies;

    private Vector3 _currentTarget;

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
}
