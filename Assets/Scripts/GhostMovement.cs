using UnityEngine;
using System.Collections;
using System;

enum Directions : int
{
    UP, RIGHT, DOWN, LEFT, NONE
}

public class GhostMovement : MonoBehaviour
{

    public float Speed = 0.05f;

    GameObject Pacman;
    System.Random rand;

    Directions ChosenDirection;
    Directions LastDirection;
    Vector2[] DirectionArr = new Vector2[4];
    BoxCollider2D[] Feelers = new BoxCollider2D[4];
    ArrayList InvalidatedDirections = new ArrayList();
    float snap = 0.5f;
    bool Running = false;
    bool[] canGo = new bool[4];
    bool InIntersection;

    // Use this for initialization
    void Start()
    {
        Pacman = GameObject.Find("Pacman");
        DirectionArr[(int)(Directions.UP)] = new Vector2(0, Speed); // Up
        DirectionArr[(int)(Directions.DOWN)] = new Vector2(0, -Speed); // Down
        DirectionArr[(int)(Directions.RIGHT)] = new Vector2(Speed, 0); // Right
        DirectionArr[(int)(Directions.LEFT)] = new Vector2(-Speed, 0); // Left
        ChosenDirection = Directions.NONE;
        this.rand = new System.Random();
        var f = GetComponents<BoxCollider2D>();
        foreach (var coll in f)
        {
            if (coll.isTrigger)
            {
                if (coll.offset.x > 0)
                {
                    Feelers[(int)Directions.RIGHT] = coll;
                } else if (coll.offset.x < 0)
                {
                    Feelers[(int)Directions.LEFT] = coll;
                } else if (coll.offset.y < 0)
                {
                    Feelers[(int)Directions.DOWN] = coll;
                } else if (coll.offset.y > 0)
                {
                    Feelers[(int)Directions.UP] = coll;
                }

            }
        }
    }

    public void SwitchRoles()
    {
        Running = !Running;
    }

    void Stop()
    {
        var snapInverse = 1 / snap;
        if (ChosenDirection != Directions.NONE)
        {
            InvalidatedDirections.Add((Directions)(ChosenDirection));
            ChosenDirection = Directions.NONE;
        }
        transform.position = new Vector2(Mathf.Round(transform.position.x * snapInverse) / snapInverse, Mathf.Round(transform.position.y * snapInverse) / snapInverse);
    }



    // Update is called once per frame
    void Update()
    {
        LayerMask collisionLayers = LayerMask.GetMask("Walls", "Ghosts");
        canGo[(int)Directions.UP] = !Feelers[(int)Directions.UP].IsTouchingLayers(collisionLayers);
        canGo[(int)Directions.DOWN] = !Feelers[(int)Directions.DOWN].IsTouchingLayers(collisionLayers);
        canGo[(int)Directions.LEFT] = !Feelers[(int)Directions.LEFT].IsTouchingLayers(collisionLayers);
        canGo[(int)Directions.RIGHT] = !Feelers[(int)Directions.RIGHT].IsTouchingLayers(collisionLayers);

        canGo[(int)LastDirection] = false;

        var deltaX = Pacman.transform.position.x - transform.position.x;
        var dirX = (deltaX > 0) ? Directions.RIGHT : Directions.LEFT;
        var oppX = (dirX == Directions.RIGHT) ? Directions.LEFT : Directions.RIGHT;

        var deltaY = Pacman.transform.position.y - transform.position.y;
        var dirY = (deltaY > 0) ? Directions.UP : Directions.DOWN;
        var oppY = (dirY == Directions.DOWN) ? Directions.UP : Directions.DOWN;

        var count = 0;
        foreach (bool can in canGo)
        {
            if (can)
            {
                count++;
            }
        }
        var atIntersection = count > 1;
        if (!atIntersection)
        {
            InIntersection = false;
        }
        if (ChosenDirection == Directions.NONE || (atIntersection && !InIntersection))
        {
            if (ChosenDirection != Directions.NONE)
            {
                InIntersection = true;
            }
            Directions[] bestDirections = new Directions[4];
            var XBiggerThanY = Math.Abs(deltaX) > Math.Abs(deltaY);
            if (XBiggerThanY)
            {
                bestDirections[0] = dirX;
                bestDirections[1] = dirY;
                bestDirections[2] = oppY;
                bestDirections[3] = oppX;
            } else
            {
                bestDirections[0] = dirY;
                bestDirections[1] = dirX;
                bestDirections[2] = oppX;
                bestDirections[3] = oppY;
            }
            if (Running)
            {
                Array.Reverse(bestDirections);
            }
            ChosenDirection = Directions.NONE;
            while (ChosenDirection == Directions.NONE) {
                foreach (Directions d in bestDirections)
                {
                    if (canGo[(int)d] && rand.NextDouble() < 0.5)
                    {
                        ChosenDirection = d;
                        break;
                    }
                }
            }
        }
        if (atIntersection && rand.NextDouble() < 0.1)
        {

        }
        if (ChosenDirection != Directions.NONE)
        {
            LastDirection = ChosenDirection;
            this.transform.Translate(DirectionArr[(int)(ChosenDirection)]);
        }
    }
}
