using UnityEngine;
using UnityEngine.SceneManagement;
public class bossHealth : MonoBehaviour
{
    private EnemyHealth health;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(health.currentHealth == 0)
        {
            SceneManager.LoadSceneAsync("Victory");
        }
    }
}
