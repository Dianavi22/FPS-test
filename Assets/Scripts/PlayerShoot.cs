using UnityEngine;
using Mirror;

public class PlayerShoot : NetworkBehaviour
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
        if(hit.collider.tag == "Player")
            {
                CmdPlayerShoot(hit.collider.name, weapon.damage);
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
