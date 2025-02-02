using UnityEngine;

public class Monster : MonoBehaviour
{
    static int monsterHandle = 0; //static 이라 모든 몬스터가 공유, 각각 고유번호 생성

    public int Handle { get; private set; }
    public float hp = 100;

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable() // MonoBehaviour 에서 제공하는 생명주기 메서드
    {
        monsterHandle++; // 몬스터마다 서로다른 Handle을 가지고 있음
        Handle = monsterHandle;

        // 첫 번째 몬스터 Handle == 1
        // 두 번째 몬스터 Handle == 2 ...
    }

    public void TakeDamage(float damage)
    {
        animator.SetBool("IsGetHit", true);

        CancelInvoke("ResetGetHit");
        Invoke("ResetGetHit", 0.5f);

        hp -= damage;
        Debug.Log($"몬스터 남은 HP : {hp}");
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
