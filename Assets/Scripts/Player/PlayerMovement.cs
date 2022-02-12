using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _playerSpeed = 10.0f;
    private PlayerActions _playerActions;
    private Rigidbody _rigidbody;
    private Vector3 _moveInput;

    private void Awake() 
    {
        _playerActions = new PlayerActions();

        _rigidbody = gameObject.GetComponent<Rigidbody>();

        if(_rigidbody == null)
            Debug.LogError($"{nameof(_rigidbody)} is null");

    }

    private void OnEnable() 
    {
        _playerActions.Player_Map.Enable();     
    }

    private void OnDisable() 
    {
        _playerActions.Player_Map.Disable();
    }

    private void FixedUpdate() 
    {
        var temp  = _playerActions.Player_Map.Movement.ReadValue<Vector2>();
        _moveInput = new Vector3(temp.x, 0, temp.y);

        _rigidbody.velocity = _moveInput * _playerSpeed;
    }
}
