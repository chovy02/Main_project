using UnityEngine;

public class FireTrap : MonoBehaviour
{
    [SerializeField] private float damage;

    private bool triggered = false;

    public void Activate()
    {
        triggered = true;
    }

    public void Deactivate()
    {
        triggered = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (triggered)
            {
                collision.GetComponent<Health>().TakeDamage(damage);
            }
        }
        
    }
}
