using UnityEngine;

public class victory : MonoBehaviour
{
    private Audio victorySound;

    private void Awake()
    {
        victorySound = GameObject.FindGameObjectWithTag("audio").GetComponent<Audio>();
        
    }

    public void SetUp()
    {
        victorySound.Playvfx(victorySound.victoryClip);
    }

    private void Start()
    {
        SetUp();
    }

}
