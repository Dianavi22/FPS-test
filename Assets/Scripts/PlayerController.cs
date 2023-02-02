using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
[RequireComponent(typeof(ConfigurableJoint))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;
    [SerializeField]
    private float _mouseSentitivityX = 3f;
    [SerializeField]
    private float _mouseSentitivityY = 3f;

    [SerializeField]
    private float _thrusterForce = 1000f;

    [SerializeField] private float thrusterFuelBurnSpeed = 1f;
    [SerializeField] private float thrusterFuelRegenSpeed = 0.3f;
    private float thrusterFuelAmount = 1f;

    private Animator _animator;

    public float GetThrusterFuelAmount()
    {
        return thrusterFuelAmount;
    }

    [Header("Joint Options")]
    [SerializeField] private float _jointSpring = 20f;
    [SerializeField] private float _jointMaxForce = 50;

    private ConfigurableJoint _joint;
    private PlayerMotor _motor;

    private void Start()
    {
        _motor = GetComponent<PlayerMotor>();
        _joint = GetComponent<ConfigurableJoint>();
        _animator = GetComponent<Animator>();
        SetJoinSettings(_jointSpring);



    }
    private void Update()
    {
        if (Cursor.lockState != CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (PauseMenu.isOn)
        {
            _motor.Move(Vector3.zero);
            _motor.Rotate(Vector3.zero);
            _motor.RotateCamera(0f);
            _motor.ApplyThruster(Vector3.zero);
            return;
        }

        if(Cursor.lockState != CursorLockMode.Locked)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        RaycastHit _hit;
        if (Physics.Raycast(transform.position, Vector3.down, out _hit, 100f))
        {
            _joint.targetPosition = new Vector3(0f, -_hit.point.y, 0f);
        }
        else
        {
            _joint.targetPosition = new Vector3(0f, 0f, 0f);

        }
        // Calculer la velocité du mouvement du joueur
        float xMov = Input.GetAxis("Horizontal");
        float zMov = Input.GetAxis("Vertical");

        Vector3 moveHozirontal = transform.right * xMov;
        Vector3 moveVertical = transform.forward * zMov;

        Vector3 _velocity = (moveHozirontal + moveVertical) * _speed;

        //jouer les animations
        _animator.SetFloat("ForwardVelocity", zMov);

        _motor.Move(_velocity);
        float yRot = Input.GetAxisRaw("Mouse X");
        Vector3 rotation = new Vector3(0, yRot, 0) * _mouseSentitivityX;

        _motor.Rotate(rotation);


        float xRot = Input.GetAxisRaw("Mouse Y");

        float cameraRotationX = xRot * _mouseSentitivityY;

        _motor.RotateCamera(cameraRotationX);

        Vector3 _thrusterVelocity = Vector3.zero;
        // Applique force JetPack
        if (Input.GetButton("Jump") && thrusterFuelAmount > 0)
        {
            thrusterFuelAmount -= thrusterFuelBurnSpeed * Time.deltaTime;
            if (thrusterFuelAmount >= 0.01f)
            {
                _thrusterVelocity = Vector3.up * _thrusterForce;
                SetJoinSettings(0f);
            }

        }
        else
        {
            thrusterFuelAmount += thrusterFuelRegenSpeed * Time.deltaTime;
            SetJoinSettings(_jointSpring);
        }

        thrusterFuelAmount = Mathf.Clamp(thrusterFuelAmount, 0f, 1f);

        _motor.ApplyThruster(_thrusterVelocity);



        // Rotation de la caméra

    }

    private void SetJoinSettings(float jointSpring)
    {
        _joint.yDrive = new JointDrive { positionSpring = jointSpring, maximumForce = _jointMaxForce };
    }


}
