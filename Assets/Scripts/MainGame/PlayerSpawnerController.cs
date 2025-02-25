using Fusion;
using UnityEngine;

public class PlayerSpawnerController : NetworkBehaviour, IPlayerJoined, IPlayerLeft
{
    [SerializeField] private NetworkPrefabRef playerNetworkPrefab = NetworkPrefabRef.Empty;
    [SerializeField] private Transform[] SpawnPoints;

    //public override void Spawned()
    //{
    //    if (Runner.IsServer)
    //    {
    //        foreach (var item in Runner.ActivePlayers)
    //        {
    //            SpawnPlayer(item);
    //        }
    //    }
    //}

    private void SpawnPlayer(PlayerRef playerRef)
    {
        if (Runner.IsServer)
        {
            var index = playerRef.AsIndex % SpawnPoints.Length;
            var spawnPoint = SpawnPoints[index].transform.position;
            var playerObject = Runner.Spawn(playerNetworkPrefab, Vector3.zero, Quaternion.identity, playerRef);

            Runner.SetPlayerObject(playerRef, playerObject);
        }
    }

    public void PlayerJoined(PlayerRef player)
    {
        SpawnPlayer(player);
    }

    private void DespawnPlayer(PlayerRef playerRef)
    {
        if (Runner.IsServer)
        {
            if (Runner.TryGetPlayerObject(playerRef, out var playerNetworkObject))
            {
                Runner.Despawn(playerNetworkObject);
            }

            // Reset player object
            Runner.SetPlayerObject(playerRef, null);
        }
    }
    public void PlayerLeft(PlayerRef player)
    {
        DespawnPlayer(player);
    }
}
