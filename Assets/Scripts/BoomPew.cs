using System.Collections;
using UnityEngine;

public class BoomPew : MonoBehaviour
{
    private float speed = 12;
    public GameObject boomPew;
    public Transform self;

    // Use this for initialization
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D()
    {
        StartCoroutine(time());
    }

    IEnumerator time()
    {
        yield return new WaitForSeconds(1f);
        boom();
    }

    void boom()
    {
        var bullet = (GameObject)Instantiate(boomPew,self.position,self.rotation);

        bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.up * speed;

        Destroy(bullet, 0.6f);
    }
}
