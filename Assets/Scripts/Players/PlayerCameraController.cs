using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCameraController : MonoBehaviour
{
    enum CameraState { TPS, FPS }
    [SerializeField] CameraState state;

    [SerializeField] CinemachineVirtualCamera tpsCam, fpsCam;
    [SerializeField] Transform lookAtTarget, lookFromTarget, modelTransform;
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
    }

    void Rotate()
    {
        transform.localRotation = Quaternion.Euler(0, yRotation, 0);
        if (state == CameraState.TPS)
        {
            modelTransform.LookAt(new Vector3(lookAtTarget.position.x, modelTransform.position.y, lookAtTarget.position.z));
        }
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
}
