using UnityEngine;
using System.Collections;
using System;

public class PlatformMovement : MonoBehaviour {

    private float worldSpaceHeight;
    private float worldSpaceWidth;

    private System.Random rand;
    private bool goLeft;

    //private float length;

    private float speed;

    private int number;

    // Use this for initialization
    void Start() {
        worldSpaceWidth = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,0,0)).x;
        float yPos = Mathf.Round(transform.position.y * 10) / 10;
        float distanceBetweenPlatforms = PlatformHandler.PH.getDistanceBetweenPlatforms();
        number = Convert.ToInt32(-yPos / distanceBetweenPlatforms);
        speed = SpeedDifficulty.SD.GetSpeed(number);

        rand = new System.Random();
        if (rand.Next(0, 2) == 0)
            goLeft = false;
        else
            goLeft = true;
    }

    // Update is called once per frame
    void Update() {
        //Dont move the first platform
        if (Mathf.Round(transform.position.y * 10) / 10 == 0)
            return;

        if (transform.position.x <= - worldSpaceWidth) {
            speed = SpeedDifficulty.SD.GetSpeed(number);
            goLeft = false;
        }
        else if (transform.position.x >= worldSpaceWidth) {
            goLeft = true;
            speed = SpeedDifficulty.SD.GetSpeed(number);
        }

        MovePlatform(goLeft);
    }

    private void MovePlatform(bool goLeft) {
        if (goLeft)
            transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
        else
            transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
    }
}
