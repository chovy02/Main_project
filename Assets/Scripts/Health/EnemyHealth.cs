using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health")]
    public float startingHealth;
    public float currentHealth { get; private set; }
    private Animator animator;
    private bool dead = false;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private float numberOfFlashes;
    private SpriteRenderer spriteRend;

    [Header("Components")]
    private bool invulnerable;

    private void Awake()
    {
        currentHealth = startingHealth;
        animator = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float _damage)
    {
        if (invulnerable) return;
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        Debug.Log($"[Boss] HP: {currentHealth} / {startingHealth}"); //Delete after
        if (currentHealth > 0)
        {
            animator.SetTrigger("hurt");
            StartCoroutine(Invunerability());
        }
        else
        {
            if (!dead)
            {
                animator.SetTrigger("die");
                dead = true;
            }

        }
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    public bool getDead()
    {
        return dead;
    }

    private IEnumerator Invunerability()
    {
        invulnerable = true;

        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }

        invulnerable = false;
    }

    public void DisableEnemy()
    {
        gameObject.SetActive(false);
    }

    // Immune
    public void EnableInvulnerability()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(8, 7, true); // 8 = Boss, 7 = Player
    }

    // Turn off immune
    public void DisableInvulnerability()
    {
        invulnerable = false;
        Physics2D.IgnoreLayerCollision(8, 7, false);
    }

}
