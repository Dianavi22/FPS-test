using UnityEngine;


[RequireComponent(typeof(Rigidbody))]

public class PlayerMotor : MonoBehaviour
{
    [SerializeField]
    private Camera _cam;

    private Vector3 _velocity;
    private Vector3 _rotation;
    private float _cameraRotationX = 0f;
    private float _currentCameraRotationX = 0f;
    private Vector3 _thrusterVelocity;

    [SerializeField]
    private float _cameraRotationLimit = 85f;

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
    public void ApplyThruster(Vector3 thrusterVelocity)
    {
        _thrusterVelocity = thrusterVelocity;
    }

    public void RotateCamera(float cameraRotationX)
    {
        _cameraRotationX = cameraRotationX;
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

        if(_thrusterVelocity != Vector3.zero)
        {
            _rb.AddForce(_thrusterVelocity * Time.fixedDeltaTime, ForceMode.Acceleration);
        }
    }

    private void PerformRotation()
    {
        _rb.MoveRotation(_rb.rotation * Quaternion.Euler(_rotation));
        _currentCameraRotationX -= _cameraRotationX;
        _currentCameraRotationX = Mathf.Clamp(_currentCameraRotationX, -_cameraRotationLimit, _cameraRotationLimit);

        _cam.transform.localEulerAngles = new Vector3(_currentCameraRotationX, 0f, 0f);
    }
}
