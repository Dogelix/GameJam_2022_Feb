using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacter
{
    public int HP {get; set;}
    public Vector3 Target {get;set;}
    public void Walk();

    public void Die();
    public void Attack();
}
