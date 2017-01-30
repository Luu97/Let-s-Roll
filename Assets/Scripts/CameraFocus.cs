using UnityEngine;
using System.Collections;

public class CameraFocus : MonoBehaviour {

    public GameObject player;
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(0, player.transform.position.y - 3);
	}
}
