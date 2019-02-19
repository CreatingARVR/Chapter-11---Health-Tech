using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {
	void Update () {
        // rotate each collectable
        // 15 in the x axis, z axis, multiplied
        // take from Roll-a-ball by Unity https://unity3d.com/learn/tutorials/s/roll-ball-tutorial
        transform.Rotate(new Vector3(0, 30, 0) * Time.deltaTime);
	}
}
