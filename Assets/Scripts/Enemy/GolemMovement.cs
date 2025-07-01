using UnityEngine;
using System.Collections;

public class MeleeEnemy : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float dashCooldown;
    [SerializeField] private float slashCooldown;
    [SerializeField] private float dashRange = 5f;
    [SerializeField] private float slashRange = 2f;
    [SerializeField] private float dashSpeed = 5f;

    [Header("Collider Parameters")]
    [SerializeField] private float dashColliderDistance = 1f;
    [SerializeField] private float slashColliderDistance = 1f;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;


    private float dashCooldownTimer = Mathf.Infinity;
    private float slashCooldownTimer = Mathf.Infinity;

    private Animator animator;
    private Transform player;
    private EnemyPatrol enemyPatrol;    

    void Awake()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    // Update is called once per frame
    void Update()
    {
        dashCooldownTimer += Time.deltaTime;
        slashCooldownTimer += Time.deltaTime;
        if (PlayerInRange(dashRange, dashColliderDistance))
        {
            if (dashCooldownTimer >= dashCooldown)
            {
                dashCooldownTimer = 0;
                StartCoroutine(DashAndSlash());
            }

            else if (PlayerInRange(slashRange, slashColliderDistance) && slashCooldownTimer >= slashCooldown)
            {
                slashCooldownTimer = 0;
                Slash();
            }
        }

        if (enemyPatrol != null) 
        {
            enemyPatrol.enabled = !PlayerInRange(dashRange, dashColliderDistance);
        }
    }

    private bool PlayerInRange(float range, float colliderDistance)
    {
        RaycastHit2D hitDashing = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * transform.localScale.x *  colliderDistance, new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0, Vector2.left, 0, playerLayer);
        return hitDashing.collider != null;
    }

    private IEnumerator DashAndSlash()
    {
        animator.SetBool("dash", true);
        Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, transform.position.z);
        if ((targetPosition.x - transform.position.x) * transform.localScale.x < 0)
        {
            Flip();
        } 

        while (Vector2.Distance(transform.position,  targetPosition) > 0.3f)
        {
            transform.Translate(Vector2.right * transform.localScale.x * dashSpeed * Time.deltaTime);
            yield return null;
        }
        animator.SetBool("dash", false);
        Slash();
    }

    void Flip()
    {
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    private void Slash()
    {
        animator.SetTrigger("slash");
    }

    private void takeDamage()
    {
        if (PlayerInRange(slashRange, slashColliderDistance))
        {
            Debug.Log("Slashed");
        }
    }

    private void OnDrawGizmos()
    {   
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * transform.localScale.x * dashColliderDistance, new Vector3(boxCollider.bounds.size.x * dashRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z));

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * transform.localScale.x * slashColliderDistance, new Vector3(boxCollider.bounds.size.x * slashRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
}
