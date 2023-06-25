using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ResourceAmountSlider : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Resource resource;
    private int currentAmount;
    public TextMeshProUGUI resourceAmount;
    public Image resourceIcon;
    public Slider slider;
    public GameObject resourceCapacityTooltip;
    private void Awake() {
        resourceIcon.sprite = resource.GetIcon();

        GameResources.onResourceAmountChanged += UpdateSlider;
        GameResources.onResourceCapacityChanged += UpdateSlider;
    }

    private void Start() {
        currentAmount = GameResources.GetResourceAmount(resource.GetResourceType());
        resourceAmount.text = currentAmount.ToString();
        slider.value = (float) currentAmount / GameResources.GetResourceCapacity(resource.GetResourceType());
        resourceCapacityTooltip.GetComponent<ResourceCapacityTooltip>().Initialise(resource);
    }

    public void UpdateSlider() {
        float previousValue = slider.value;
        float newValue = (float) GameResources.GetResourceAmount(resource.GetResourceType()) 
            / GameResources.GetResourceCapacity(resource.GetResourceType());
        StartCoroutine(AnimateSliderOverTime(1f, previousValue, newValue));
        currentAmount = GameResources.GetResourceAmount(resource.GetResourceType());
        resourceAmount.text = currentAmount.ToString();
    }
    private IEnumerator AnimateSliderOverTime(float seconds, float previousValue, float newValue)
    {
        float animationTime = 0f;
        while (animationTime < seconds)
        {
            animationTime += Time.deltaTime;
            float lerpValue = animationTime / seconds;
            slider.value = Mathf.Lerp(previousValue, newValue, lerpValue);
            yield return null;
        }
    }

    private void OnDestroy() {
        GameResources.onResourceAmountChanged -= UpdateSlider;
        GameResources.onResourceCapacityChanged -= UpdateSlider;
    }

    public void OnPointerEnter(PointerEventData pointerEventData) {
        resourceCapacityTooltip.SetActive(true);
    }

    public void OnPointerExit(PointerEventData pointerEventData) {
        resourceCapacityTooltip.SetActive(false);
    }
}
