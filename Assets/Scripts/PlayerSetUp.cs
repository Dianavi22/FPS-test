using Mirror;
using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerController))]
public class PlayerSetUp : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] componentsToDisable;

    [SerializeField]
    private string _remoteLayerName = "RemotePlayer";

    [SerializeField] private string _dontDrawLayerName = "DontDraw";
    [SerializeField] private GameObject _playerGraphics;

    [SerializeField] private GameObject _playerUIPrefab;
    
    [HideInInspector]
    public GameObject _playerUIInstance;

    
    private void Start()
    {
        if (!isLocalPlayer)
        {
            AssignRemoteLayer();
            DisableComponents();
        }
        else
        {
            //désactiver la partie graphique du joueur local
            Util.SetLayerRecurcively(_playerGraphics, LayerMask.NameToLayer(_dontDrawLayerName));

            //création du UI du joueur local
            _playerUIInstance = Instantiate(_playerUIPrefab);

            //configuration UI
            PlayerUI ui = _playerUIInstance.GetComponent<PlayerUI>();
            if(ui == null) 
            {
                Debug.LogError("missing component PlayerUI on PlayerUIInstance");
            }
            else
            {
                ui.SetController(GetComponent<PlayerController>());
            } 
            GetComponent<Player>().SetUp();
        }
       

    }


    public override void OnStartClient()
    {
        base.OnStartClient();
        string netId = GetComponent<NetworkIdentity>().netId.ToString();
        Player player = GetComponent<Player>();
        GameManager.RegisterPlayer(netId, player);
    }

    private void RegisterPlayer()
    {
        //change le nom du joueur
        string playerName = "Player" + GetComponent<NetworkIdentity>().netId;
        transform.name = playerName;
    }

    private void AssignRemoteLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(_remoteLayerName);
    }

    private void DisableComponents()
    {
        // Désactiver les différents composants si ce n'est pas le joueur actif
        for (int i = 0; i < componentsToDisable.Length; i++)
        {
            componentsToDisable[i].enabled = false;
        }
    }

    private void OnDisable()
    {
        Destroy(_playerUIInstance);

        if (isLocalPlayer) GameManager.instance.SetSceneCameraActive(true);
        
        GameManager.UnregisterPlayer(transform.name);

    }
}
