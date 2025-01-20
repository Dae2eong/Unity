using UnityEngine;
using UnityEngine.AI;

public class MoveController : MonoBehaviour
{
    NavMeshAgent agent;

    CharacterController cc;
    Animator animator;

    public float moveSpeed = 5f;

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
            int result = Random.Range(1, 4);
            animator.SetBool("IsAttack", true);
            animator.SetInteger("AttackNumber", result);
        }
        else
        {
            animator.SetBool("IsAttack", false);
        }

        UpdateTarget();
    }

    private void OnCollisionEnterHit(ControllerColliderHit hit)
    {
        if (hit.rigidbody)
        {
            Debug.Log("�浹!");
        }
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