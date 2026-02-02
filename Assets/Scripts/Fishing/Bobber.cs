using UnityEngine;
using System;

public class Bobber : MonoBehaviour, IBobber
{
    public event Action<Vector3> OnHitGround;

    public float checkRadius = 0.15f;
    public float checkDistance = 0.3f;
    public LayerMask hitMask;

    bool hasHit;

    void Update()
    {
        if (hasHit) return;

        RaycastHit hit;
        if (Physics.SphereCast(transform.position, checkRadius, Vector3.down,
                out hit, checkDistance, hitMask))
        {
            hasHit = true;
            OnHitGround?.Invoke(hit.point);
        }
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void ResetBobber()
    {
        hasHit = false;
    }
}