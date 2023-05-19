using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    public static ResourceNode selectedNode;
    public delegate void OnSelect();
    public static event OnSelect onSelect;
    public string itemName;
    public Sprite resourceType;
    public Sprite node;
    public int amount;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string getName() {
        return itemName;
    }

    public Sprite getSprite() {
        return node;
    } 

    public Sprite getTypeSprite() {
        return resourceType;
    }

    public int getAmount() {
        return amount;
    }
    private void OnMouseDown() {
        Debug.Log("Clicked");
        selectedNode = this;
        onSelect?.Invoke();
    }
}
