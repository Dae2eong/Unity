using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float attackDamage = 10.0f;
    public Transform hitBoxPosition;
    public float hitBoxRadius = 1.5f;
    public LayerMask monsterLayer; // ���� ��� ����

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
        // �迭�� ����ϴ� ���� : ���� ���� ���� ���� ��ü�� ���� ���

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
