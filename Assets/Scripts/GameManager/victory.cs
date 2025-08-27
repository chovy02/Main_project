using UnityEngine;

public class victory : MonoBehaviour
{
    private Audio victorySound;

    private void Awake()
    {

        victorySound = GameObject.FindGameObjectWithTag("audio").GetComponent<Audio>();
        gameObject.SetActive(false);
    }

    public void SetUp()
    {
        gameObject.SetActive(true);
        victorySound.GetComponent<AudioSource>().Stop();
        victorySound.Playvfx(victorySound.victoryClip);
    }

    private void Start()
    {
        SetUp();
    }

}
