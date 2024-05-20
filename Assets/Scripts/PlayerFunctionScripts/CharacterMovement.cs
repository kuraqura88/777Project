using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMovement : MonoBehaviour
{
    CharacterController controller;
    protected Rigidbody2D rb;

    Vector2 movementDirection = Vector2.zero;

    public float speed = 5;
    public bool isDead = false;

    protected void Awake()
    {
        controller = GetComponent<CharacterController>();
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
        if (!isDead) // �׾��� ���� �̵����� ����
        {
            ApplyMovement(movementDirection);
        }
    }
    void ApplyMovement(Vector2 direction)
    {
        direction *= speed;
        rb.velocity = direction;
    }
    public void Die(Vector2 fallVelocity)
    {
        isDead = true;
        rb.velocity = fallVelocity; // �׾��� ���� �߶� �ӵ��� ����
    }
}