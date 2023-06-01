using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] CharacterController characterController;
    [SerializeField] Animator animator;
    [SerializeField] Transform lookFromTransform;
    [SerializeField] CapsuleCollider hitCapsule;
    [SerializeField] Vector2 moveDir;
    [SerializeField] float nowSpeed, crawlSpeed, walkSpeed, runSpeed, ySpeed, jumpSpeed;

    void Start()
    {
        nowSpeed = walkSpeed;
        animator.SetFloat("Speed", 3f);
    }

    void FixedUpdate()
    {
        XZMove();
        YMove();
    }

    void XZMove()
    {
        characterController.Move((transform.forward * moveDir.y + transform.right * moveDir.x) * nowSpeed * Time.deltaTime);
        if(animator.GetFloat("Speed") < nowSpeed)
            animator.SetFloat("Speed", animator.GetFloat("Speed") + 2f * Time.deltaTime);
        else if (animator.GetFloat("Speed") > nowSpeed)
            animator.SetFloat("Speed", animator.GetFloat("Speed") - 2f * Time.deltaTime);
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
            ySpeed = jumpSpeed;
            animator.SetTrigger("Jump");
        }
    }

    void OnCrawl(InputValue inputValue)
    {
        if (inputValue.isPressed)
        {
            StopAllCoroutines();
            StartCoroutine(Sit(1f));
            nowSpeed = crawlSpeed;
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(Sit(1.5f));
            nowSpeed = walkSpeed;
        }
    }

    void OnRun(InputValue inputValue)
    {
        StopAllCoroutines();
        StartCoroutine(Sit(1.5f));
        if (inputValue.isPressed)
            nowSpeed = runSpeed;
        else
            nowSpeed = walkSpeed;
    }

    bool GroundCheck()
    {
        RaycastHit hit;
        if(Physics.SphereCast(transform.position + transform.up, 0.3f, -transform.up, out hit, 1f, LayerMask.GetMask("Ground")))
            animator.SetBool("Ground", true);
        else
            animator.SetBool("Ground", false);
        return animator.GetBool("Ground");
    }

    IEnumerator Sit(float dest)
    {
        if (lookFromTransform.localPosition.y > dest)
        {
            while (lookFromTransform.localPosition.y > dest)
            {
                lookFromTransform.Translate(-Vector3.up * 0.01f);
                hitCapsule.height -= 0.012f;
                hitCapsule.center += new Vector3(0f, 0.012f, 0.004f);
                yield return new WaitForSeconds(0.005f);
            }
        }
        else
        {
            while (lookFromTransform.localPosition.y < dest)
            {
                lookFromTransform.Translate(Vector3.up * 0.01f);
                hitCapsule.height += 0.03f;
                hitCapsule.center -= new Vector3(0f, 0.012f, 0.004f);
                yield return new WaitForSeconds(0.005f);
            }
        }
    }
}
