using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("General")]
    public float turnSpeed = 20f;
    public Vector2 sensibility;
    float rotX;
    public bool notCanMove = false;

    public Transform playerCamera;
    public AudioSource m_AudioSource;
    Animator m_Animator;
    Rigidbody m_Rigidbody;
    Vector2 m_Movement;
    Vector2 moveRotacion;

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        if (!notCanMove)
        {
            Cursor.lockState = CursorLockMode.Locked;
            rotX = playerCamera.eulerAngles.x;
        }
    }

    void FixedUpdate()
    {
        if (!notCanMove)
        {
            MoveRotate();
        }
    }

    public void MoveRotate()
    {
        m_Movement.x = Input.GetAxis("Horizontal") * turnSpeed * Time.fixedDeltaTime;
        m_Movement.y = Input.GetAxis("Vertical") * turnSpeed * Time.fixedDeltaTime;

        bool hasHorizontalInput = !Mathf.Approximately(m_Movement.x, 0f);
        bool hasVerticalInput = !Mathf.Approximately(m_Movement.y, 0f);

        bool isWalking = hasHorizontalInput || hasVerticalInput;

        m_Animator.SetBool("isWalking", isWalking);

        if (isWalking)
        {
            if (!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }
        }
        else
        {
            m_AudioSource.Stop();
        }

        moveRotacion.x = Input.GetAxis("Mouse X") * sensibility.x;
        moveRotacion.y = Input.GetAxis("Mouse Y") * sensibility.y;

        transform.rotation *= Quaternion.Euler(0, moveRotacion.x, 0);

        RotateCamera();
    }

    private void OnAnimatorMove()
    {
        m_Rigidbody.velocity =
            transform.forward * m_Movement.y +
            transform.right * m_Movement.x +
            new Vector3(0, m_Rigidbody.velocity.y, 0);
    }

    public void RotateCamera()
    {
        rotX -= moveRotacion.y;
        rotX = Mathf.Clamp(rotX, -50, 50);
        playerCamera.localRotation = Quaternion.Euler(rotX, 0, 0);
    }
}
