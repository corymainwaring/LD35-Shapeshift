using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreUpdater : MonoBehaviour {

    GameManager Game;
    Text t;
    // Use this for initialization
    void Start () {
        Game = FindObjectOfType<GameManager>();
        t = GetComponent<Text>();
    }
    
    // Update is called once per frame
    void Update () {
        t.text = "Score: " + Game.Score;
    }
}
