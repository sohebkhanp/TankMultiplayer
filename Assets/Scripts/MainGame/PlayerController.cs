using Fusion;
using UnityEngine;

public class PlayerController : NetworkBehaviour, IBeforeUpdate
{
    [SerializeField] private float moveSpeed = 6;

    private float horizontal;
    private Rigidbody2D rb;

    public override void Spawned()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Called at the start of the Fusion Update loop, before the Fusion simulation loop.
    public void BeforeUpdate()
    {
        // we are in local machine
        if (Runner.LocalPlayer.Equals(Object.HasInputAuthority))
        {
            const string HORIZONTAL = "Horizontal";
            horizontal = Input.GetAxisRaw(HORIZONTAL);
        }
    }

    public override void FixedUpdateNetwork()
    {
        // the client that has input authority will control the player
        if (Runner.TryGetInputForPlayer<PlayerData>(Object.InputAuthority, out var playerData)) 
        {
            rb.linearVelocity = new Vector2(playerData.HorizontalInput * moveSpeed, rb.linearVelocity.y);
        }
    }

    public PlayerData GetPlayerNetworkInput()
    {
        PlayerData data = new PlayerData();
        data.HorizontalInput = horizontal;
        return data;
    }
}
