using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDie : CharacterMovement
{
    public Sprite spriteImage;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    CharacterStats stats;

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        stats = GetComponentInChildren<CharacterStats>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            if (spriteImage != null)
            {
                spriteRenderer.sprite = spriteImage;
            }
            stats.Life -= 1;
            animator.enabled = false;
            rb.gravityScale = 100f;
        }
    }
}