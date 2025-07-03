using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Common Settings")]
    [SerializeField] private WeaponManager weaponManager;
    [SerializeField] private Animator animator;

    [Header("Melee Attack Settings")]
    [SerializeField] private float unarmedDamage = 1f;
    [SerializeField] private float swordDamage = 2f;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float attackColliderDistance = 0.5f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Ranged Attack Settings")]
    [SerializeField] private float bowCooldown = 1f;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] arrows;

    private float bowCooldownTimer = Mathf.Infinity;

    private void Update()
    {
        if (weaponManager.currentWeapon == WeaponType.Bow)
        {
            bowCooldownTimer += Time.deltaTime;

            if (Input.GetKey(KeyCode.X) && bowCooldownTimer >= bowCooldown)
            {
                animator.SetTrigger("attack");
                bowCooldownTimer = 0f;
            }
        }
        else if (weaponManager.currentWeapon == WeaponType.Sword || weaponManager.currentWeapon == WeaponType.None)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                animator.SetTrigger("attack");
            }
        }
    }

    public void PerformAttack()
    {
        float damage = weaponManager.currentWeapon == WeaponType.Sword ? swordDamage : unarmedDamage;

        RaycastHit2D hit = Physics2D.BoxCast(
            boxCollider.bounds.center + transform.right * transform.localScale.x * attackColliderDistance,
            new Vector3(boxCollider.bounds.size.x * attackRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0,
            Vector2.left,
            0,
            enemyLayer
        );

        if (hit.collider != null)
        {
            hit.collider.GetComponent<Health>()?.TakeDamage(damage);
        }
    }

    public void ShootArrow()
    {
        int arrowIndex = FindArrow();
        if (arrowIndex >= 0)
        {
            arrows[arrowIndex].transform.position = firePoint.position;
            arrows[arrowIndex].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
        }
    }

    private int FindArrow()
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            if (!arrows[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    private void OnDrawGizmosSelected()
    {
        if (weaponManager == null || boxCollider == null) return;
        if (weaponManager.currentWeapon == WeaponType.Bow) return;

        Gizmos.color = weaponManager.currentWeapon == WeaponType.Sword ? Color.red : Color.green;
        Vector3 boxCenter = boxCollider.bounds.center + transform.right * transform.localScale.x * attackColliderDistance;
        Vector3 boxSize = new Vector3(boxCollider.bounds.size.x * attackRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z);
        Gizmos.DrawWireCube(boxCenter, boxSize);
    }
}
