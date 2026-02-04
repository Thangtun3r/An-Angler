using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerMovement movement;
    public CharacterController characterController;

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        LockMouse();
    }

    public void FreezeMovementOnly()
    {
        movement.IsFrozen = true;       
    }

    public void UnFreezeMovementOnly()
    {
        movement.IsFrozen = false;
    }
    
    
    public void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void UnlockMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}