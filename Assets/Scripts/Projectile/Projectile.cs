using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    private Rigidbody2D _rb2d;

    private float speed = 10.0f;

    private Vector2 direction = Vector2.zero;

    private LayerMask target;

    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Shoot();
    }

    public void Init(Vector2 direction, float speed, LayerMask target)
    {
        this.direction = direction;
        this.speed = speed;
        this.target = target;
    }

    public void Shoot()
    {
        _rb2d.velocity = direction * speed;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(IsLayerMatched(target, collision.gameObject.layer))
        {
            IDamagable damagable = collision.gameObject.GetComponent<IDamagable>();
            
            if(damagable != null)
            {
                damagable.Damage(-1);
                Debug.Log("¾Æ¾ß");
            }

            PoolManager.Instance.Push<Projectile>(this);
        }
        if (collision.CompareTag("Level"))
        {
            PoolManager.Instance.Push<Projectile>(this);
        }

    }

    private void Clear()
    {
        _rb2d.velocity = Vector2.zero;
        direction = Vector2.zero;
    }

    private bool IsLayerMatched(int value, int objectLayer)
    {
        return value == (value | 1 << objectLayer);
    }
}
