using UnityEngine;

namespace Fishing // Added namespace to fix your IDE warning
{
    public class FishingCast : MonoBehaviour
    {
        [Header("References")]
        public Rigidbody bobberRT;
        public Bobber bobber;
        // HingeJoint removed
        public Transform head;
        public Transform rodHead;
        public LineRenderer line;

        [Header("Tuning")]
        public float castForce = 15f;
        public float reelSpeed = 12f;

        private bool hasCasted;
        private bool canReel;
        private bool isReeling;

        private void OnEnable()
        {
            Bobber.OnBobberLanded += EnableReel;
        }

        private void OnDisable()
        {
            Bobber.OnBobberLanded -= EnableReel;
        }

        private void Start()
        {
            line.positionCount = 2;
            line.useWorldSpace = true;

            // Start correctly attached to rod
            AttachToRodIdle();
        }

        private void Update()
        {
            IdleFollowRod();
            CastRod();
            StartReel();
            ReelMovement();
            UpdateLine();
        }

        private void CastRod()
        {
            if (Input.GetMouseButtonDown(0) && !hasCasted)
            {
                hasCasted = true;
                canReel = false;
                isReeling = false;
                bobber.ResetBobber();

                // Deparent and position
                bobberRT.transform.parent = null;
                bobberRT.transform.position = head.position;

                // Reset physics and launch
                bobberRT.velocity = Vector3.zero;
                bobberRT.angularVelocity = Vector3.zero;
                bobberRT.isKinematic = false;

                bobberRT.AddForce(head.forward * castForce, ForceMode.Impulse);
            }
        }

        private void EnableReel()
        {
            if (hasCasted)
            {
                canReel = true;
            }
        }

        private void StartReel()
        {
            if (Input.GetMouseButtonDown(1) && canReel && !isReeling)
            {
                // Try to catch fish (optional, does not block reel)
                if (bobber.currentFish != null && bobber.currentFish.IsBiting())
                {
                    bobber.currentFish.TryCatchFish(bobberRT.transform);
                }

                isReeling = true;
                bobberRT.isKinematic = true;
            }
        }



        private void ReelMovement()
        {
            if (!isReeling) return;

            bobberRT.transform.position = Vector3.MoveTowards(
                bobberRT.transform.position,
                rodHead.position,
                reelSpeed * Time.deltaTime
            );

            if (Vector3.Distance(bobberRT.transform.position, rodHead.position) < 0.05f)
            {
                isReeling = false;
                hasCasted = false;
                canReel = false;

                AttachToRodIdle();
            }
        }

        private void IdleFollowRod()
        {
            if (!hasCasted && !isReeling)
            {
                AttachToRodIdle();
            }
        }

        private void AttachToRodIdle()
        {
            bobberRT.isKinematic = true;
            bobberRT.transform.parent = rodHead;
            bobberRT.transform.localPosition = Vector3.zero;
            bobberRT.transform.localRotation = Quaternion.identity;
        }

        private void UpdateLine()
        {
            line.SetPosition(0, rodHead.position);
            line.SetPosition(1, bobberRT.transform.position);
        }
    }
}