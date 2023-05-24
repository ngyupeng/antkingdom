using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Resource", menuName = "Assets/Resource")]    
public class Resource : ScriptableObject
{
    [SerializeField]
    private string resourceName;
    
    [SerializeField]
    private GameResources.ResourceType resourceType;

    [SerializeField]
    private Sprite resourceIcon;

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
