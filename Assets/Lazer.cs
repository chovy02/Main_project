using UnityEngine;

public class LazerProjectile : EnemyDamage
{
    private BoxCollider2D coll;

    private void Awake()
    {
        coll = GetComponent<BoxCollider2D>();
        coll.enabled = false;
    }

    public void ActivateLazer()
    {
        coll.enabled = true;
        gameObject.SetActive(true);
    }

    public void DeactivateLazer()
    {
        coll.enabled = false;
        gameObject.SetActive(false);
    }

    public void TakeDamage()
{
    Collider2D[] hits = Physics2D.OverlapBoxAll(coll.bounds.center, coll.bounds.size, 0f, LayerMask.GetMask("Player"));

    foreach (Collider2D collision in hits)
    {
        if (collision.CompareTag("Player"))
        {
            base.OnTriggerEnter2D(collision);

            Debug.Log("Lazer dealt damage to Player");
        }
    }
}



}
