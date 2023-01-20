using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;
    [SerializeField]
    private float _mouseSentitivityX = 3f;
    [SerializeField]
    private float _mouseSentitivityY = 3f;

    private PlayerMotor _motor;

    private void Start()
    {
        _motor = GetComponent<PlayerMotor>();

        
     }
    private void Update()
    {
        // Calculer la velocité du mouvement du joueur
        float xMov = Input.GetAxisRaw("Horizontal");
        float zMov = Input.GetAxisRaw("Vertical");

        Vector3 moveHozirontal = transform.right * xMov;
        Vector3 moveVertical = transform.forward * zMov;

        Vector3 _velocity = (moveHozirontal + moveVertical).normalized * _speed;

        _motor.Move(_velocity);
        float yRot = Input.GetAxisRaw("Mouse X");
        Vector3 rotation = new Vector3(0, yRot, 0) * _mouseSentitivityX;

        _motor.Rotate(rotation);


        // Rotation de la caméra
        float xRot = Input.GetAxisRaw("Mouse Y");
        Vector3 cameraRotation = new Vector3(xRot, 0, 0) * _mouseSentitivityY;

        _motor.RotateCamera(cameraRotation);
    }


}
