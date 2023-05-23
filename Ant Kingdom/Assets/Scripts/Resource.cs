using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Resource", menuName = "Assets/Resource")]    
public class Resource : ScriptableObject
{
    public GameResources.ResourceType resourceType;
    public string resourceName;
    public Sprite resourceIcon;

    public GameResources.ResourceType getResourceType() {
        return resourceType;
    }
    public string getName() {
        return resourceName;
    }

    public Sprite getIcon() {
        return resourceIcon;
    }
}
