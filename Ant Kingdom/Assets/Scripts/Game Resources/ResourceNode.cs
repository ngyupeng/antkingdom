using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ResourceNode : MonoBehaviour
{
    public static ResourceNode selectedNode;
    public delegate void OnSelect();
    public static event OnSelect onSelect;
    public delegate void OnResourceNodeRegen();
    public static event OnResourceNodeRegen onResourceNodeRegen;
    
    [SerializeField]
    private ResourceNodeType resourceNodeType;
    private int amount;
    [SerializeField]
    private SpriteRenderer sprite;

    private BoundsInt area;

    private void Awake() {
        amount = resourceNodeType.GetAmount();
        InvokeRepeating("RegenerateResources", 0f, 10f);
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
        return sprite.sprite;
    } 

    public Resource GetResource() {
        return resourceNodeType.GetResource();
    }

    public Sprite GetResourceIcon() {
        return resourceNodeType.GetResourceIcon();
    }

    public void RegenerateSingleResourceAmount() {
        if (amount < resourceNodeType.GetAmount()) {
            amount++;
            UpdateSprite();
            onResourceNodeRegen?.Invoke();
        }
    }

    public void RegenerateResources() {
        if (amount < resourceNodeType.GetAmount()) {
            amount = resourceNodeType.GetAmount();
            UpdateSprite();
        }
    }

    public int GetAmount() {
        return amount;
    }

    // When a unit needs to take a certain amount
    // Returns amount taken.
    public int TakeAmount(int targetAmount) {
        int takenAmount = Mathf.Min(amount, targetAmount);
        amount -= takenAmount;
        UpdateSprite();
        return takenAmount;
    }

    public int CanTakeAmount(int targetAmount) {
        return Mathf.Min(amount, targetAmount);
    }

    private void SetAreaAvailable() {

    }

    private void UpdateSprite() {
        if (amount == 0) {
            sprite.sprite = resourceNodeType.GetDepletedSprite();
        } else {
            sprite.sprite = resourceNodeType.GetSprite();
        }
    }

    private bool isClicking;
    private Vector3 clickPosition;
    private void OnMouseDown() {
        isClicking = true;
        clickPosition = Input.mousePosition;
    }
    // Should only do stuff if there is no dragging
    private void OnMouseUp() {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        if (isClicking && clickPosition == Input.mousePosition) {
            selectedNode = this;
            onSelect?.Invoke();
        }
        isClicking = false;
    }
}
