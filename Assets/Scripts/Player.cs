using System.Collections;
using Mirror;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [SerializeField] private float _maxHealth = 100f;
    [SyncVar] private float _currentHealth;

    private void Awake()
    {
        SetDefaults();
    }

    public void SetDefaults()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(float amount)
    {
        _currentHealth -= amount;
    }

}
