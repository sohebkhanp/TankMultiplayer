using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateNicknamePanel : LobbyPanelBase
{
    [Header("CreateNicknamePanel Vars")]
    [SerializeField] private TMP_InputField nicknameInputField;
    [SerializeField] private Button createNicknameButton;

    private const int MAX_CHAR_FOR_NICKNAME = 2;

    public override void InitPanel(LobbyUIManager uIManager)
    {
        base.InitPanel(uIManager);
        createNicknameButton.interactable = false;
        createNicknameButton.onClick.AddListener(OnClickCreateNickname);
        nicknameInputField.onValueChanged.AddListener(OnInputValueChanged);
    }

    private void OnInputValueChanged(string value)
    {
        createNicknameButton.interactable = value.Length >= MAX_CHAR_FOR_NICKNAME;
    }

    private void OnClickCreateNickname()
    {
        string nickname = nicknameInputField.text;
        
        if (nickname.Length >= MAX_CHAR_FOR_NICKNAME)
        {
            base.ClosePanel();
            lobbyUIManager.ShowPanel(LobbyPanelType.MiddleSectionPanel);
        }
    }
}
