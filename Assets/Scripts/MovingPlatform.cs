using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovingPlatform : MonoBehaviour
{
    public float tocDoDiChuyen = 1f;
    public Transform start;
    public Transform end;

    private Vector2 diemMucTieu;
    private Rigidbody2D rb;
    private Vector2 previousPosition;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        diemMucTieu = end.position;
        previousPosition = rb.position;
    }

    private void FixedUpdate()
    {
        if (Vector2.Distance(rb.position, diemMucTieu) < 0.1f)
        {
            diemMucTieu = diemMucTieu == (Vector2)start.position ? end.position : start.position;
        }

        Vector2 newPosition = Vector2.MoveTowards(rb.position, diemMucTieu, tocDoDiChuyen * Time.fixedDeltaTime);
        rb.MovePosition(newPosition);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.collider.attachedRigidbody;
            if (playerRb != null)
            {
                Vector2 delta = rb.position - previousPosition;
                playerRb.position += delta;
            }
        }
    }

    private void LateUpdate()
    {
        previousPosition = rb.position;
    }
}
