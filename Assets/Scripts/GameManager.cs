using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const string _playerIdPrefix = "Player ";
 private static Dictionary<string, Player> _players = new Dictionary<string, Player>();

    public static void  RegisterPlayer(string netID, Player player)
    {
        string playerId = _playerIdPrefix + netID;
        _players.Add(playerId, player);
        player.transform.name = playerId;
    }

    public static void UnregisterPlayer(string playerId)
    {
        _players.Remove(playerId);
    }
}
