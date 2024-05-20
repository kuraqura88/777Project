using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public CharacterType characterType { get; private set; }
    public int Life { get; set; }
    public float Damage { get; set; }
    public float Speed { get; set; }

    public void Initialize(CharacterType characterType, int life, float damage, float speed) // 생성자 된다
    {
        this.characterType = characterType;
        Life = life;
        Damage = damage;
        Speed = speed;
    }

    public void SetTypeStats(CharacterType characterType)
    {
        this.characterType = characterType;

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
    public void IncreaseAttackPower(float amount)
    {
        Damage += amount;
        Debug.Log("Attack Power increased: " + Damage);
    }

    public void IncreaseSpeed(float amount)
    {
        Speed += amount;
        Debug.Log("Speed increased: " + Speed);
    }

    public void IncreaseLife(int amount)
    {
        Life += amount;
        Debug.Log("Life increased: " + Life);
    }

    //public void IncreaseProjectileCount(int amount)
    //{
    //    projectileCount += amount;
    //    Debug.Log("Projectile Count increased: " + projectileCount);
    //}
}