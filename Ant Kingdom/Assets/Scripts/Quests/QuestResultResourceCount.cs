using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class QuestResultResourceCount : MonoBehaviour
{
    public Image resource;
    public TextMeshProUGUI resourceCount;

    public void Init(GameResources.ResourceType type, int count) {
        resource.sprite = GameResources.GetResourceFromType(type).GetIcon();
        resourceCount.text = count.ToString();
    }
}
