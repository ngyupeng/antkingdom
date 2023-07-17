using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AntCountInstance : MonoBehaviour
{
    private AntData ant;
    private int antCount;
    private int reduceCount = 0;
    public TextMeshProUGUI antCountText;
    public Image antImage;

    public delegate void OnDecreaseCount();
    public static event OnDecreaseCount onDecreaseCount;

    private void Awake() {
        AntManager.onIdleAntNumberChanged += UpdateView;
    }
    public void Init(AntData antD) {
        ant = antD;
        antImage.sprite = ant.sprite;
        antCount = AntManager.GetAntCount(ant.antType);
        antCountText.text = antCount.ToString() + "x";
    }

    public void UpdateView() {
        antCount = AntManager.GetIdleAntCount(ant.antType);
        if (reduceCount > antCount) {
            reduceCount = antCount;
        }
        antCountText.text = (antCount - reduceCount).ToString() + "x";
        if (reduceCount > 0) {
            antCountText.color = Color.red;
        } else {
            antCountText.color = Color.black;
        }
    }

    public void Reset() {
        reduceCount = 0;
        UpdateView();
    }

    public void DecreaseCount() {
        reduceCount++;
        UpdateView();
        onDecreaseCount?.Invoke();
    }

    public void ConfirmReduce() {
        AntManager.AddAntAmount(ant.antType, -reduceCount);
        reduceCount = 0;
        UpdateView();
        if (AntManager.GetIdleAntCount(ant.antType) == 0) {
            Destroy(gameObject);
        }
    }

    private void OnDestroy() {
        AntManager.onIdleAntNumberChanged -= UpdateView;
    }
}
