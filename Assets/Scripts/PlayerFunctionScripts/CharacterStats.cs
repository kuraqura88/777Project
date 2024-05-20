using UnityEngine;

public class CharacterStats : MonoBehaviour, IDamagable
{
    public CharacterType characterType { get; private set; } // 수정: private set 추가
    public int Life { get; set; }
    public int Power { get; set; }
    public int Speed { get; set; }

    public CharacterStats(CharacterType characterType, int life, int damage, int speed)
    {
        this.characterType = characterType;
        Life = life;
        Power = power;
        Speed = speed;
    }

    public void SetTypeStats(CharacterType characterType)
    {
        this.characterType = characterType; // 캐릭터 타입 설정
        switch (characterType)
        {
            case Define.CharacterType.Normal:
                Life = 1;
                Power = 1;
                Speed = 1;
                break;
            case Define.CharacterType.Rare:
                Life = 2;
                Damage = 2;
                Speed = 2;
                break;
            case Define.CharacterType.Unique:
                Life = 3;
                Power = 3;
                Speed = 3;
                break;
            case Define.CharacterType.Epic:
                Life = 4;
                Power = 4;
                Speed = 4;
                break;
            default:
                break;
        }
    }
    public CharacterStats()
    {

    }

    public bool Damage(int damage)
    {
        Life += damage;

        Life = Mathf.Clamp(Life, 0, 5);


        if (Life <= 0)
        {
            // TOOD : 사망처리
            return true;
        }

        if (damage >= 0)
        {
            // TODO : 회복 처리
            return false;
        }
        else
        {
            // TODO : 데미지 처리
        }

        return true;
    }
}