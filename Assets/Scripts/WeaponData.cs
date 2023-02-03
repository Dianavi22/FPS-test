using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "My Game/Weapon Data")]
public class WeaponData : ScriptableObject
{
    public string name = "Fusil semi-automatique";
    public float damage = 10;
    public float range = 100f;

    public float fireRate = 0f;
    public GameObject gfx;
}
