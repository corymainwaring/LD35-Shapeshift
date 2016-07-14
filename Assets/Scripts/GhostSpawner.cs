using UnityEngine;
using System.Collections;

public class GhostSpawner : MonoBehaviour
{

    public GameObject GhostToSpawn;
    public float TimeToWait;
    public bool Spawning;
    float TimeWaited;
    SpriteFlipper Flipper;

    // Use this for initialization
    void Start()
    {
        Flipper = GetComponent<SpriteFlipper>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Spawning)
        {
            TimeWaited += Time.deltaTime;
            if (TimeWaited > TimeToWait - (UnityEngine.Random.value * TimeToWait))
            {
                var Ghost = Instantiate(GhostToSpawn);
                var flipper = Ghost.GetComponent<SpriteFlipper>();
                flipper.Sprites[0] = Flipper.Current();
                flipper.Sprites[1] = Flipper.Next();
                Ghost.transform.position = transform.position;
                TimeWaited -= TimeToWait;
            }
        }
    }
}
