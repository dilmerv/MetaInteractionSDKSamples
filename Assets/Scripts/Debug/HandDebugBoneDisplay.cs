using UnityEngine;

public class HandDebugBoneDisplay : MonoBehaviour
{
    [SerializeField]
    private OVRHand hand;

    [SerializeField]
    private OVRSkeleton handSkeleton;

    [SerializeField]
    private GameObject pointerPosePrefab;

    [SerializeField]
    private GameObject bonePrefab;

    private Transform pointerPose;

    private bool bonesAdded;

    private void Awake()
    {
        if (!hand) hand = GetComponent<OVRHand>();
        if (!handSkeleton) handSkeleton = GetComponent<OVRSkeleton>();
    }

    private void CreateBones()
    {
        foreach (var bone in handSkeleton.Bones)
        {
            Instantiate(bonePrefab, bone.Transform)
                .GetComponent<HandDebugBoneInfo>()
                .AddBone(bone);
        }

        bonesAdded = true;
    }

    void Update()
    {
        if(hand.IsTracked)
        {
            if (!pointerPose)
            {
                pointerPose = Instantiate(pointerPosePrefab).transform;
            }

            if (hand.IsPointerPoseValid)
            {
                pointerPose.position = hand.PointerPose.position;
                pointerPose.rotation = hand.PointerPose.rotation;
            }

            if (!bonesAdded) CreateBones();
        }
    }
}
