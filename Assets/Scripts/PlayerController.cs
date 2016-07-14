using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    Vector2 Velocity;
    float SnapValue = 0.50f;
    bool GhostMode = false;
    GameManager Game;

    public bool Controllable;
    public float Speed;

    void Start()
    {
        Game = FindObjectOfType<GameManager>();
    }

    public void Stop()
    {
        Velocity.x = 0;
        Velocity.y = 0;
    }

    public void SwitchRoles()
    {
        GhostMode = !GhostMode;
    }

    void OnCollisionEnter2D(Collision2D incoming)
    {
        if (incoming.gameObject.tag == "Ghost")
        {
            if (GhostMode)
            {
                Destroy(incoming.gameObject);
                Game.Score++;
            } else
            {
                Game.GameEnd();
            }
        }
    }

    // Update is called once per frame
    void Update () {
        if (!Controllable)
        {
            return;
        }
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");
        var snapInverse = 1 / SnapValue;
        if (x < 0)
        {
            Velocity.x = Speed;
            transform.position = new Vector3(transform.position.x, Mathf.Round(transform.position.y * snapInverse) / snapInverse);
            this.transform.rotation = new Quaternion(0, 0, 1.0f, 0);
        } else if (x > 0)
        {
            Velocity.x = Speed;
            transform.position = new Vector3(transform.position.x, Mathf.Round(transform.position.y * snapInverse) / snapInverse);
            this.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        else if (y < 0)
        {
            Velocity.x = Speed;
            transform.position = new Vector3(Mathf.Round(transform.position.x * snapInverse) / snapInverse, transform.position.y);
            this.transform.rotation = new Quaternion(0, 0, -0.7f, 0.7f);
        } else if (y > 0)
        {
            Velocity.x = Speed;
            transform.position = new Vector3(Mathf.Round(transform.position.x * snapInverse) / snapInverse, transform.position.y);
            this.transform.rotation = new Quaternion(0, 0, 0.7f, 0.7f);
        }

        transform.Translate(Velocity);
    }
}
