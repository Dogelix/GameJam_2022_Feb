using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterXSeconds : MonoBehaviour
{
    [SerializeField]
    private float _delay =10.0f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, _delay);
    }

}
