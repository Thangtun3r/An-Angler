using System;
using UnityEngine;

public class Bobber : MonoBehaviour
{
    public static event Action OnBobberLanded;
    
    public IFish currentFish;

    public float groundCheckRadius = 0.2f;
    public float groundCheckDistance = 0.3f;
    public LayerMask groundLayer;
    private Rigidbody rb;

    private bool hasLanded;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (hasLanded) return;

        if (Physics.SphereCast(transform.position, groundCheckRadius, Vector3.down,
                out RaycastHit hit, groundCheckDistance, groundLayer))
        {
            rb.isKinematic = true;
            hasLanded = true;
            OnBobberLanded?.Invoke();
            currentFish = hit.collider.GetComponent<IFish>();

            if (currentFish != null)
            {
                currentFish.BobberLanded(transform);
            }
        }
    }


    public void ResetBobber()
    {
        rb.isKinematic = false;
        hasLanded = false;
        currentFish = null;  
    }


    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Vector3 start = transform.position;
        Vector3 end = start + Vector3.down * groundCheckDistance;

        // Start sphere
        Gizmos.DrawWireSphere(start, groundCheckRadius);

        // End sphere
        Gizmos.DrawWireSphere(end, groundCheckRadius);

        // Lines connecting spheres (visualize the cast)
        Gizmos.DrawLine(start + Vector3.forward * groundCheckRadius, end + Vector3.forward * groundCheckRadius);
        Gizmos.DrawLine(start - Vector3.forward * groundCheckRadius, end - Vector3.forward * groundCheckRadius);
        Gizmos.DrawLine(start + Vector3.right * groundCheckRadius, end + Vector3.right * groundCheckRadius);
        Gizmos.DrawLine(start - Vector3.right * groundCheckRadius, end - Vector3.right * groundCheckRadius);

        // Center line
        Gizmos.DrawLine(start, end);
    }
}