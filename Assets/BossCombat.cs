using System.Collections;
using UnityEngine;

public class BossCombat : MonoBehaviour
{
    [Header("Cooldowns")]
    public float blockCooldown = 5f;
    public float rangedCooldown = 4f;
    public float lazerCooldown = 6f;

    private float blockTimer;
    private float rangedTimer;
    private float lazerTimer;

    private bool hasHealed = false;
    private Animator animator;
    private EnemyHealth health;
    public Transform player;

    [Header("Melee Attack")]
    [SerializeField] private float meleeRange = 2f;
    [SerializeField] private float meleeColliderDistance = 1f;
    [SerializeField] private float damage = 20f;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;

    [Header("Ranged Attack")]
    [SerializeField] private Transform firepoint; 
    [SerializeField] private BossProjectile[] rangedProjectiles;

    [Header("Lazer Attack")]
    [SerializeField] private Transform lazerPoint;            
    [SerializeField] private LazerProjectile[] lazerProjectiles; 


    private void Awake()
    {
        animator = GetComponent<Animator>();
        health = GetComponent<EnemyHealth>();
    }

    private void Update()
    {
        if (health.currentHealth <= 0) return;

        blockTimer -= Time.deltaTime;
        rangedTimer -= Time.deltaTime;
        lazerTimer -= Time.deltaTime;

        if (blockTimer <= 0f)
        {
            blockTimer = blockCooldown;
            animator.SetTrigger("block");
            return;
        }

        if (!hasHealed && health.currentHealth <= health.startingHealth * 0.3f)
        {
            hasHealed = true;
            animator.SetTrigger("heal");
            return;
        }

        if (hasHealed && lazerTimer <= 0f)
        {
            lazerTimer = lazerCooldown;
            animator.SetTrigger("lazer");
            return;
        }

        if (!hasHealed && rangedTimer <= 0f)
        {
            rangedTimer = rangedCooldown;
            animator.SetTrigger("ranged");
            return;
        }

        // Melee Attack
        if (IsPlayerInMeleeRange())
        {
            animator.SetTrigger("melee");
        }
    }

    private bool IsPlayerInMeleeRange()
    {
        RaycastHit2D hit = Physics2D.BoxCast(
            boxCollider.bounds.center + transform.right * transform.localScale.x * meleeColliderDistance,
            new Vector3(boxCollider.bounds.size.x * meleeRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer
        );

        return hit.collider != null;
    }


    // Call from Animation Event
    public void MeleeAttack()
    {
        RaycastHit2D hit = Physics2D.BoxCast(
            boxCollider.bounds.center + transform.right * transform.localScale.x * meleeColliderDistance,
            new Vector3(boxCollider.bounds.size.x * meleeRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer
        );

        if (hit.collider != null)
        {
            Health playerHealth = hit.collider.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                Debug.Log("Boss dealt damage to Player");
            }
        }
    }

    public void RangedAttack()
    {
        int index = FindInactiveProjectile();
        BossProjectile projectile = rangedProjectiles[index];

        projectile.transform.position = firepoint.position;

        Vector2 direction = (Vector2)(player.position) - (Vector2)firepoint.position;

        projectile.Launch(direction);

        Debug.Log("Boss fired a ranged projectile!");
    }


    private int FindInactiveProjectile()
    {
        for (int i = 0; i < rangedProjectiles.Length; i++)
        {
            if (!rangedProjectiles[i].gameObject.activeInHierarchy)
                return i;
        }
        return 0; 
    }


    public void LazerAttack()
    {
        int index = FindInactiveLazer();
        LazerProjectile lazer = lazerProjectiles[index];

        lazer.transform.position = lazerPoint.position;

        Vector2 direction = (Vector2)(player.position) - (Vector2)lazerPoint.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        lazer.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        lazer.ActivateLazer();

        Debug.Log("Boss fired a lazer!");
    }

    private int FindInactiveLazer()
    {
        for (int i = 0; i < lazerProjectiles.Length; i++)
        {
            if (!lazerProjectiles[i].gameObject.activeInHierarchy)
                return i;
        }
        return 0; // fallback
    }


    public void Heal()
    {
        health.AddHealth(health.startingHealth * 0.3f);
        Debug.Log("Healed");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(
            boxCollider.bounds.center + transform.right * transform.localScale.x * meleeColliderDistance,
            new Vector3(boxCollider.bounds.size.x * meleeRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z)
        );
    }
}


