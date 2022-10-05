using UnityEngine;

public class HandDebugInteraction : MonoBehaviour
{
    [SerializeField]
    private OVRHand hand;

    private bool pauseDisplay = false;

    private void Awake()
    {
        if (!hand) hand = GetComponent<OVRHand>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) pauseDisplay = !pauseDisplay;

        if (hand.IsTracked && !pauseDisplay)
        {
            Logger.Instance.LogInfo($"{OVRHand.HandFinger.Index} " +
                $"is pinching: {hand.GetFingerIsPinching(OVRHand.HandFinger.Index)}");

            Logger.Instance.LogInfo($"{OVRHand.HandFinger.Middle} " +
                $"is pinching: {hand.GetFingerIsPinching(OVRHand.HandFinger.Middle)}");

            Logger.Instance.LogInfo($"{OVRHand.HandFinger.Index} " +
                $"confidence is: {hand.GetFingerConfidence(OVRHand.HandFinger.Index)}");

            Logger.Instance.LogInfo($"{OVRHand.HandFinger.Ring} " +
                $"confidence is: {hand.GetFingerConfidence(OVRHand.HandFinger.Ring)}");

            Logger.Instance.LogInfo($"{OVRHand.HandFinger.Index} " +
                $"strength is: {hand.GetFingerPinchStrength(OVRHand.HandFinger.Index)}");

            Logger.Instance.LogInfo($"{OVRHand.HandFinger.Ring} " +
                $"strength is: {hand.GetFingerPinchStrength(OVRHand.HandFinger.Ring)}");
        }
    }
}