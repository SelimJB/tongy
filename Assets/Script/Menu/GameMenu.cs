using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
	[SerializeField] private Button goBackToTitleScreeButton;
	[SerializeField] private Button restartButton;
	[SerializeField] private LifeManager lifeManager;
	[SerializeField] private GameObject gameoverMenu;

	// FIXME : On pointer down
	private void Start()
	{
		lifeManager.OnGameOver += DisplayGameOverMenu;
		goBackToTitleScreeButton.onClick.AddListener(() => LoadGame(0));
		restartButton.onClick.AddListener(() => LoadGame(1));
	}

	private void LoadGame(int sceneNum)
	{
		lifeManager.OnGameOver -= DisplayGameOverMenu;
		SceneManager.LoadScene(sceneNum);
	}

	private void DisplayGameOverMenu()
	{
		gameoverMenu.SetActive(true);
	}
}