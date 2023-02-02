using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    private const string _playerIdPrefix = "Player ";
 private static Dictionary<string, Player> _players = new Dictionary<string, Player>();
    public MatchSettings matchSettings;
    public static GameManager instance;
    [SerializeField] private GameObject sceneCamera;
    private bool isActive;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            return;
        }
        Debug.LogError("Plus d'une instance de GameManager daans la scène");
    }
    public static void  RegisterPlayer(string netID, Player player)
    {
        string playerId = _playerIdPrefix + netID;
        _players.Add(playerId, player);
        player.transform.name = playerId;
    }

    public void SetSceneCameraActive(bool isActive)
    {
        if (sceneCamera == null) return;
        sceneCamera.SetActive(isActive);
    }

    public static void UnregisterPlayer(string playerId)
    {
        _players.Remove(playerId);
    }
    public static Player GetPlayer(string playerId)
    {
        return _players[playerId];
    }

    public static Player[] GetAllPlayers()
    {
        return _players.Values.ToArray();
    }
}
