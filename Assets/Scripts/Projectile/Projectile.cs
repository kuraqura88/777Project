using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    private Rigidbody2D _rb2d;

    private float speed = 10.0f;

    private Vector2 direction = Vector2.zero;

    private string target = "";


    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Shoot();
    }

    public void Init(Vector2 direction, float speed)
    {
        this.direction = direction;
        this.speed = speed;
    }

    public void Shoot()
    {
        _rb2d.velocity = direction * speed;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            PoolManager.Instance.Push<Projectile>(this);
        }

    }

    private void Clear()
    {
        _rb2d.velocity = Vector2.zero;
        direction = Vector2.zero;
    }

}
