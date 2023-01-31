using Mirror;
using UnityEngine;

public class PlayerSetUp : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] componentsToDisable;

    [SerializeField]
    private string _remoteLayerName = "RemotePlayer";

    [SerializeField] private string _dontDrawLayerName = "DontDraw";
    [SerializeField] private GameObject _playerGraphics;

    [SerializeField] private GameObject _playerUIPrefab;
    private GameObject _playerUIInstance;

    Camera sceneCamera;
    private void Start()
    {
        if (!isLocalPlayer)
        {
            AssignRemoteLayer();
            DisableComponents();
        }
        else
        {
            sceneCamera = Camera.main;
            if (sceneCamera != null)
            {
                sceneCamera.gameObject.SetActive(false);
            }

            //d�sactiver la partie graphique du joueur locaal
            Util.SetLayerRecurcively(_playerGraphics, LayerMask.NameToLayer(_dontDrawLayerName));

            //cr�ation du UI du joueur local
            _playerUIInstance = Instantiate(_playerUIPrefab);

        }
        GetComponent<Player>().SetUp();

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
        // D�sactiver les diff�rents composants si ce n'est pas le joueur actif
        for (int i = 0; i < componentsToDisable.Length; i++)
        {
            componentsToDisable[i].enabled = false;
        }
    }

    private void OnDisable()
    {
        Destroy(_playerUIInstance);

        if (sceneCamera != null)
        {
            sceneCamera.gameObject.SetActive(true);
        }

        GameManager.UnregisterPlayer(transform.name);

    }
}
