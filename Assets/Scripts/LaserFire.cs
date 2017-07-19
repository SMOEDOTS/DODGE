using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserFire : MonoBehaviour {

    private LineRenderer laserline;
    public Transform RaySpawn;
    private bool on = false;
    private bool pewpew = false;
	// Use this for initialization
	void Start () {
        laserline = GetComponent<LineRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
		if (on)
        {
            StartCoroutine(lasershoot());
        }

        if (pewpew)
        {
            pewpew = false;
            laserline.SetPosition(0, RaySpawn.position);

            RaycastHit2D hit = Physics2D.Raycast(RaySpawn.position, RaySpawn.up);

            if (hit.collider != null)
            {

                laserline.SetPosition(1, hit.point);
                if (hit.collider.name == "Player")
                {
                    Debug.Log("gitGud");
                    hit.transform.SendMessage("HitByRay");
                }
            }
            StartCoroutine(waitpew());
        }
	}

    void OnTriggerEnter2D()
    {
        on = true;
    }

    IEnumerator lasershoot()
    {
        yield return new WaitForSeconds(0.8f);
        pewpew = true;
    }

    IEnumerator waitpew()
    {
        yield return new WaitForSeconds(0.2f);
        pewpew = true;
    }
}
