using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    private Audio sound;
    public void SetUp()
    {
        gameObject.SetActive(true);
        sound.GetComponent<AudioSource>().Stop();
        sound.Playvfx(sound.defeatClip);
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
