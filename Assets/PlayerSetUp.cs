using Mirror;
using UnityEngine;

public class PlayerSetUp : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] componentsToDisable;

    Camera sceneCamera;
    private void Start()
    {
        if (!isLocalPlayer)
        {

            // Désactiver les différents composants si ce n'est pas le joueur actif
            for (int i = 0; i < componentsToDisable.Length; i++)
            {
                componentsToDisable[i].enabled = false;
            }
        }
        else
        {
            sceneCamera = Camera.main; 
            if(sceneCamera != null)
            {
                sceneCamera.gameObject.SetActive(false);    
            }
        }
    }

    private void OnDisable()
    {

        if (sceneCamera != null)
        {
            sceneCamera.gameObject.SetActive(true);
        }
        
    }
}
