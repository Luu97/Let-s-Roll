using UnityEngine;
using System.Collections;

public class PlatformTrigger : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            PlatformHandler.PH.PlayerHitPlatform(gameObject);
        }
    }
}
