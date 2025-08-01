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
        // Instantiate projectile here
        Debug.Log("Ranged Attack Triggered");
    }

    public void LazerAttack()
    {
        // Lazer Attack
        Debug.Log("Lazer Attack Triggered");
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


