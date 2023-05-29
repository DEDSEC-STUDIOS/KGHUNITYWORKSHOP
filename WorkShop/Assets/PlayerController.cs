using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] PlayerInput input;
    Vector2 currentMovement;
    bool runPressed;

    void Awake()
    {
        input = new PlayerInput();

        //store value of input in cM and check mP if input is there
        input.CharacterControls.Movement.performed += ctx =>
        {
            currentMovement = ctx.ReadValue<Vector2>();
          //movementPressed = currentMovement.x != 0 || currentMovement.y != 0;

        };
        input.CharacterControls.Run.performed += ctx => runPressed = ctx.ReadValueAsButton();


        // Check for Input Debug
         // input.CharacterControls.Movement.performed += ctx => Debug.Log(ctx.ReadValueAsObject());
        // input.CharacterControls.Run.performed += ctx => Debug.Log(ctx.ReadValueAsObject());
    }


    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        handleRotation();
    }

    private void LateUpdate()
    {
        Debug.Log(currentMovement);
    }

    void handleRotation()
    {
        Vector3 currentPostion = transform.position;

        Vector3 newPosition = new Vector3(currentMovement.x, 0, currentMovement.y);

        Vector3 positionToLookAt = currentPostion - newPosition;

        transform.LookAt(positionToLookAt);
    }

    void handleMovement()
    {

    }


    void OnEnable()
    {
        input.CharacterControls.Enable();

    }
    void OnDisable()
    {
        input.CharacterControls.Disable();

    }
}
