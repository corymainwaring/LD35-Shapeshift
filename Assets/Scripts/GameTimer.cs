using UnityEngine;
using System.Collections;

public class GameTimer : MonoBehaviour
{

    public int DayLengthInSeconds;
    float curTime;
    bool NightTime = true;
    bool Running = false;
    float XDistance;
    GameManager Game;
    // Use this for initialization
    void Start()
    {
        XDistance = transform.position.x * -1.0f - transform.position.x;
        Game = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Running)
        {
            curTime = (curTime + Time.deltaTime) % DayLengthInSeconds;
            float timePos = (curTime / DayLengthInSeconds * XDistance) - (XDistance / 2);
            if (timePos < transform.position.x) // New Day!
            {
                NightTime = !NightTime;
                if (Game)
                {
                    Game.SendMessage("Flip");

                }
            }
            transform.position = new Vector2(timePos, transform.position.y);
        }
    }

    public void TimeStart()
    {
        curTime = 0;
        if (!NightTime)
        {
            Game.SendMessage("Flip");
            NightTime = true;
        }
        transform.position = new Vector2(-6.47f, 7.04f);
        Running = true;
    }

    public void TimeStop()
    {
        curTime = 0;
        if (!NightTime)
        {
            Game.SendMessage("Flip");
            NightTime = true;
        }
        transform.position = new Vector2(-6.47f, 7.04f);
        Running = false;

    }
}
