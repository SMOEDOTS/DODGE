using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDrone : MonoBehaviour {

    public Transform target;
    public float speed;
    bool TRIGGERED = false;

	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, 60f);
	}
	
	// Update is called once per frame
	void Update () {
        if (TRIGGERED == false)
        {
            //rotate to look at the player
            var dir = target.position - transform.position;
            var angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) - 90;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            this.gameObject.GetComponent<Rigidbody2D>().AddForce(this.gameObject.transform.up * speed);
        }
    }

    void OnTriggerEnter2D()
    {
        TRIGGERED = true;
        GetComponent<Animator>().Play("LazerFiring");
        this.gameObject.GetComponent<Rigidbody2D>().drag = 1;
        StartCoroutine(waitasec());
    }

    IEnumerator waitasec()
    {
        yield return new WaitForSeconds(2.5f);
        Destroy(this.gameObject);
    }

}
