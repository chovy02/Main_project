using UnityEngine;

public class BossProjectile : EnemyDamage
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float resetTime = 5f;

    private float lifetime;
    private Animator anim;
    private BoxCollider2D coll;
    private bool hit;

    private Vector2 moveDirection;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
    }

    public void Launch(Vector2 direction)
    {
        hit = false;
        lifetime = 0f;
        gameObject.SetActive(true);
        coll.enabled = true;

        moveDirection = direction.normalized;

        // Rotation
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private void Update()
    {
        if (hit) return;

        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);

        lifetime += Time.deltaTime;
        if (lifetime > resetTime)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            hit = true;
            base.OnTriggerEnter2D(collision);
            coll.enabled = false;

            if (anim != null)
                anim.SetTrigger("explode");
            else
                gameObject.SetActive(false);
        }
    }

   
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
