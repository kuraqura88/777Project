using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.RuleTile.TilingRuleOutput;
using UnityEditor;

public class CharacterMovement : MonoBehaviour
{
    MoveController controller;
    protected Rigidbody2D rb;
    Vector2 movementDirection = Vector2.zero;
    public float speed = 5;

    protected void Awake()
    {
        controller = GetComponent<MoveController>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        controller.OnMoveEvent += Move;
    }
    void Move(Vector2 direction)
    {
        movementDirection = direction;
    }
    private void FixedUpdate()
    {
        ApplyMovement(movementDirection);
    }
    void ApplyMovement(Vector2 direction)
    {
        direction *= speed;
        rb.velocity = direction;
    }
}