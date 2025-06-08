using UnityEngine;

public class Player : MonoBehaviour
{
    
    public int playerHealth = 100;
    public int currentHealth;

    public Health_bar healthBar;
    void Start()
    {
        currentHealth = playerHealth;
        healthBar.SetMaxHealth(playerHealth);
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            takeDamage(20);
        }
    }
    void takeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.setHealth(currentHealth);
    }
}
