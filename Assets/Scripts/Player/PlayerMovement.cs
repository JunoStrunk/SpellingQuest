using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public bool playerCanMove;
    private Player playerInput;
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    [SerializeField]
    private float playerSpeed = 2.0f;
    private float gravityValue = -9.81f;

    private Transform camMain;

    private void Awake()
    {
        playerInput = new Player();
        controller = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        playerInput.Enable();
        playerInput.PlayerMain.Tap.performed += Tapped;
        playerCanMove = true;
    }

    private void OnDisable()
    {
        playerInput.Disable();
        playerInput.PlayerMain.Tap.performed -= Tapped;
        playerCanMove = false;
    }

    private void Tapped(InputAction.CallbackContext cxt)
    {
        Debug.Log("Getting tap");
    }

    public void CanPlayerMove(bool move)
    {
        playerCanMove = move;
    }


    private void Start()
    {
        camMain = Camera.main.transform;
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 movementInput = playerInput.PlayerMain.Move.ReadValue<Vector2>();
        Vector3 move = camMain.forward * movementInput.y + camMain.right * movementInput.x;
        move.y = 0f;
        if (playerCanMove)
            controller.Move(playerSpeed * Time.deltaTime * move);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        if (playerCanMove)
            controller.Move(playerVelocity * Time.deltaTime);
    }
}
