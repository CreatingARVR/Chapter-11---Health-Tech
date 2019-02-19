using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindChimeShaker : MonoBehaviour {
    public float signal = 0;
    public float deltaX = 1f;
    public float absMaxDelta = 10.0f;
    public Vector3 initialWindChimePosition;
    // Use this for initialization
    void Start () {
        Vector3 initialWindChimePosition = transform.position;
}
	
	// Update is called once per frame
	void Update () {

        if(Mathf.Abs(initialWindChimePosition.x - transform.position.x) > absMaxDelta)
        {
            Debug.Log("hi");
            deltaX = deltaX * -1.0f;
        }
        transform.SetPositionAndRotation(transform.position + new Vector3(0f,0f,deltaX), transform.rotation);

    }
}
