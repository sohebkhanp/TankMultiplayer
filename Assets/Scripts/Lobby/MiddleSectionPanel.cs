using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MiddleSectionPanel : LobbyPanelBase
{
    [Header("MiddleSectionPanel Vars")]
    [SerializeField] private Button joinRandomRoomBtn;
    [SerializeField] private Button joinRoomByArgsBtn;
    [SerializeField] private Button createRoomBtn;

    [SerializeField] private TMP_InputField joinRoomByArgsInputField;
    [SerializeField] private TMP_InputField createRoomInputField;

    private NetworkRunnerController networkRunnerController;

    public override void InitPanel(LobbyUIManager uIManager)
    {
        base.InitPanel(uIManager);

        networkRunnerController = GlobalManager.Instance.networkRunnerController;

        createRoomBtn.onClick.AddListener(()=> CreateRoom(GameMode.Host, createRoomInputField.text));
        joinRoomByArgsBtn.onClick.AddListener(()=> CreateRoom(GameMode.Client, joinRoomByArgsInputField.text));

    }

    private void CreateRoom(GameMode mode, string field)
    {
        if (field.Length >= 2)
        {
            Debug.Log($"................{mode}.................");
            networkRunnerController.StartGame(mode, field);
        }
    }

    private void JoinRandomRoom()
    {
        Debug.Log($"................JoinRandomRoom.................");
        networkRunnerController.StartGame(GameMode.AutoHostOrClient, string.Empty);
    }
}
