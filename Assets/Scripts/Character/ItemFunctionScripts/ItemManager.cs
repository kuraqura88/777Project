using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemManager : MonoBehaviour
{
    CharacterStats characterStats;

    void Start()
    {
        if (characterStats == null )
        {
            Debug.Log("null");
        }
    }
    public void ApplyItemEffect (itemType itemType)
    {
        //switch (itemType)
        //{
        //    case itemType.AttackUp:
        //        characterStats.IncreaseAttackPower(2f); // 테스트로 일단 2만 상승
        //        break;
        //    case itemType.SpeedUp:
        //        characterStats.IncreaseSpeed(2f);
        //        break;
        //    case itemType.LifeUp:
        //        characterStats.IncreaseLife(1);
        //        break;
        //    default:
        //        Debug.Log("Unknown item type");
        //        break;
        //}
    }
}