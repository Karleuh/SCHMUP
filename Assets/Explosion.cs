using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float time;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Explode");
    }

    private IEnumerator Explode(){
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
