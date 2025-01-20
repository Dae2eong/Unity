using UnityEngine;

public class Monster : MonoBehaviour
{
    static int monsterHandle = 0; //static �̶� ��� ���Ͱ� ����, ���� ������ȣ ����

    public int Handle { get; private set; }
    private void OnEnable() // MonoBehaviour ���� �����ϴ� �����ֱ� �޼���
    {
        monsterHandle++; // ���͸��� ���δٸ� Handle�� ������ ����
        Handle = monsterHandle;

        // ù ��° ���� Handle == 1
        // �� ��° ���� Handle == 2 ...
    }
}
