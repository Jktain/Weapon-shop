using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneManage : MonoBehaviour
{
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
