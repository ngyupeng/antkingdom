using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    public static ResourceNode selectedNode;
    public delegate void OnSelect();
    public static event OnSelect onSelect;
    public ResourceNodeType resourceNodeType;
    private Sprite nodeSprite;
    private int amount;

    private void Awake() {
        nodeSprite = resourceNodeType.getSprite();
        amount = resourceNodeType.getAmount();
    }

    public string getName() {
        return resourceNodeType.getName();
    }

    public Sprite getSprite() {
        return nodeSprite;
    } 

    public Sprite getResourceIcon() {
        return resourceNodeType.getResourceIcon();
    }

    public int getAmount() {
        return amount;
    }

    // When a unit needs to take a certain amount
    // Returns amount taken.
    public int takeAmount(int targetAmount) {
        int takenAmount = Mathf.Min(amount, targetAmount);
        amount -= takenAmount;
        return takenAmount;
    }
    private void OnMouseDown() {
        Debug.Log("Clicked");
        selectedNode = this;
        onSelect?.Invoke();
    }
}
