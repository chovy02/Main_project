using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    public void SetUp()
    {
        gameObject.SetActive(true);
    }
    public void RestartButton()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
