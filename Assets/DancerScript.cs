using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DancerScript : MonoBehaviour
{
    Animator ani;

    public string[] terms;

    // Start is called before the first frame update
    void Awake()
    {
        ani = GetComponent<Animator>();
        ani.SetBool(terms[Random.Range(0, terms.Length)], true);
    }
}
