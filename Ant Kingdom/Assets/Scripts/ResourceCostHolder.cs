using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ResourceCostHolder : MonoBehaviour
{
    [SerializeField] private Image resourceIcon;
    [SerializeField] private TextMeshProUGUI resourceName;
    [SerializeField] private TextMeshProUGUI resourceAmount;

    public void Initialise(Resource resource, int amount) {
        resourceIcon.sprite = resource.GetIcon();
        resourceName.text = resource.GetName();
        resourceAmount.text = amount.ToString();
    }
}
