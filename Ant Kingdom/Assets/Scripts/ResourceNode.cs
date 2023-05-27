using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ResourceNode : MonoBehaviour
{
    public static ResourceNode selectedNode;
    public delegate void OnSelect();
    public static event OnSelect onSelect;
    
    [SerializeField]
    private ResourceNodeType resourceNodeType;
    private Sprite nodeSprite;
    private int amount;

    private BoundsInt area;

    private void Awake() {
        nodeSprite = resourceNodeType.GetSprite();
        amount = resourceNodeType.GetAmount();
    }

    private void Start() {
        InitArea();
    }

    private void InitArea() {
        area = resourceNodeType.GetArea();
        area.position = GridBuildingSystem.current.gridLayout.WorldToCell(gameObject.transform.position);
        GridBuildingSystem.current.SetBuildingTilesUnavailable(area);
    }

    public string GetName() {
        return resourceNodeType.GetName();
    }

    public Sprite GetSprite() {
        return nodeSprite;
    } 

    public Resource GetResource() {
        return resourceNodeType.GetResource();
    }

    public Sprite GetResourceIcon() {
        return resourceNodeType.GetResourceIcon();
    }

    public int GetAmount() {
        return amount;
    }

    // When a unit needs to take a certain amount
    // Returns amount taken.
    public int TakeAmount(int targetAmount) {
        int takenAmount = Mathf.Min(amount, targetAmount);
        amount -= takenAmount;
        return takenAmount;
    }

    private void SetAreaAvailable() {

    }
    private void OnMouseUp() {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        selectedNode = this;
        onSelect?.Invoke();
    }
}
