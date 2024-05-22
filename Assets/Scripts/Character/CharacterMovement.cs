using System;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMovement : MonoBehaviour
{
    CharacterController controller;

    protected Rigidbody2D rb;

    private Collider2D collider; 

    Vector2 movementDirection = Vector2.zero;

    public float speed = 5;
    public bool isDead = false;
    
    private bool isStart = false;

    private bool isHit = false;

    protected void Awake()
    {
        controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        controller.OnMoveEvent += Move;
        GameManager.Instance.Player.statusHandler.OnHit += OnHit;
        GameManager.Instance.Player.statusHandler.OnDead += OnDead;
        GameManager.Instance.OnGameClear += Invincibility;
        GameManager.Instance.OnGameStart += CanMove;
    }



    private void OnDisable()
    {
        controller.OnMoveEvent -= Move;
        if(GameManager.Instance.Player != null)
        {
            GameManager.Instance.Player.statusHandler.OnHit -= OnHit;
            GameManager.Instance.Player.statusHandler.OnDead -= OnDead;
        }

        GameManager.Instance.OnGameClear -= Invincibility;

        GameManager.Instance.OnGameStart -= CanMove;
    }



    private void FixedUpdate()
    {
        if (isStart)
        {
            if(!isHit)
            {
                ApplyMovement(movementDirection);
            }
            else
            {
                Fall();
            }
        }
    }
    void Move(Vector2 direction)
    {
        movementDirection = direction;
    }

    private void OnDead()
    {
        isStart = false;
        rb.velocity = Vector2.zero;
    }

    void ApplyMovement(Vector2 direction)
    {
        direction *= speed;
        rb.velocity = direction;
    }

    private void CanMove(Define.Scene scene)
    {
        if(scene != Define.Scene.Start || scene != Define.Scene.ClearStage || scene != Define.Scene.GameoverStage)
        {
            isStart = true;
        }
    }

    private void OnHit(bool active)
    {
        isHit = true;
        collider.enabled = false;

        rb.velocity = Vector2.zero;
        Invoke(nameof(Respawn), 2.5f);
    }

    private void Fall()
    {
        Vector2 newVelocity = rb.velocity + Vector2.left* 0.5f + Vector2.down * 1.5f;
        rb.velocity = new Vector2(Mathf.Clamp(newVelocity.x, -3f, 0), Mathf.Clamp(newVelocity.y, -10f, 0));
    }

    private void Respawn()
    {
        transform.position = new Vector3(-7f, 0, 0);
        isHit = false;
        Invoke(nameof(CanDamage), 0.5f);
    }

    private void Invincibility()
    {
        collider.enabled = false;
    }
    private void CanDamage()
    {
        collider.enabled = true;
    }
}