using UnityEngine;
using System.Collections;

public class FishContainer : MonoBehaviour, IFish
{
    public FishSO fishSO;

    [Header("Bite Timing")]
    public float minWaitTime = 2f;
    public float maxWaitTime = 5f;
    public float biteWindow = 1.5f;

    private bool fishIsBiting;
    private Transform currentBobber;

    public void BobberLanded(Transform bobber)
    {
        currentBobber = bobber;
        StartCoroutine(FishBiteRoutine());
    }

    public bool IsBiting()
    {
        return fishIsBiting;
    }

    IEnumerator FishBiteRoutine()
    {
        float wait = Random.Range(minWaitTime, maxWaitTime);
        yield return new WaitForSeconds(wait);

        fishIsBiting = true;
        Debug.Log("Fish is biting!");

        yield return new WaitForSeconds(biteWindow);

        fishIsBiting = false;
        Debug.Log("Fish got away...");
    }

    public bool TryCatchFish(Transform bobber)
    {
        if (!fishIsBiting) return false;

        Instantiate(fishSO.fishprefab, bobber.position, Quaternion.identity);
        fishIsBiting = false;
        return true;
    }
}