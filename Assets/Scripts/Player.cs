using System.Collections;
using Mirror;
using UnityEngine;


[RequireComponent(typeof(PlayerSetUp))]
public class Player : NetworkBehaviour
{
    [SyncVar]
    private bool _isDead = false;
    public bool IsDead
    {
        get { return _isDead; }
        protected set { _isDead = value; }
    }

    [SerializeField] private GameObject deathEffect;
    [SerializeField] private GameObject spawnEffect;

    [SerializeField] private float _maxHealth = 100f;
    [SyncVar] private float _currentHealth;
    [SerializeField] private Behaviour[] _disableOnDeath;

    [SerializeField] private GameObject[] _disableGameObjectOnDeath;
    private bool[] _wasEnableOnStart;

    public void SetUp()
    {
        _wasEnableOnStart = new bool[_disableOnDeath.Length];
        for (int i = 0; i < _disableOnDeath.Length; i++)
        {
            _wasEnableOnStart[i] = _disableOnDeath[i].enabled;
        }
        SetDefaults();
    }

    public void SetDefaults()
    {
        _isDead = false;
        _currentHealth = _maxHealth;

        for (int i = 0; i < _disableOnDeath.Length; i++)
        {
            _disableOnDeath[i].enabled = _wasEnableOnStart[i];
        }
        for (int i = 0; i < _disableGameObjectOnDeath.Length; i++)
        {
            _disableGameObjectOnDeath[i].SetActive(true);
        }

        Collider col = GetComponent<Collider>();
        if(col != null)
        {
            col.enabled = true;
        }
        if (isLocalPlayer) { 
        GameManager.instance.SetSceneCameraActive(false);
        GetComponent<PlayerSetUp>()._playerUIInstance.SetActive(true);
        }

        GameObject _gfxIns = Instantiate(spawnEffect, transform.position, Quaternion.identity);
        Destroy(_gfxIns, 3f);

    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(GameManager.instance.matchSettings.respawnTimer);
        Transform spawnPoint = NetworkManager.singleton.GetStartPosition();
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;

        SetDefaults();

    }

    private void Update()
    {
        if (!isLocalPlayer) return;
        if(Input.GetKeyDown(KeyCode.H))
        {
            RpcTakeDamage(50);
        }
    }

    [ClientRpc]
    public void RpcTakeDamage(float amount)
    {
        if (_isDead)
        {
            return;
        }
        _currentHealth -= amount;
        if(_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        _isDead = true;    
        for (int i = 0; i < _disableOnDeath.Length; i++)
        {
            _disableOnDeath[i].enabled = false;
        }
        for (int i = 0; i < _disableGameObjectOnDeath.Length; i++)
        {
            _disableGameObjectOnDeath[i].SetActive(false);
        }
        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            col.enabled = false;
        }
       GameObject _gfxIns = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(_gfxIns, 3f);
        if (isLocalPlayer) GameManager.instance.SetSceneCameraActive(true);
        GetComponent<PlayerSetUp>()._playerUIInstance.SetActive(false);
        StartCoroutine(Respawn());
    }

}
