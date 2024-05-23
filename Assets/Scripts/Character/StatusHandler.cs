
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
        Debug.Log("�������� �Ծ����ϴ�.");
        OnHit?.Invoke(isHit);
    }

    public bool Damage(int damage)
    {
        CurrentLife += damage;

        if(CurrentLife <= 0)
        {
            // TODO : ��� ó��
            return true;
        }
        
        if(damage > 0)
        {
            // TODO ��ó��
        }
        else if(damage < 0)
        {
            Hit(true);
            return true;
        }

        return false;
    }
}
