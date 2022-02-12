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

    private Camera _playerCamera;
    private Vector3 _target;
    private HordeManager _hordeManager;

    private void Awake() 
    {
        _playerActions = new PlayerActions();

        _rigidbody = gameObject.GetComponent<Rigidbody>();

        _playerCamera = gameObject.GetComponentInChildren<Camera>();

        if(_rigidbody == null)
            Debug.LogError($"{nameof(_rigidbody)} is null");

        _playerActions.Player_Map.RMBClick.performed += ctx => RMBClicked();   
        _playerActions.Player_Map.LMBClick.performed += ctx => LMBClicked(); 

        _hordeManager = GameObject.FindGameObjectWithTag("HordeManager").GetComponent<HordeManager>();
    }

    private void OnEnable() 
    {
        _playerActions.Player_Map.Enable();
    }

    private void OnDisable() 
    {
        _playerActions.Player_Map.Disable();
    }


    public Vector3 MoveInput => _moveInput;

    private void FixedUpdate() 
    {
        var temp  = _playerActions.Player_Map.Movement.ReadValue<Vector2>();
        _moveInput = new Vector3(temp.x, 0, temp.y);

        _rigidbody.velocity = _moveInput * _playerSpeed;
    }

    private void LMBClicked()
    {
        //_hordeManager.AddZombie(Vector3.zero);
    }

    private void RMBClicked()
    {
        Plane plane = new Plane(Vector3.up, 0);
        float distance;
#if ENABLE_INPUT_SYSTEM
        Vector3 mousePosition = _playerActions.Player_Map.MousePosition.ReadValue<Vector2>();
#else   
        Vector3 mousePosition = Input.mousePosition;
#endif

        mousePosition.z = 20;
        Ray ray = _playerCamera.ScreenPointToRay(mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(_playerCamera.ScreenPointToRay(mousePosition), out hit, 100)) {
            _target = hit.point;
        }

        Destroy(GameObject.FindGameObjectWithTag("Indicator"));

        Instantiate(Resources.Load("ClickIndicator") as GameObject, _target, Quaternion.identity);
        _hordeManager.MoveZombies(_target);
    }


//     public Vector3 worldPosition;
// void Update()
// {
//     Plane plane = new Plane(Vector3.up, 0);
//     float distance;
//     Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//     if (plane.Raycast(ray, out distance))
//     {
//         worldPosition = ray.GetPoint(distance);
//     }
// }
}
