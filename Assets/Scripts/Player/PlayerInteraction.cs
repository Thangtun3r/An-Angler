using System;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float raycastDistance = 5f;

    private Camera mainCamera;
    private IPlayerInteraction current;

    [HideInInspector] public bool isTalking = false;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (!isTalking)
        {
            HandleInteraction();
        }
    }

       private void HandleInteraction()
    {
        // Get the ray from the center of the screen
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        Debug.DrawRay(ray.origin, ray.direction * raycastDistance, Color.red);

        if (Physics.Raycast(ray, out RaycastHit hit, raycastDistance))
        {
            IPlayerInteraction interactable = hit.collider.GetComponent<IPlayerInteraction>();

            if (interactable != current)
            {
                current?.Unhighlight();
                current = interactable;
                current?.Highlight();
            }

            if (current != null && Input.GetKeyDown(KeyCode.E))
            {
                current.Interact();
            }
        }
        else
        {
            current?.Unhighlight();
            current = null;
        }
    }
}