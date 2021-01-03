using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] Button goBackToTitleScreeButton;
    [SerializeField] Button restartButton;

    // FIXME : On pointer down
    private void Start()
    {
        goBackToTitleScreeButton.onClick.AddListener(() => LoadGame(0));
        restartButton.onClick.AddListener(() => LoadGame(1));
    }

    private void LoadGame(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
    }
}