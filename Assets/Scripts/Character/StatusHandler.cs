
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
        OnHit?.Invoke(isHit);
    }

    public bool Damage(int damage)
    {
        CurrentLife += damage;

        if(CurrentLife <= 0)
        {
            // TODO : 荤噶 贸府
            return true;
        }
        
        if(damage > 0)
        {
            // TODO 鳃贸府
        }
        else if(damage < 0)
        {
            Hit(true);
            return true;
        }

        return false;
    }
}
