using UnityEngine;
using UnityEngine.UI;

public class LoadingCanvasController : MonoBehaviour
{
    [SerializeField] private Button cancelButton;
    [SerializeField] private Animator animator;

    private NetworkRunnerController networkRunnerController;

    private void Start()
    {
        networkRunnerController = GlobalManager.Instance.networkRunnerController;
        networkRunnerController.OnStartedRunnerConnection += OnStartedRunnerConnection;
        networkRunnerController.OnPlayerJoinedSuccessfully += OnPlayerJoinedSuccessfully;

        cancelButton.onClick.AddListener(networkRunnerController.ShutDownRunner);

        this.gameObject.SetActive(false);
    }

    private void OnStartedRunnerConnection()
    {
        this.gameObject.SetActive(true);
        const string CLIP_NAME = "In";
        StartCoroutine(Utils.PlayAnimAndSetStateWhenFinished(gameObject, animator, CLIP_NAME));
    }

    private void OnPlayerJoinedSuccessfully()
    {
        //const string CLIP_NAME = "Out";
        //StartCoroutine(Utils.PlayAnimAndSetStateWhenFinished(gameObject, animator, CLIP_NAME, false));
    }

}
