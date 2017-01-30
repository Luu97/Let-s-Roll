using UnityEngine;
using System.Collections;

public class SizeDifficulty : MonoBehaviour {

    public static SizeDifficulty SD;

    public float maxValue = 4.0f;

    public float minValue = 1.0f;

    public float mathBase = 1.04f;

    void Awake () {
        if (SD != null)
            Destroy(SD);
        else
            SD = this;
    }

    public float getSize(int number) {
        float difference = maxValue - minValue;
        float value = minValue + difference * Mathf.Pow(mathBase, -number);
        return value;
    }
}
