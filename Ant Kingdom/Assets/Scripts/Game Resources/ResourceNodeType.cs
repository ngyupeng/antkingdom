using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Node", menuName = "Assets/Resource Node")]    
public class ResourceNodeType : ScriptableObject
{
    [SerializeField]
    private string nodeName;
    
    [SerializeField]
    private Resource resource;

    [SerializeField]
    private Sprite nodeSprite;
    [SerializeField]
    private Sprite depletedNodeSprite;
    
    [SerializeField]
    private int maxResourceAmount;

    [SerializeField]
    private BoundsInt area;

    public string GetName() {
        return nodeName;
    }

    public Resource GetResource() {
        return resource;
    }

    public Sprite GetSprite() {
        return nodeSprite;
    } 

    public Sprite GetDepletedSprite() {
        return depletedNodeSprite;
    }

    public Sprite GetResourceIcon() {
        return resource.GetIcon();
    }

    public int GetAmount() {
        return maxResourceAmount;
    }

    public BoundsInt GetArea() {
        return area;
    }
}
