using TMPro;
using UnityEngine;

public class HandDebugBoneInfo : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI boneText;

    private OVRBone bone;

    public void AddBone(OVRBone bone) => this.bone = bone;

    void Update()
    {
        if (bone == null) return;
        boneText.text = $"{bone.Id}";
        boneText.transform.rotation = Quaternion.LookRotation(boneText.transform.position - Camera.main.transform.position);
        transform.position = bone.Transform.position;
        transform.rotation = bone.Transform.rotation;
    }
}