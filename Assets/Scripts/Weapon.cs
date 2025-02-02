using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float attackDamage = 10.0f;
    public Transform hitBoxPosition;
    public float hitBoxRadius = 1.5f;
    public LayerMask monsterLayer; // 공격 대상 지정

    private bool isAttack = false;

    

    public void StartAttack()
    {
        isAttack = true;
        Hit();
    }

    public void EndAttack()
    {
        isAttack = false;
    }

    private void Hit()
    {
        if (!isAttack)
        {
            return;
        }

        Collider[] hitMonsters = Physics.OverlapSphere(hitBoxPosition.position, hitBoxRadius, monsterLayer);
        // 배열을 사용하는 이유 : 공격 범위 내에 여러 객체가 있을 경우

        foreach (Collider hit in hitMonsters)
        {
            Monster monster = hit.GetComponent<Monster>();
            if (monster != null) 
            {
                monster.TakeDamage(attackDamage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (hitBoxPosition != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(hitBoxPosition.position, hitBoxRadius);
        }
    }
}
