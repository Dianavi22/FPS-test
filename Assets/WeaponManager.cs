using UnityEngine;
using Mirror;
public class WeaponManager : NetworkBehaviour
{
    [SerializeField] private PlayerWeapon primaryWeapon;
    private PlayerWeapon currentWeapon;
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
    void EquipWeapon(PlayerWeapon _weapon)
    {
        currentWeapon = _weapon;
        GameObject weaponIns = Instantiate(_weapon.gfx, weaponHolder.position, weaponHolder.rotation);
        weaponIns.transform.SetParent(weaponHolder);
        if (isLocalPlayer)
        {
            SetLayerRecurcively(weaponIns, LayerMask.NameToLayer(weaponLayerName));
        }
    }
 
    private void SetLayerRecurcively(GameObject obj, int newLayer)
    {
        obj.layer = newLayer;
        foreach (Transform child in obj.transform)
        {
            SetLayerRecurcively(child.gameObject, newLayer);
        }
    }
}
