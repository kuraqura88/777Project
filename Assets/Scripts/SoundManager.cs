using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // ���� ���� ���� �� �޼��� �߰�
    public void LaunchSound(string soundName)
    {
        // ���� ��� ���� ����
        Debug.Log("����Ʈ ����");
    }
    public void BackGroundSound(string soundName)
    {
        Debug.Log("������� ����");

    }
    public void DamageSound(string soundName)
    {
        Debug.Log("�ǰ� ���� ����");

    }

    public void SetVolume(float volume)
    {
        // ���� ���� ���� ���� ����
        Debug.Log("���� ���� ���� ����");
    }
}
