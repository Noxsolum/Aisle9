using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    public GameObject light;

    private void Start()
    {
        light = this.gameObject.transform.GetChild(1).gameObject;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WaitRandomTime()
    {
        yield return new WaitForSeconds(Random.Range(2, 20));
    }

    IEnumerator WaitTwoSeconds()
    {
        yield return new WaitForSeconds(2);
    }
}
