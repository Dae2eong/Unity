using UnityEngine;

public class Monster : MonoBehaviour
{
    static int monsterHandle = 0; //static �̶� ��� ���Ͱ� ����, ���� ������ȣ ����

    public int Handle { get; private set; }
    public float hp = 100;

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable() // MonoBehaviour ���� �����ϴ� �����ֱ� �޼���
    {
        monsterHandle++; // ���͸��� ���δٸ� Handle�� ������ ����
        Handle = monsterHandle;

        // ù ��° ���� Handle == 1
        // �� ��° ���� Handle == 2 ...
    }

    public void TakeDamage(float damage)
    {
        animator.SetBool("IsGetHit", true);

        CancelInvoke("ResetGetHit");
        Invoke("ResetGetHit", 0.5f);

        hp -= damage;
        Debug.Log($"���� ���� HP : {hp}");
        if (hp <= 0)
        {
            Die();
        }
    }

    void ResetGetHit()
    {
        animator.SetBool("IsGetHit", false);
    }

    void Die()
    {
        //Map map = FindObjectOfType<Map>();

        animator.SetBool("IsDie", true);

        if (Map.Instance != null)
        {
            
            Map.Instance.RemoveMonster(this);
        }
        Invoke("DestroyObject", 3.0f);
    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }
}
