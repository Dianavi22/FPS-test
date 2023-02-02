using UnityEngine;
using Mirror;


public class WeaponManager : NetworkBehaviour
{

    [SerializeField] private PlayerWeapon primaryWeapon;
    private PlayerWeapon currentWeapon;
    private WeaponGfx currentGfx;
    [SerializeField]
    private string weaponLayerName = "Weapon";
    [SerializeField] private Transform weaponHolder;
    private void Start()
    {
        EquipWeapon(primaryWeapon); 
    }
    public PlayerWeapon GetCurrentWeapon()
    {
        return currentWeapon;
    }
    public WeaponGfx GetCurrentGfx()
    {
        return currentGfx;
    }
    void EquipWeapon(PlayerWeapon _weapon)
    {
        currentWeapon = _weapon;

        GameObject weaponIns = Instantiate(_weapon.gfx, weaponHolder.position, weaponHolder.rotation);
        weaponIns.transform.SetParent(weaponHolder);
        currentGfx = weaponIns.GetComponent<WeaponGfx>();
        if(currentGfx == null)
        {
            Debug.Log("Pas de WeaponGfx sur l'arme : " + weaponIns.name);
        }
        if (isLocalPlayer)
        {
            Util.SetLayerRecurcively(weaponIns, LayerMask.NameToLayer(weaponLayerName));
        }
    }
 
   
}
