using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public static UIManager UIM;

    public Text scoreText;

    void Awake () {
        if (UIM != null)
            Destroy(UIM);
        else
            UIM = this;
    }

	public void changeScore(int score) {
        scoreText.text = "Score: " + score;

    }
}
