using UnityEngine;
using UnityEngine.AI;

public class MoveController : MonoBehaviour
{
    NavMeshAgent agent;

    CharacterController cc;
    Animator animator;

    public float moveSpeed = 5.0f;
    public Weapon weapon;

    int targetHandle;

    float horizontal;
    float vertical;
    Vector3 moveDirection;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // ĳ���Ͱ� ���鿡 �ִ� ���
        if (cc.isGrounded)
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");

            animator.SetBool("IsGround", true);

            moveDirection = new Vector3(horizontal, 0, vertical) * moveSpeed;

            if (moveDirection != Vector3.zero)
            {
                // ���� �������� ĳ���� ȸ��
                transform.rotation = Quaternion.Euler(0, Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg, 0);
                animator.SetBool("IsMove", true);
            }
            else
            {
                animator.SetBool("IsMove", false);
            }

            // Space �� ������ ����
            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetBool("IsGround", false);
                moveDirection.y = 7.5f;
            }
        }

        moveDirection.y += Physics.gravity.y * Time.deltaTime;
        cc.Move(moveDirection * Time.deltaTime);

        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }

        UpdateTarget();
    }

    void Attack()
    {
        int result = Random.Range(1, 4);
        animator.SetBool("IsAttack", true);
        animator.SetInteger("AttackNumber", result);

        weapon.StartAttack();

        Invoke("EndAttack", 0.5f);
    }

    void EndAttack()
    {
        weapon.EndAttack();
        animator.SetBool("IsAttack", false);
    }

    void UpdateTarget()
    {
        var map = GetMap();
        var monster = map.GetNearestMonster(this.transform.position);

        int monsterHandle;
        if (monster == null)
        {
            monsterHandle = 0; // ��ó�� ���Ͱ� ������ 0
        }
        else
        {
            monsterHandle = monster.Handle; // ���Ͱ� ������ �ش� ������ �ڵ� ��
        }

        if (targetHandle != monsterHandle)
        {
            MonsterHpUI.Instance?.SetMonster(monster); // ?.�� Instance�� null�̸� �������� ����

            // ���� Ÿ���� �ƿ������� ����
            var prevTarget = map.GetMonster(targetHandle);
            if (prevTarget != null)
            {
                var outline = prevTarget.GetComponent<Outline>();
                if (outline != null)
                {
                    outline.enabled = false;
                }
            }
            // ���ο� Ÿ���� �ƿ������� ǥ��
            if (monster != null)
            {
                var outline = monster.GetComponent<Outline>();
                if (outline != null)
                {
                    outline.enabled = true;
                }
            }

            targetHandle = monsterHandle;
        }
    }

    Map GetMap()
    {
        var map = GameObject.Find("Map");
        if (map != null)
        {
            return map.GetComponent<Map>();
        }

        return null;
    }
}