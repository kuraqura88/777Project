using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterDie : CharacterController
{
    private bool isDead = false;
    private Vector2 fallVelocity = Vector2.zero;
    public float fallSpeed = 10f;

    private CharacterMovement characterMovement;
    public Sprite spriteImage;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    CharacterStats stats;

    private void Awake()
    {
        characterMovement = GetComponent<CharacterMovement>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        if (characterMovement == null)
        {
            Debug.LogError("characterMovement is null.");
        }
    }
    void Update()
    {
        if (isDead)
        {
            Fall();
        }
    }
    private void Fall()
    {
        fallVelocity.y -= fallSpeed * Time.deltaTime;
        characterMovement.Die(fallVelocity);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            stats.Life -= 1;
            isDead = true;
            animator.enabled = false;
            if (spriteImage != null)
            {
                spriteRenderer.sprite = spriteImage;
            }
            Invoke("DeactivateGameObject", 3f);
        }
    }
    void DeactivateGameObject()
    {
        gameObject.SetActive(false);
    }

    void GameOver()
    {
        if(stats.Life <= 0)
        {
            SceneManager.LoadScene("GameOverScene"); // 게임 오버 씬으로 전환
        }
    }
}