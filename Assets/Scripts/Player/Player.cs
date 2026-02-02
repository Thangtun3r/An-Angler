using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerMovement movement;
    private PlayerInteraction interaction;
    public CharacterController characterController;

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        interaction = GetComponent<PlayerInteraction>();
        characterController = GetComponent<CharacterController>();
    }


    private void Start()
    {
        LockMouse();
    }


    private void DisablePlayer()
    {
        movement.enabled = false;
        interaction.enabled = false;
        characterController.enabled = false;
    }

    private void EnablePlayer()
    {
        movement.enabled = true;
        interaction.enabled = true;
        characterController.enabled = true;
        
    }

    private void FreezeMovementOnly()
    {
        movement.IsFrozen = true;       
    }

    private void UnFreezeMovementOnly()
    {
        movement.IsFrozen = false;
    }
    
    
    private void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void SetPlayerSpawnPoint(Transform spawnPoint)
    {
        
        // Disable CharacterController to allow position changes
        characterController.enabled = false;
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;
        movement.ResetHead();
        
        // Re-enable CharacterController
        characterController.enabled = true;
    }
}