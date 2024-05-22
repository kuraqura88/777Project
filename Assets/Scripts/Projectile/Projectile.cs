using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    private Rigidbody2D _rb2d;

    private float speed = 10.0f;

    private Vector2 direction = Vector2.zero;

    public LayerMask target;

    public SpriteRenderer spRenderer;
    public GameObject hitObj;

    public AudioSource audioSource;
    public AudioClip hitSound;

    private bool isHit = false;

    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        spRenderer.enabled = true;
        hitObj.SetActive(false);
        isHit = false;

        SceneManager.sceneLoaded += OnLoadedScene;
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

        spRenderer.flipX = direction.x < 0;
    }
    private void OnLoadedScene(Scene arg0, LoadSceneMode arg1)
    {
        PoolManager.Instance.Push<Projectile>(this);
    }

    public void Shoot()
    {
        if(!isHit)
            _rb2d.velocity = direction * speed;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(IsLayerMatched(target, collision.gameObject.layer))
        {
            IDamagable damagable = collision.gameObject.GetComponent<IDamagable>();
            
            if(damagable != null)
            {
                if(audioSource.isPlaying)
                {
                    audioSource.Stop();
                }
                audioSource.PlayOneShot(hitSound, 0.5f);
                damagable.Damage(-1);
                isHit = true;
                _rb2d.velocity = Vector2.zero;
                spRenderer.enabled = false;
                hitObj.SetActive(true);
            }
            Invoke(nameof(Hide), 0.5f);
        }
        if (collision.CompareTag("Level"))
        {
            PoolManager.Instance.Push<Projectile>(this);
        }

    }

    public void Hide()
    {
        PoolManager.Instance.Push<Projectile>(this);
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
