using System;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class PlayerController : MonoBehaviour
{
    public StatusHandler statusHandler;

    public SpriteLibrary library;

    public Animator animator;

    public GameObject Root;

    private readonly int hashDead = Animator.StringToHash("IsDead");

    private readonly int hashHit = Animator.StringToHash("IsHit");
    [SerializeField]
    private CharacterMovement movement;

    private void OnEnable()
    {
        statusHandler.OnHit += OnHit;
        statusHandler.OnDead += OnDead;
    }


    private void OnDisable()
    {
        statusHandler.OnHit -= OnHit;
        statusHandler.OnDead -= OnDead;
    }

    public void ChangeStutus(CharacterStats stat)
    {
        if(stat != null)
            statusHandler.SetCharacterStat(stat);
    }

    public void ChangeCharacter(SpriteLibraryAsset asset)
    {
        if (library != null)
        {
            if (asset != null)
            {
                library.spriteLibraryAsset = asset;
            }
            else
            {
                Debug.Log("에셋이 없습니다");
            }
        }
        else
        {
            Debug.Log("라이브러리가 없습니다");
        }
    }
    public void RefreshCharacter()
    {
        library.RefreshSpriteResolvers();
    }

    public void Enter()
    {
        Debug.Log("등장");
    }

    private void OnHit(bool active)
    {
        animator.SetTrigger(hashHit);

    }

    private void OnDead()
    {
        animator.SetBool(hashDead, true);

        Invoke(nameof(Dead), 1f);
    }

    private void Dead()
    {
        GameManager.Instance.GameOver();

        Destroy(Root);

    }
}