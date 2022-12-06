using Oculus.Interaction;
using Oculus.Interaction.Surfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RaysDemosUIManager : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;

    [SerializeField]
    private RaysDemosUISettings floatingUISettings;

    [SerializeField]
    private RaysDemosUISettings onTableUISettings;

    [SerializeField]
    private RaysDemosUISettings onTableLeftUISettings;

    [SerializeField]
    private RaysDemosUISettings onTableRightUISettings;

    [SerializeField]
    private GameObject new3DObjectParent;

    [SerializeField]
    private TextMeshProUGUI ui3DObjectStatus;

    [SerializeField]
    private Toggle randomColorToggle;

    private UI3DObject ui3DObjectSelected = UI3DObject.Cube;

    public enum UIMode
    {
        Floating,
        OnTable,
        OnTableLeft,
        OnTableRight
    }

    public enum UI3DObject
    {
        Cube,
        Sphere
    }

    public void OnUIModeDropdownOption(int uiModeOption)
    {
        UIMode modeSelected = (UIMode)uiModeOption;
        ApplyUIMode(modeSelected);
    }

    public void ApplyUIMode(UIMode newUIMode)
    {
        if (newUIMode == UIMode.Floating)
            canvas.transform.SetPositionAndRotation(floatingUISettings.uiPosition, floatingUISettings.uiRotation);
        else if(newUIMode == UIMode.OnTable)
            canvas.transform.SetPositionAndRotation(onTableUISettings.uiPosition, onTableUISettings.uiRotation);
        else if (newUIMode == UIMode.OnTableLeft)
            canvas.transform.SetPositionAndRotation(onTableLeftUISettings.uiPosition, onTableLeftUISettings.uiRotation);
        else if (newUIMode == UIMode.OnTableRight)
            canvas.transform.SetPositionAndRotation(onTableRightUISettings.uiPosition, onTableRightUISettings.uiRotation);
    }

    public void On3DObjectDropdownOption(int ui3DObjectOption)
    {
        UI3DObject new3DObject = (UI3DObject)ui3DObjectOption;
        ui3DObjectSelected = new3DObject;
    }

    public void Add3DObject()
    {
        var newGO = GameObject.CreatePrimitive(ui3DObjectSelected == UI3DObject.Sphere ? PrimitiveType.Sphere : PrimitiveType.Cube);
        newGO.transform.parent = new3DObjectParent.transform;
        newGO.transform.localPosition = Vector3.zero;
        newGO.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        newGO.AddComponent<Rigidbody>();
        ui3DObjectStatus.text = $"New 3D Object ({ui3DObjectSelected}) Created...";

        if (randomColorToggle.isOn)
        {
            Material newMaterial = new Material(Shader.Find("Standard"));
            newMaterial.SetColor("_Color", Random.ColorHSV());
            newGO.GetComponent<MeshRenderer>().material = newMaterial;
        }

        var colliderSurface = newGO.AddComponent<ColliderSurface>();
        colliderSurface.InjectCollider(newGO.GetComponent<Collider>());
        var interactable = newGO.AddComponent<RayInteractable>();
        interactable.InjectSurface(colliderSurface);
    }
}
