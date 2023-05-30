using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] CharacterController characterController;
    [SerializeField] Animator animator;

    enum MoveState { Crawl, Walk, Run }
    [SerializeField] MoveState moveState;
    [SerializeField] Vector2 moveDir;
    [SerializeField] float nowSpeed, crawlSpeed, walkSpeed, runSpeed, ySpeed, jumpSpeed;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        moveState = MoveState.Walk;
        nowSpeed = walkSpeed;
    }

    void FixedUpdate()
    {
        XZMove();
        YMove();
    }

    void XZMove()
    {
        characterController.Move((transform.forward * moveDir.y + transform.right * moveDir.x) * nowSpeed * Time.deltaTime);
    }

    void YMove()
    {
        if (GroundCheck() && ySpeed < 0)
            ySpeed = -1f;
        else
            ySpeed -= 0.5f;

        characterController.Move(transform.up * ySpeed * Time.deltaTime);
    }

    void OnMove(InputValue inputValue)
    {
        moveDir = inputValue.Get<Vector2>();
        animator.SetFloat("Foward", moveDir.y);
        animator.SetFloat("Sideward", moveDir.x);
        if (moveDir == Vector2.zero)
            animator.SetBool("Move", false);
        else
            animator.SetBool("Move", true);
    }

    void OnJump()
    {
        if (GroundCheck())
        {
            animator.SetTrigger("Jump");
            ySpeed = jumpSpeed;
        }
    }

    void OnCrawl()
    {
        if(moveState == MoveState.Crawl)
        {
            animator.SetBool("Crawl", false);
            moveState = MoveState.Walk;
            nowSpeed = walkSpeed;
        }
        else if(moveState == MoveState.Walk)
        {
            animator.SetBool("Run", true);
            moveState = MoveState.Run;
            nowSpeed = runSpeed;
        }
        else
        {
            animator.SetBool("Run", false);
            animator.SetBool("Crawl", true);
            moveState = MoveState.Crawl;
            nowSpeed = crawlSpeed;
        }
    }

    bool GroundCheck()
    {
        RaycastHit hit;
        if(Physics.SphereCast(transform.position + transform.up, 0.3f, -transform.up, out hit, 0.8f, LayerMask.GetMask("Ground")))
        {
            animator.SetBool("Ground", true);
        }
        else
        {
            animator.SetBool("Ground", false);
        }
        return animator.GetBool("Ground");
    }
}
