using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    private Rigidbody2D _rb2d;
    private float speed = 10.0f;

    private Vector2 direction = Vector2.left;


    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Shot();
    }

    public void Init(Vector2 direction)
    {
        this.direction = direction;
    }

    public void Shot()
    {
        _rb2d.velocity = direction * speed;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            PoolManager.Instance.Push(gameObject);
        }
    }

}
