using UnityEngine;

public class DemonMovement : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float damage = 15f;

    [Header("Movement Parameters")]
    [SerializeField] private float moveSpeed = 2f;

    [Header("Collider Parameters")]
    [SerializeField] private float attackColliderDistance = 1f;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;

    [Header("Patrol Bounds")]
    [SerializeField] private Transform leftBound;
    [SerializeField] private Transform rightBound;

    private float cooldownTimer = Mathf.Infinity;

    private Animator animator;
    private Transform player;
    private Vector3 initScale;
    private EnemyPatrol enemyPatrol;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        initScale = transform.localScale;

        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        bool inRange = PlayerInRange();

        // Turn on/off enemyPatrol
        if (enemyPatrol != null)
            enemyPatrol.enabled = !inRange;

        if (!inRange)
        {
            animator.SetBool("move", false);
            return;
        }

        float playerX = player.position.x;
        float enemyX = transform.position.x;

        float distanceToPlayer = Mathf.Abs(playerX - enemyX);

        if (!PlayerInAttackRange())
        {
            animator.SetBool("move", true);

            int direction = playerX < enemyX ? -1 : 1;
            float nextX = enemyX + direction * moveSpeed * Time.deltaTime;

            // Cannot out of bounds
            if (nextX >= leftBound.position.x && nextX <= rightBound.position.x)
            {
                transform.position = new Vector3(nextX, transform.position.y, transform.position.z);
                FlipToDirection(direction);
            }
        }
        else
        {
            animator.SetBool("move", false);

            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                Attack();
            }
        }
    }

    private void FlipToDirection(int direction)
    {
        Vector3 scale = transform.localScale;
        if (Mathf.Sign(scale.x) != direction)
        {
            scale.x = Mathf.Abs(initScale.x) * direction;
            transform.localScale = scale;
        }
    }

    private bool PlayerInRange()
    {
        return player.position.x >= leftBound.position.x && player.position.x <= rightBound.position.x;
    }

    private bool PlayerInAttackRange()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * transform.localScale.x * attackColliderDistance,
            new Vector3(boxCollider.bounds.size.x * attackRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        return hit.collider != null;
    }

    private void Attack()
    {
        animator.SetTrigger("attack");
    }

    private void takeDamage() {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * transform.localScale.x * attackColliderDistance,
            new Vector3(boxCollider.bounds.size.x * attackRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
        {
            Health playerHealth = hit.collider.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if (boxCollider != null)
        {
            Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * transform.localScale.x * attackColliderDistance,
                new Vector3(boxCollider.bounds.size.x * attackRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
        }
    }
}
