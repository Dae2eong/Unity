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
        // 캐릭터가 지면에 있는 경우
        if (cc.isGrounded)
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");

            animator.SetBool("IsGround", true);

            moveDirection = new Vector3(horizontal, 0, vertical) * moveSpeed;

            if (moveDirection != Vector3.zero)
            {
                // 진행 방향으로 캐릭터 회전
                transform.rotation = Quaternion.Euler(0, Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg, 0);
                animator.SetBool("IsMove", true);
            }
            else
            {
                animator.SetBool("IsMove", false);
            }

            // Space 바 누르면 점프
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
            Debug.Log("충돌!");
        }
    }

    void UpdateTarget()
    {
        var map = GetMap();
        var monster = map.GetNearestMonster(this.transform.position);

        int monsterHandle;
        if (monster == null)
        {
            monsterHandle = 0; // 근처에 몬스터가 없으면 0
        }
        else
        {
            monsterHandle = monster.Handle; // 몬스터가 있으면 해당 몬스터의 핸들 값
        }

        if (targetHandle != monsterHandle)
        {
            // 이전 타겟의 아웃라인을 제거
            var prevTarget = map.GetMonster(targetHandle);
            if (prevTarget != null)
            {
                var outline = prevTarget.GetComponent<Outline>();
                if (outline != null)
                {
                    outline.enabled = false;
                }
            }
            // 새로운 타겟의 아웃라인을 표시
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