using UnityEngine;

public class SpearAttack : EnemyDamage
{
    [SerializeField] private float extendedHeight = 5f;

    private Vector2 originalSize;
    private BoxCollider2D boxCollider;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        originalSize = boxCollider.size;
    }

    public void Extend()
    {
        boxCollider.size = new Vector2(boxCollider.size.x, originalSize.y * extendedHeight);
        boxCollider.offset = new Vector2(0, originalSize.y * extendedHeight / 2f);
    }

    public void ResetSpear()
    {
        boxCollider.size = new Vector2(boxCollider.size.x, originalSize.y);
        boxCollider.offset = new Vector2(0, 0);
    }
}
