using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image currentHealthBar;

    private void Start()
    {
        totalHealthBar.fillAmount = playerHealth.currentHealth / 100;
    }

    private void Update()
    {
        currentHealthBar.fillAmount = 0.2f + 0.8f * (playerHealth.currentHealth / 100);
    }
}
