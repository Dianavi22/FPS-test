using UnityEngine;


[RequireComponent(typeof(Rigidbody))]

public class PlayerMotor : MonoBehaviour
{
    [SerializeField]
    private Camera _cam;

    private Vector3 _velocity;
    private Vector3 _rotation;
    private Vector3 _cameraRotation;

    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    public void Move(Vector3 velocity)
    {
        _velocity = velocity;
    }
    public void Rotate(Vector3 rotation)
    {
        _rotation = rotation;
    }

    public void RotateCamera(Vector3 cameraRotation)
    {
        _cameraRotation = cameraRotation;
    }
    private void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
    }
    private void PerformMovement()
    {
        if (_velocity != Vector3.zero) {
            _rb.MovePosition(_rb.position + _velocity * Time.fixedDeltaTime);
        }
    }

    private void PerformRotation()
    {
        _rb.MoveRotation(_rb.rotation * Quaternion.Euler(_rotation));
        _cam.transform.Rotate(-_cameraRotation);
    }
}
