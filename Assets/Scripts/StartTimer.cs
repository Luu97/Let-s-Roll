using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartTimer : MonoBehaviour {

    public GameObject player;

    public Text startCountdownText;

    float time = 3.5f;
	// Use this for initialization
	void Start () {
        player.GetComponent<Rigidbody2D>().gravityScale = 0;
        Physics.gravity = new Vector3(0, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
        if (time >= 0) {
            player.GetComponent<CircleCollider2D>().enabled = false;
            startCountdownText.text = Mathf.Round(time).ToString();
            time -= Time.deltaTime;
        }
        else {
            player.GetComponent<CircleCollider2D>().enabled = true;
            player.GetComponent<Rigidbody2D>().gravityScale = 2;
            Destroy(startCountdownText);
            return;            
        }
	    
	}
}
