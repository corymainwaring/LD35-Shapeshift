using UnityEngine;
using System.Collections;

public class CameraFollower : MonoBehaviour {

    public GameObject Following;

	// Update is called once per frame
	void Update () {
        this.transform.rotation = Quaternion.Euler(-Following.transform.position.y, Following.transform.position.x, 0);
	}
}
