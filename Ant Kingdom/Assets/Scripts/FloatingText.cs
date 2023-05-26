using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float destroyTime = .5f;
    void Start() {
        Destroy(gameObject, destroyTime);
    }
}
