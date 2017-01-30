using UnityEngine;
using System.Collections.Generic;

public class PlatformRotation : MonoBehaviour {

    public float sensitivity = 100000f;

    public bool isRotateable = false;

    private Color activeColor;
    private Color inactiveColor;

    private Rigidbody2D thisRigidbody;

    private float prevPosition;

    private bool firstCall = true;

    void Start() {
        activeColor = new Color32(255, 255, 255, 255);
        inactiveColor = new Color32(185, 185, 185, 255);

        thisRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        thisRigidbody.angularVelocity = 0;
        thisRigidbody.isKinematic = true;

        if (!isRotateable) {
            GetComponent<SpriteRenderer>().color = inactiveColor;
            firstCall = true;
            return;
        }

        GetComponent<SpriteRenderer>().color = activeColor;
        List<Touch> touches = InputHelper.GetTouches();
        if (touches.Count == 0)
            return;

        if (touches[0].phase == TouchPhase.Began || firstCall) {
            prevPosition = touches[0].position.y;
        }
        firstCall = false;

        float deltaY = touches[0].position.y - prevPosition;

        thisRigidbody.isKinematic = false;
        float degree = sensitivity * deltaY;
        if (deltaY != 0)
            thisRigidbody.AddTorque(deltaY * sensitivity * transform.localScale.x, ForceMode2D.Force);

        prevPosition = touches[0].position.y;


    }
}
