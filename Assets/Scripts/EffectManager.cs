using UnityEngine;

public class EffectManager : MonoBehaviour
{
    // 이펙트 관련 변수 및 메서드 추가
    public void DamageEffect(string effectName)
    {
        // 이펙트 재생 로직 구현
        Debug.Log("피격 이펙트 생성");
    }
    public void LaunchEffect(string effectName)
    {
        Debug.Log("발사 이펙트 생성");

    }
    public void ItemAcquisitionEffect(string effectName)
    {
        Debug.Log("아이템 획득 이펙트 생성");
    }
}
