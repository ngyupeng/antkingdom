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

    public string GetName() {
        return nodeName;
    }

    public Resource GetResource() {
        return resource;
    }

    public Sprite GetSprite() {
        return nodeSprite;
    } 

    public Sprite GetResourceIcon() {
        return resource.GetIcon();
    }

    public int GetAmount() {
        return defaultResourceAmount;
    }

    public BoundsInt GetArea() {
        return area;
    }
}
