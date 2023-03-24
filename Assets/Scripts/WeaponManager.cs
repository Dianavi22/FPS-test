using UnityEngine;
using Mirror;
using System.Collections;


public class WeaponManager : NetworkBehaviour
{

    [SerializeField] private WeaponData primaryWeapon;
    private WeaponData currentWeapon;
    private WeaponGfx currentGfx;
    [SerializeField]
    private string weaponLayerName = "Weapon";
    [SerializeField] private Transform weaponHolder;

    [HideInInspector] public int currentMagazineSize;

    public bool isReloading = false;
    private void Start()
    {
        EquipWeapon(primaryWeapon); 
    }
    public WeaponData GetCurrentWeapon()
    {
        return currentWeapon;
    }
    public WeaponGfx GetCurrentGfx()
    {
        return currentGfx;
    }
    void EquipWeapon(WeaponData _weapon)
    {
        currentWeapon = _weapon;
        currentMagazineSize = _weapon.magazineSize;

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

    public IEnumerator Reload()
    {
        if (isReloading) yield break;
        isReloading = true;
        yield return new WaitForSeconds(currentWeapon.reloadTime);
        Debug.Log("Rechargééééééé");
        currentMagazineSize = currentWeapon.magazineSize; 
        isReloading = false;
    }
    
 
   
}
