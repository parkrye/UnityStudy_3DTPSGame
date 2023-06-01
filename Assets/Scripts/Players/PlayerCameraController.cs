using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCameraController : MonoBehaviour
{
    enum CameraState { TPS, FPS }
    [SerializeField] CameraState state;
    [SerializeField] CinemachineVirtualCamera tpsCam, fpsCam;
    [SerializeField] Transform lookAtTarget, lookFromTarget, modelTransform, aimTargetTransform;
    [SerializeField] GameObject model;
    [SerializeField] [Range(10, 60)] float sensitivity;
    [SerializeField] float xRotation, yRotation;
    [SerializeField] Vector2 lookDelta;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        Rotate();
        Look();

        if (state == CameraState.TPS)
            modelTransform.LookAt(new Vector3(lookAtTarget.position.x, modelTransform.position.y, lookAtTarget.position.z));

        aimTargetTransform.position = lookAtTarget.position;
    }

    void Rotate()
    {
        transform.localRotation = Quaternion.Euler(0, yRotation, 0);
    }

    void Look()
    {
        lookFromTarget.localRotation = Quaternion.Euler(-xRotation, 0, 0);
    }

    void OnLook(InputValue inputValue)
    {
        lookDelta = inputValue.Get<Vector2>();
        yRotation += lookDelta.x * sensitivity * 0.5f * Time.deltaTime;
        xRotation += lookDelta.y * sensitivity * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
    }

    void OnChangeCamera()
    {
        if(state == CameraState.TPS)
        {
            state = CameraState.FPS;
            model.SetActive(false);
            tpsCam.Priority = 0;
            fpsCam.Priority = 1;
        }
        else
        {
            state = CameraState.TPS;
            model.SetActive(true);
            tpsCam.Priority = 1;
            fpsCam.Priority = 0;
        }
    }

    void OnZoom(InputValue inputValue)
    {
        if (inputValue.isPressed)
        {
            StopAllCoroutines();
            StartCoroutine(Zoom(15f));
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(Zoom(60f));
        }
    }

    IEnumerator Zoom(float dest)
    {
        if(fpsCam.m_Lens.FieldOfView > dest)
        {
            while (fpsCam.m_Lens.FieldOfView > dest)
            {
                fpsCam.m_Lens.FieldOfView -= 1f;
                tpsCam.m_Lens.FieldOfView -= 1f;

                yield return new WaitForSeconds(0.005f);
            }
        }
        else
        {
            while (fpsCam.m_Lens.FieldOfView < dest)
            {
                fpsCam.m_Lens.FieldOfView += 1f;
                tpsCam.m_Lens.FieldOfView += 1f;

                yield return new WaitForSeconds(0.005f);
            }
        }
    }
}
