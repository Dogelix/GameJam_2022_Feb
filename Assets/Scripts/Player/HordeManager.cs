using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HordeManager : MonoBehaviour
{
    private List<Zombie> _zombies;

    private void Awake() 
    {
        _zombies = new List<Zombie>();
        GetZombiesInHorde();
    }

    public void MoveZombies(Vector3 moveLocation)
    {
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

    
}
