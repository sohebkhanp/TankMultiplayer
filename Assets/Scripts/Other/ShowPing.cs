using Fusion;
using UnityEngine;
using UnityEngine.UI;

public class ShowPing : MonoBehaviour
{
    public Text pingText;
    private NetworkRunner networkRunner;

    private void Start()
    {
        networkRunner = FindAnyObjectByType<NetworkRunner>();
    }

    void Update()
    {
        double ping = networkRunner.GetPlayerRtt(networkRunner.LocalPlayer) * 1000; // multiply by 1000 to convert to milliseconds

        pingText.text = "Ping: " + ping.ToString("0.0") + " ms";
    }
}
