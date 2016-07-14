using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    bool NightTime = true;
    public int Score = 0;
    public Vector2 StartLocation;
    GhostSpawner[] Spawners;
    PlayerController Player;
    GameObject Intro;
    GameObject Ending;
    GameTimer Timer;

    // Use this for initialization
    void Start () {
    
        Spawners = FindObjectsOfType<GhostSpawner>();
        Player = FindObjectOfType<PlayerController>();
        Intro = GameObject.Find("IntroPanel");
        Ending = GameObject.Find("EndingPanel");
        Timer = FindObjectOfType<GameTimer>();
        Ending.SetActive(false);
    }

    void Flip()
    {
        NightTime = !NightTime;
        var components = FindObjectsOfType<GameObject>();
        foreach (GameObject comp in components){
            var flipper = comp.GetComponent<SpriteFlipper>();
            if (flipper)
            {
                flipper.SendMessage("Flip");
            }
            var controller = comp.GetComponent<PlayerController>();
            if (controller)
            {
                controller.SwitchRoles();

            }
            var ghostmovement = comp.GetComponent<GhostMovement>();
            if (ghostmovement)
            {
                ghostmovement.SwitchRoles();

            }
        }
    }

    public void Reset()
    {
        var ghosts = GameObject.FindGameObjectsWithTag("Ghost");
        foreach (GameObject g in ghosts)
        {
            Destroy(g);
        }
        foreach (GhostSpawner s in Spawners)
        {
            s.Spawning = false;
        }
        Player.transform.position = StartLocation;
        Player.Controllable = false;
        Timer.TimeStop();
        Score = 0;
        Ending.SetActive(false);
    }

    public void GameEnd()
    {
        Player.Controllable = false;
        Player.transform.position = new Vector2(10, 10);
        Player.transform.rotation = new Quaternion(0, 0, -0.7f, 0.7f);
        Timer.TimeStop();
        Ending.SetActive(true);
    }

    public void GameStart()
    {
        Player.Controllable = true;
        foreach (GhostSpawner s in Spawners)
        {
            s.Spawning = true;
        }
        Intro.SetActive(false);
        Timer.TimeStart();
    }
    
    // TODO: Reset!

    // Update is called once per frame
    void Update () {
    
    }
}
