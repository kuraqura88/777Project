using UnityEngine;

public class EffectManager : MonoBehaviour
{
    // ����Ʈ ���� ���� �� �޼��� �߰�
    public void DamageEffect(string effectName)
    {
        // ����Ʈ ��� ���� ����
        Debug.Log("�ǰ� ����Ʈ ����");
    }
    public void LaunchEffect(string effectName)
    {
        Debug.Log("�߻� ����Ʈ ����");

    }
    public void ItemAcquisitionEffect(string effectName)
    {
        Debug.Log("������ ȹ�� ����Ʈ ����");
    }
}
