using UnityEngine;

public class CharacterStats
{
    public CharacterType characterType { get; private set; } // 수정: private set 추가
    public int Life { get; set; }
    public int Damage { get; set; }
    public int Speed { get; set; }

    public CharacterStats(CharacterType characterType, int life, int damage, int speed)
    {
        this.characterType = characterType;
        Life = life;
        Damage = damage;
        Speed = speed;
    }

    public void SetTypeStats(CharacterType characterType)
    {
        this.characterType = characterType; // 캐릭터 타입 설정
        switch (characterType)
        {
            case CharacterType.Normal:
                Life = 1;
                Damage = 1;
                Speed = 1;
                break;
            case CharacterType.Rare:
                Life = 2;
                Damage = 2;
                Speed = 2;
                break;
            case CharacterType.Unique:
                Life = 3;
                Damage = 3;
                Speed = 3;
                break;
            case CharacterType.Epic:
                Life = 4;
                Damage = 4;
                Speed = 4;
                break;
            default:
                break;
        }
    }
    public CharacterStats()
    {

    }
}
