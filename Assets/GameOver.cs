using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    private Audio sound;
    public void SetUp()
    {
        sound.Playvfx(sound.defeatClip);
        gameObject.SetActive(true);
    }
    private void Awake()
    {
        sound = GameObject.FindGameObjectWithTag("audio").GetComponent<Audio>();
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
