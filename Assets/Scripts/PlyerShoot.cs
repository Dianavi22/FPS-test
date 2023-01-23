using UnityEngine;
using Mirror;

public class PlyerShoot : NetworkBehaviour
{
    public PlayerWeapon weapon;

    [SerializeField]
    private Camera _cam;

    [SerializeField]
    private LayerMask _mask;

    void Start()
    {
        if(_cam == null)
        {
            Debug.LogError("Penser à renseigner la caméra");
            this.enabled = false;
        }
    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        RaycastHit hit;

        if (Physics.Raycast(_cam.transform.position, _cam.transform.forward, out hit, weapon.range, _mask))
        {
            Debug.Log("Touched" + hit.collider.name);
        }
    }
}
