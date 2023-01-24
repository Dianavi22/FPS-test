using Mirror;
using UnityEngine;

public class PlayerSetUp : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] componentsToDisable;

    [SerializeField]
    private string _remoteLayerName = "RemotePlayer";

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
            if(sceneCamera != null)
            {
                sceneCamera.gameObject.SetActive(false);    
            }
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
        // Désactiver les différents composants si ce n'est pas le joueur actif
        for (int i = 0; i < componentsToDisable.Length; i++)
        {
            componentsToDisable[i].enabled = false;
        }
    }

    private void OnDisable()
    {

        if (sceneCamera != null)
        {
            sceneCamera.gameObject.SetActive(true);
        }

        GameManager.UnregisterPlayer(transform.name);
        
    }
}
