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

    private Animator _animator;

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
        if (Input.GetButton("Jump"))
        {
            _thrusterVelocity = Vector3.up * _thrusterForce;
            SetJoinSettings(0f);
        }
        else
        {
            SetJoinSettings(_jointSpring);
        }

        _motor.ApplyThruster(_thrusterVelocity);
           
        

        // Rotation de la caméra
    
    }

    private void SetJoinSettings(float jointSpring)
    {
        _joint.yDrive = new JointDrive { positionSpring = jointSpring, maximumForce = _jointMaxForce };
    } 


}
