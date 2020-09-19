using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPC : MonoBehaviour
{
    private CharacterController controller;
    public new Camera camera;
    public Transform groundTransform;
    public LayerMask groundLayer;

    public float sensitivity;
    public float speed;

    private float rotation = 0f;
    private Vector3 velocity = Vector3.zero;
    private float gravity = -9.81f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    private void Update()
    {
        float x = Input.GetAxis(InputStrings.MouseX) * sensitivity * Time.deltaTime;
        float y = Input.GetAxis(InputStrings.MouseY) * sensitivity * Time.deltaTime;

        controller.transform.Rotate(Vector3.up * x);

        rotation += y;

        rotation = Mathf.Clamp(rotation, -88f, 90f);
        camera.transform.localRotation = Quaternion.Euler(-rotation, 0f, 0f);

        float moveX = Input.GetAxis(InputStrings.HorizontalAxis);
        float moveZ = Input.GetAxis(InputStrings.VerticalAxis);

        Vector3 moveVector = controller.transform.forward * moveZ +
            controller.transform.right * moveX;

        moveVector *= speed * Time.deltaTime;

        if (IsGrounded() && velocity.y < 0)
        {
            velocity.y = 0;
        }

        velocity.y += gravity * Time.deltaTime * Time.deltaTime;

        if (Input.GetButtonDown(InputStrings.Jump) && IsGrounded())
        {
            velocity.y = 0.1f;
        }

        controller.Move(moveVector + velocity);
    }

    private bool IsGrounded()
    {
        //Debug.Log(groundTransform.position + " ground position");
        //Debug.Log(groundLayer + " ground layer");
        return Physics.CheckSphere(groundTransform.position, 0.5f, groundLayer);
    }
}

public struct InputStrings
{
    public static string MouseX = "Mouse X";
    public static string MouseY = "Mouse Y";
    public static string HorizontalAxis = "Horizontal";
    public static string VerticalAxis = "Vertical";
    public static string Jump = "Jump";
    public static string Fire = "Fire1";
}