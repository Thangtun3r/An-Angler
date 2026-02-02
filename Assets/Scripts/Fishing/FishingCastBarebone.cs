using UnityEngine;

public class FishingRodCaster : MonoBehaviour
{
    [Header("Refs")]
    public Transform rodTip;
    public Camera cam;
    public LineRenderer line;

    IBobber bobber;

    [Header("Distance Settings")]
    public float minDistance = 6f;
    public float maxDistance = 25f;

    [Header("Arc Settings")]
    public float minArcMultiplier = 0.15f;
    public float maxArcMultiplier = 0.6f;

    [Header("Timing")]
    public float castDuration = 1f;

    float t;
    bool casting;

    Vector3 start;
    Vector3 flatForward;
    float distance;
    float arcHeight;

    void Awake()
    {
        bobber = FindObjectOfType<Bobber>();
        bobber.OnHitGround += HandleBobberHit;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !casting)
            BeginCast();

        if (casting)
            UpdateCast();

        DrawLine();
    }
    
    Vector3 targetPos;

    void BeginCast()
    {
        bobber.ResetBobber();
        casting = true;
        t = 0f;

        start = rodTip.position;
        Vector3 forward = cam.transform.forward;
        float pitchFactor = Mathf.Clamp01(forward.y + 0.5f);

        distance = Mathf.Lerp(minDistance, maxDistance, pitchFactor);
        flatForward = Vector3.ProjectOnPlane(forward, Vector3.up).normalized;
    
        // Define exactly where the bobber SHOULD land if it hits nothing
        targetPos = start + (flatForward * distance);
    
        float arcMul = Mathf.Lerp(minArcMultiplier, maxArcMultiplier, pitchFactor);
        arcHeight = distance * arcMul;
    }

    void UpdateCast()
    {
        t += Time.deltaTime / castDuration;

        // 1. Move horizontally from start to targetPos based on t
        Vector3 currentPos = Vector3.Lerp(start, targetPos, t);

        // 2. Add the vertical arc height
        // Using a parabola (4 * t * (1-t)) is often cleaner than Sin for gravity feel
        float heightOffset = Mathf.Sin(Mathf.PI * Mathf.Clamp01(t)) * arcHeight;
        currentPos.y += heightOffset;

        bobber.SetPosition(currentPos);

        // Stop if the animation finishes even if no collision occurred
        if (t >= 1f)
        {
            casting = false;
        }
    }

    void HandleBobberHit(Vector3 hitPoint)
    {
        casting = false;
        bobber.SetPosition(hitPoint);
    }

    void DrawLine()
    {
        Vector3 p0 = rodTip.position;
        Vector3 p2 = ((MonoBehaviour)bobber).transform.position;

        Vector3 mid = (p0 + p2) * 0.5f;

        Vector3 p1;
        if (casting)
            p1 = mid + Vector3.up * Mathf.Sin(Mathf.PI * t) * 0.5f;
        else
            p1 = mid + Vector3.down * 0.6f;

        int segments = 20;
        line.positionCount = segments;

        for (int i = 0; i < segments; i++)
        {
            float tt = i / (segments - 1f);

            Vector3 point =
                Mathf.Pow(1 - tt, 2) * p0 +
                2 * (1 - tt) * tt * p1 +
                Mathf.Pow(tt, 2) * p2;

            line.SetPosition(i, point);
        }
    }
}
