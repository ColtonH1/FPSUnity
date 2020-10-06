using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    Level01Controller level01Controller = new Level01Controller();

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    //bullet
    [SerializeField] ParticleSystem _gunParticles;
    [SerializeField] AudioClip _gunSound;


    // Update is called once per frame
    void Update()
    {
        float currentSpeed = speed;
        Jumping();

        currentSpeed = Running(currentSpeed);

        Moving(currentSpeed);

        Shooting();
    }

    private void Shooting()
    {
        if (Input.GetMouseButtonDown(0) && !level01Controller.IsPaused())
        {
            _gunParticles.Clear();
            AudioManager.Instance.PlaySong(_gunSound);
            _gunParticles.Play();
        }
    }

    private float Running(float currentSpeed)
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed *= 2;
        }
        else
        {
            currentSpeed = speed;
        }

        return currentSpeed;
    }

    private void Moving(float currentSpeed)
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * currentSpeed * Time.deltaTime);

        controller.Move(velocity * Time.deltaTime);
    }

    private void Jumping()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
    }
}
