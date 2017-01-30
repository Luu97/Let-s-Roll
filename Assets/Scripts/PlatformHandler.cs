using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlatformHandler : MonoBehaviour {

    //Singleton trick to get a static reference to this class
    public static PlatformHandler PH;

    public GameObject platformPrefab;

    public GameObject platformFinishedEffect;

    public GameObject player;

    public GameObject[] walls;

    public Text scoreCount;

    public float updateTimer = 1.0f;

    public float distanceBetweenPlatforms = 2.5f;

    public float spawnRange = 30.0f;

    private List<GameObject> activePlatforms = new List<GameObject>();

    private GameObject lastTouchedPlatform;

    private System.Random rand;

    void Awake() {
        if (PH != null)
            Destroy(PH);
        else
            PH = this;

        rand = new System.Random();
        distanceBetweenPlatforms = distanceBetweenPlatforms;
    }

    void Start() {
        lastTouchedPlatform = null;
        SpawnFirstPlatforms();     
    }

    private void SpawnFirstPlatforms() {
        int count = Convert.ToInt32(spawnRange / distanceBetweenPlatforms);

        float yPos;
        for (int i = 0; i < count; i++) {
            yPos = -i * distanceBetweenPlatforms;
            SpawnNewPlatform(yPos);
        }
    }

    // Update is called once per frame
    void Update() {
        FindRotateablePlatforms();
        ChekIfDead();
    }

    //Check if the player fell by all the platforms
    private void ChekIfDead() {
        if (activePlatforms.Count >= 1) {
            if (player.transform.position.y <= activePlatforms[activePlatforms.Count - 1].transform.position.y) {
                Die();
            }
        }
    }

    private void FindRotateablePlatforms() {
        if (activePlatforms.Count >= 2) {
            activePlatforms[0].GetComponent<PlatformRotation>().isRotateable = true;
            activePlatforms[1].GetComponent<PlatformRotation>().isRotateable = true;
        }
    }

    public void PlayerHitPlatform(GameObject platform) {

        //First Platform was touched
        if (lastTouchedPlatform == null) {
            lastTouchedPlatform = platform;
        }
        //If we didn't hit the same platform again it should be the next platform
        else if (lastTouchedPlatform != platform) {
            float yPos = Mathf.Round(platform.transform.position.y * 10) / 10;
            //The expected Position of the last touched Platform
            float lastYPos = yPos + distanceBetweenPlatforms;
            if (Mathf.Round(lastTouchedPlatform.transform.position.y * 10) / 10 != lastYPos) {
                Die();
            }
            //The correct platform was touched
            else {
                CorrectPlatformTouched(platform);
            }
        }
    }

    private void CorrectPlatformTouched(GameObject platform) {
        GameStatus.GS.score++;
        UIManager.UIM.changeScore(GameStatus.GS.score);

        lastTouchedPlatform = platform;
        GameObject lastPlatform = FindPlatform(platform.transform.position.y + distanceBetweenPlatforms);
        activePlatforms.Remove(lastPlatform);
        GameObject effect = (GameObject)Instantiate(platformFinishedEffect, lastPlatform.transform.position, Quaternion.identity);
        Destroy(effect, 5);
        Destroy(lastPlatform);
        SpawnNewPlatform(activePlatforms[activePlatforms.Count - 1].transform.position.y - distanceBetweenPlatforms);
    }

    private void SpawnNewPlatform(float yPos) {
        float xPos;
        if (yPos == 0) {
            xPos = 0;
        }

        else
            xPos = rand.Next(-8, 8);

        GameObject platform = (GameObject)Instantiate(platformPrefab, new Vector3(xPos, yPos), Quaternion.identity);
        float size = SizeDifficulty.SD.getSize(Convert.ToInt32(-yPos / distanceBetweenPlatforms));
        platform.transform.localScale = new Vector3(size, platformPrefab.transform.localScale.y, platformPrefab.transform.localScale.z);
        activePlatforms.Add(platform);
        foreach (GameObject wall in walls) {
            Physics2D.IgnoreCollision(wall.GetComponent<BoxCollider2D>(), platform.GetComponent<BoxCollider2D>());
        }
    }

    private GameObject FindPlatform(float yPos) {
        foreach (GameObject platform in activePlatforms) {
            if (Mathf.Round(platform.transform.position.y * 10) / 10 == yPos) {
                return platform;
            }
        }
        return null;
    }

    private void Die() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public float getDistanceBetweenPlatforms() {
        return distanceBetweenPlatforms;
    }

    public GameObject getFirstPlatform() {
        return activePlatforms[0];
    }
}
