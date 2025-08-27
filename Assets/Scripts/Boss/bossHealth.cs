using UnityEngine;
using UnityEngine.SceneManagement;
public class bossHealth : MonoBehaviour
{
    private EnemyHealth health;
    private victory win;
    // Update is called once per frame

    public void Awake()
    {
        health = GetComponent<EnemyHealth>();
        win = FindObjectOfType<victory>(true);
    }
    void Update()
    {
        if (health != null && health.getDead())
        {
            if (win != null)
            {
                win.SetUp();
            }
            SceneManager.LoadSceneAsync("Victory");
        }
    }
}
