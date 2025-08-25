using UnityEngine;
using UnityEngine.SceneManagement;
public class bossHealth : MonoBehaviour
{
    private EnemyHealth health;

    // Update is called once per frame
    void Update()
    {
        if(health.currentHealth == 0)
        {
            SceneManager.LoadSceneAsync("Victory");
        }
    }
}
