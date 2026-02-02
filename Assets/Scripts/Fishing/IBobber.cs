using UnityEngine;
using System;

public interface IBobber
{
    event Action<Vector3> OnHitGround;

    void SetPosition(Vector3 position);
    void ResetBobber();
}