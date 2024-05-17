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
    CharacterStats stats;
    protected Rigidbody2D rb;
    Vector2 movementDirection = Vector2.zero;

    protected void Awake()
    {
        controller = GetComponent<MoveController>();
        rb = GetComponent<Rigidbody2D>();
        stats = GetComponentInChildren<CharacterStats>();
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
        direction *= stats.Speed;
        rb.velocity = direction;
    }
}