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
    private int defaultResourceAmount;

    [SerializeField]
    private BoundsInt area;

    public string getName() {
        return nodeName;
    }

    public Sprite getSprite() {
        return nodeSprite;
    } 

    public Sprite getResourceIcon() {
        return resource.getIcon();
    }

    public int getAmount() {
        return defaultResourceAmount;
    }

    public BoundsInt getArea() {
        return area;
    }
}
