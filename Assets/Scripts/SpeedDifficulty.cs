using UnityEngine;
using System.Collections;

public class SpeedDifficulty : MonoBehaviour {

    public static SpeedDifficulty SD;

    public float maxValue = 4.0f;

    public float minValue = 1.0f;

    public float mathBase = 1.04f;

    public float randomRange = 0.5f;

    private System.Random rand;

    void Awake() {
        if (SD != null)
            Destroy(SD);
        else
            SD = this;
    }

    void Start() {
        rand = new System.Random();
    }

    public float GetSpeed(int number) {
        float difference = maxValue - minValue;
        float value = maxValue - difference * Mathf.Pow(mathBase, -number);
        int randomNumber = rand.Next(0, 100);
        return value + randomRange * randomNumber / 100;
    }
}
