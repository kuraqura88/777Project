using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // 사운드 관련 변수 및 메서드 추가
    public void LaunchSound(string soundName)
    {
        // 사운드 재생 로직 구현
        Debug.Log("이펙트 생성");
    }
    public void BackGroundSound(string soundName)
    {
        Debug.Log("배경음악 생성");

    }
    public void DamageSound(string soundName)
    {
        Debug.Log("피격 사운드 생성");

    }

    public void SetVolume(float volume)
    {
        // 사운드 볼륨 조절 로직 구현
        Debug.Log("사운드 볼륨 조절 생성");
    }
}
