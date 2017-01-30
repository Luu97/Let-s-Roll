using UnityEngine;
using System.Collections;

public class GameStatus : MonoBehaviour {

    public static GameStatus GS;

    public int score;

    void Awake () {
        if (GS != null)
            Destroy(GS);
        else
            GS = this;
    }
	// Use this for initialization
	void Start () {
        score = 0;
	}
}
