using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;
    int isRunningHash;

    PlayerInput input;

    Vector2 currentMovement;
    bool movementPressed;
    bool runPressed;

    private void Awake()
    {
        input = new PlayerInput();

        //store value of input in cM and check mP if input is there
        input.CharacterControls.Movement.performed += ctx =>
        {
            currentMovement = ctx.ReadValue<Vector2>();
            movementPressed = currentMovement.x != 0 || currentMovement.y != 0;

        };
        input.CharacterControls.Run.performed += ctx => runPressed = ctx.ReadValueAsButton();


        // Check for Input Debug
       //  input.CharacterControls.Movement.performed += ctx => Debug.Log(ctx.ReadValueAsObject());
      // input.CharacterControls.Run.performed += ctx => Debug.Log(ctx.ReadValueAsObject());
    }

    // Start is called before the first frame update
    void Start()
    {
        animator= GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
    }

    // Update is called once per frame
    void Update()
    {
        handleMovement();
        handleRotation();
    }

    void handleRotation()
    {
        Vector3 currentPostion =  transform.position;

        Vector3 newPosition = new Vector3(currentMovement.x,0,currentMovement.y);

        Vector3 positionToLookAt = currentPostion - newPosition;

        transform.LookAt(positionToLookAt);
    }

    void handleMovement()
    {
        bool isRunning = animator.GetBool(isRunningHash);
        bool isWalking = animator.GetBool(isWalkingHash);

        //start walking if input
        if(movementPressed && !isWalking)
        {
            animator.SetBool(isWalkingHash, true);
        }

        //stop walking if input nada
        if (!movementPressed && isWalking)
        {
            animator.SetBool(isWalkingHash, false);
        }

        //start running if input
        if ((movementPressed && runPressed) && !isRunning)
        {
            animator.SetBool(isRunningHash, true);
        }

        //stop running input nada
        if ((!movementPressed || !runPressed) && isRunning)
        {
            animator.SetBool(isRunningHash, false);
        }
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
