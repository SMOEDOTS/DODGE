using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonControllerForstart : MonoBehaviour {

    public GameObject thisbutton;

    void Start()
    {
        StartCoroutine(reactive());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator reactive()
    {
        //Fix This
        thisbutton.SetActive(false);
        yield return new WaitForSeconds(6);
        print(Time.time);
        thisbutton.SetActive(true);

    }
}
