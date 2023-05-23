using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Node", menuName = "Assets/Resource Node")]    
public class ResourceNodeType : ScriptableObject
{
    // Name of the node
    public string nodeName;
    // Resource contained in the node
    public Resource resource;
    // Sprite for the node
    public Sprite node;
    // Default amount for the node
    public int amount;

    public string getName() {
        return nodeName;
    }

    public Sprite getSprite() {
        return node;
    } 

    public Sprite getResourceIcon() {
        return resource.getIcon();
    }

    public int getAmount() {
        return amount;
    }
}
