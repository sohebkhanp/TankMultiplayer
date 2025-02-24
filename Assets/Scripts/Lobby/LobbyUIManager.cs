using UnityEngine;

public class LobbyUIManager : MonoBehaviour
{
    [SerializeField] private LoadingCanvasController loadingCanvasControllerPrefab;
    [SerializeField] private LobbyPanelBase[] lobbyPanels;

    private void Start()
    {
        foreach (var panel in lobbyPanels)
        {
            panel.InitPanel(this);
        }

        Instantiate(loadingCanvasControllerPrefab);
    }

    public void ShowPanel(LobbyPanelBase.LobbyPanelType type)
    {
        foreach (var panel in lobbyPanels)
        {
            if (panel.lobbyPanelType == type)
            {
                panel.ShowPanel();
                break;
            }
        }
    }
}
