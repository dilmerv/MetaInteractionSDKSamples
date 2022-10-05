using UnityEngine;

public class HandDebugSkeletonInfo : MonoBehaviour
{
    [SerializeField]
    private OVRHand hand;

    [SerializeField]
    private OVRSkeleton handSkeleton;

    [SerializeField]
    private HandInfoFrequency handInfoFrequency = HandInfoFrequency.Once;

    private bool handInfoDisplayed = false;

    private bool pauseDisplay = false;

    private void Awake()
    {
        if (!hand) hand = GetComponent<OVRHand>();
        if (!handSkeleton) handSkeleton = GetComponent<OVRSkeleton>();
    }

    private void DisplayBoneInfo()
    {
        foreach (var bone in handSkeleton.Bones)
        {
            Logger.Instance.LogInfo($"{handSkeleton.GetSkeletonType()}: boneId -> {bone.Id} pos -> {bone.Transform.position}");
        }

        Logger.Instance.LogInfo($"{handSkeleton.GetSkeletonType()} num of bones: {handSkeleton.GetCurrentNumBones()}");
        Logger.Instance.LogInfo($"{handSkeleton.GetSkeletonType()} num of skinnable bones: {handSkeleton.GetCurrentNumSkinnableBones()}");
        Logger.Instance.LogInfo($"{handSkeleton.GetSkeletonType()} start bone id: {handSkeleton.GetCurrentStartBoneId()}");
        Logger.Instance.LogInfo($"{handSkeleton.GetSkeletonType()} end bone id: {handSkeleton.GetCurrentEndBoneId()}");
    }
      
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) pauseDisplay = !pauseDisplay;

        if(hand.IsTracked && !pauseDisplay)
        {
            if (handInfoFrequency == HandInfoFrequency.Once && !handInfoDisplayed)
            {
                DisplayBoneInfo();
                handInfoDisplayed = true;
            }
            else if (handInfoFrequency == HandInfoFrequency.Repeat)
            {
                DisplayBoneInfo();
            }
        }
    }
}
