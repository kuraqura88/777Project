using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMovement : MonoBehaviour
{
    CharacterController controller;

    protected Rigidbody2D rb;

    Vector2 movementDirection = Vector2.zero;

    public float speed = 5;
    public bool isDead = false;
    
    private bool isStart = false;

    private bool isHit = false;

    protected void Awake()
    {
        controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        controller.OnMoveEvent += Move;

        GameManager.Instance.OnGameStart += CanMove;
    }

    private void OnDisable()
    {
        controller.OnMoveEvent -= Move;

        GameManager.Instance.OnGameStart -= CanMove;
    }

    void Move(Vector2 direction)
    {
        movementDirection = direction;
    }

    private void FixedUpdate()
    {
        if (isStart)
        {
            if(!isHit)
            {
                ApplyMovement(movementDirection);
            }
        }
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
}