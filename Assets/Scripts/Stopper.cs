using UnityEngine;
using System.Collections;

public class Stopper : MonoBehaviour {


    void OnCollisionEnter2D(Collision2D coll)
    {
        coll.gameObject.SendMessage("Stop");
    }
    void OnCollisionStay2D(Collision2D coll)
    {
        coll.gameObject.SendMessage("Stop");
    }
}
