using UnityEngine;
using Mirror;

[RequireComponent(typeof(WeaponManager))]
public class PlayerShoot : NetworkBehaviour
{
    private WeaponManager weaponManager;
    private PlayerWeapon currentWeapon;

    [SerializeField]
    private Camera _cam;

    [SerializeField]
    private LayerMask _mask;

    void Start()
    {
        if (_cam == null)
        {
            Debug.LogError("Penser à renseigner la caméra");
            this.enabled = false;
        }
        weaponManager = GetComponent<WeaponManager>();
    }
    private void Update()
    {
        currentWeapon = weaponManager.GetCurrentWeapon();

        if (currentWeapon.fireRate <= 0f)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1"))
            {
                InvokeRepeating("Shoot", 0f, 1f / currentWeapon.fireRate);
            }else if (Input.GetButtonUp("Fire1"))
            {
                CancelInvoke("Shoot");
            }
        }

    }
    [Client]
    private void Shoot()
    {
        Debug.Log("Piou");
        RaycastHit hit;

        if (Physics.Raycast(_cam.transform.position, _cam.transform.forward, out hit, currentWeapon.range, _mask))
        {
            if (hit.collider.tag == "Player")
            {
                CmdPlayerShoot(hit.collider.name, currentWeapon.damage);
            }
        }
    }
    [Command]
    private void CmdPlayerShoot(string playerId, float damage)
    {
        Debug.Log(playerId + " a été touché");

        Player player = GameManager.GetPlayer(playerId);
        player.RpcTakeDamage(damage);
    }
}
