using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public bool playerCanMove;
    public bool inTut = false;
    private Player playerInput;
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    [SerializeField]
    private float playerSpeed = 2.0f;
    private float gravityValue = -9.81f;

    private Transform camMain;
    private Animator anim;

    private void Awake()
    {
        playerInput = new Player();
        controller = GetComponent<CharacterController>();
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    private void OnEnable()
    {
        playerInput.Enable();
        playerInput.PlayerMain.Tap.performed += Tapped;
        // playerCanMove = true;
    }

    private void OnDisable()
    {
        playerInput.Disable();
        playerInput.PlayerMain.Tap.performed -= Tapped;
        playerCanMove = false;
    }

    private void Tapped(InputAction.CallbackContext cxt)
    {
        UIEventManager.current.PlayerTap();
    }

    public void CanPlayerMove(bool move)
    {
        playerCanMove = move;
    }


    private void Start()
    {
        if (!inTut)
            camMain = Camera.main.transform;
    }

    void Update()
    {
        if (!playerCanMove)
            return;
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 movementInput = playerInput.PlayerMain.Move.ReadValue<Vector2>();
        Vector3 move = camMain.forward * movementInput.y + camMain.right * movementInput.x;
        move.y = 0f;
        controller.Move(playerSpeed * Time.deltaTime * move);

        if (move != Vector3.zero)
        {
            anim.SetBool("Moving", true);
            gameObject.transform.forward = move;
        }
        else
            anim.SetBool("Moving", false);

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
