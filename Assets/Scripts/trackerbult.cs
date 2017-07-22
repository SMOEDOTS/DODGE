using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trackerbult : MonoBehaviour {

    public Transform target;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        var dir = target.position - transform.position;
        var angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) - 270;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
