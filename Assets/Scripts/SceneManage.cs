using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManage : MonoBehaviour
{
    public void SaveAndGoToMainMenu()
    {
        SaveManager.SaveGame();
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("GameScene");
        SaveManager.LoadGame();
    }
}
