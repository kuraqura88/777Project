using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : EnemyController
{
    public Define.BossType bossType = Define.BossType.Basic;


    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        statusHandler.SetCharacterStat(CreateBossStat(bossType));
        GameManager.Instance.OnFightBoss += CanDamage;
    }



    protected override void OnDisable()
    {
        base.OnDisable();
        GameManager.Instance.OnFightBoss -= CanDamage;

    }

    protected override void Start()
    {
        base.Start();

    }
    private void CanDamage()
    {
        col.enabled = true;
    }

    public void Appear()
    {
        col.enabled = false;
        StartCoroutine(MoveToPos(new Vector3(9.5f, -5.25f, 0)));
    }

    private IEnumerator MoveToPos(Vector3 pos)
    {
        while(Vector3.Distance(transform.position, pos) >= 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, pos, 0.1f);
            yield return null;
        }
        yield break;
    }

    private CharacterStats CreateBossStat(Define.BossType type)
    {
        CharacterStats stats = new CharacterStats();

        switch(type)
        {
            case Define.BossType.Basic:
                stats.SetLife(10);
                stats.SetPower(1);
                stats.SetSpeed(5);

                break;
            case Define.BossType.Standard:
                stats.SetLife(15);
                stats.SetPower(1);
                stats.SetSpeed(5);
                break;
            case Define.BossType.Challange:
                stats.SetLife(20);
                stats.SetPower(1);
                stats.SetSpeed(5);
                break;

        }
        return stats;
    }

    protected override void OnDead()
    {
        base.OnDead();
        GameManager.Instance.GameClear();
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnFightBoss -= CanDamage;

    }
}
