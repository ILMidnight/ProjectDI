using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    

    [Header("Input Values")]
    public Vector2 movementInput;
    public bool jumpInput;
    [Space]

    [Header("Models")]
    public GameObject playerModel;
    
    PlayerMovements pMovement;

    private void Awake()
    {
        pMovement = gameObject.AddComponent<PlayerMovements>();
        pMovement.player = this;
    }

    private void Update()
    {
        Input();
    }

    private void Input()
    {
        movementInput.x = UnityEngine.Input.GetAxisRaw("Horizontal");
        movementInput.y = UnityEngine.Input.GetAxisRaw("Vertical");
        movementInput = Vector2.ClampMagnitude(movementInput, 1f);

        jumpInput |= UnityEngine.Input.GetKey(KeyCode.Z);
    }
}
