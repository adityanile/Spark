using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainManager : MonoBehaviour
{
    public float offset = 2f;
    public GameObject follow;

    public float speed = 1f;

    // Update is called once per frame
    void Update()
    {
        if (follow)
        {
            Vector3 dir = (follow.transform.position - transform.position).normalized;
            float dist = Vector3.Distance(transform.position, follow.transform.position);

            if(dist > offset)
            {
                transform.Translate(dir * speed * Time.deltaTime);
            }

        }
    }
}
