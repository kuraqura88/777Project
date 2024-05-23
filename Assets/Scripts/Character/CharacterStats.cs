using UnityEngine;

public class CharacterStats
{
    public int Life { get; private set; }
    public int Power { get; private set; }
    public float Speed { get; private set; }

    public void SetTypeStats(Define.CharacterType characterType)
    {
        switch (characterType)
        {
            case Define.CharacterType.Normal:
                Life = 3;
                Power = 1;
                Speed = 1.0f;
                break;
            case Define.CharacterType.Rare:
                Life = 5;
                Power = 2;
                Speed = 2.0f;
                break;
            case Define.CharacterType.Unique:
                Life = 7;
                Power = 3;
                Speed = 3.0f;
                break;
            case Define.CharacterType.Epic:
                Life = 400;
                Power = 4;
                Speed = 4.0f;
                break;
            default:
                break;
        }
    }

    #region ========== Setter ==========
    
    public void SetLife(int life)
    {
        Life = life;
    }

    public void SetPower(int power)
    {
        this.Power = power;
    }

    public void SetSpeed(float speed)
    {
        Speed = speed;
    }

    #endregion
}