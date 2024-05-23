
using System;
using UnityEngine;

public class StatusHandler : MonoBehaviour, IDamagable
{
    public event Action<bool> OnHit;
    public Define.CharacterType characterType { get; private set; }

    public CharacterStats CurrentStat { get; private set; }


    public int CurrentLife { get; private set; }

    public void SetCharacterStat(CharacterStats characterStat)
    {
        CurrentStat = characterStat;

        CurrentLife = characterStat.Life;   
    }
    
    public void Hit(bool isHit)
    {
        Debug.Log("데미지를 입었습니다.");
        OnHit?.Invoke(isHit);
    }

    public bool Damage(int damage)
    {
        CurrentLife += damage;

        if(CurrentLife <= 0)
        {
            // TODO : 사망 처리
            return true;
        }
        
        if(damage > 0)
        {
            // TODO 힐처리
        }
        else if(damage < 0)
        {
            Hit(true);
            return true;
        }

        return false;
    }
}
