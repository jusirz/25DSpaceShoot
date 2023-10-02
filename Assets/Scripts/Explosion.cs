using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(ExplodeTimer());
        StopCoroutine(ExplodeTimer());
    }

    private IEnumerator ExplodeTimer()
    {
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
    }

}
