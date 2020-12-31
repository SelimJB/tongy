using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreenMenu : MonoBehaviour
{
    [SerializeField] Button startButton;

    // FIXME : On pointer down
    private void Start()
    {
        startButton.onClick.AddListener(LoadGame);
    }

    private void LoadGame()
    {
        SceneManager.LoadScene(1);
    }
}