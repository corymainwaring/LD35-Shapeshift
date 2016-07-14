using UnityEngine;
using System.Collections;
using System;

public class Warper : MonoBehaviour {

    BoxCollider2D[] Colliders;
    // Use this for initialization
    void Start () {
        Colliders = this.GetComponents<BoxCollider2D>();
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        GameObject Warpable = coll.gameObject;
        if (Warpable.transform.position.x > 0)
        {
            Warpable.transform.position = new Vector2(
                 Warpable.transform.position.x - Math.Abs(Colliders[1].offset.x - Colliders[0].offset.x) + Colliders[0].size.x + 0.75f,
                Warpable.transform.position.y);
        } else
        {
            Warpable.transform.position = new Vector2(
                Warpable.transform.position.x + Math.Abs(Colliders[1].offset.x - Colliders[0].offset.x) - Colliders[1].size.x - 0.75f,
                Warpable.transform.position.y);
        }
        foreach (BoxCollider2D box in Colliders)
        {
            if (coll.collider == box)
            {
                coll.gameObject.transform.Translate(-1, 0, 0);
            }
        }
    }
    
    // Update is called once per frame
    void Update () {
    
    }
}
