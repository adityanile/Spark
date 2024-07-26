using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SparkManager : MonoBehaviour
{
    public GameObject pref;
    public List<GameObject> spark = new List<GameObject>();

    public GameObject source;
    public float yOff = -0.1f;

    public Transform movingEnd;
    public bool spawned = false;

    RaycastHit hit;

    private void Start()
    {
        source = GameObject.FindGameObjectWithTag("Source");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(touch.position);

            if (touch.phase == TouchPhase.Began)
            {
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.CompareTag("Source"))
                    {
                        InstSpark();
                    }
                }
                ResetEffect();
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                if (spawned)
                {
                    Vector3 pos = Camera.main.ScreenToWorldPoint(touch.position);
                    Vector3 movePos = new Vector3(pos.x, pos.y, 0);

                    movingEnd.position = movePos;
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                if (spawned)
                {
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider.CompareTag("Destination"))
                        {
                            spawned = false;
                            hit.collider.GetComponent<BulbManager>().TurnOn();

                            return;
                        }
                    }
                    DestroySpark();
                }
            }
        }
    }

    void ResetEffect()
    {
        foreach(var i in spark)
        {
            i.GetComponent<VisualEffect>().Reinit();
        }
    }

    void DestroySpark()
    {
        if (spark.Count > 0)
        {
            Destroy(spark[spark.Count - 1]);
            spark.RemoveAt(spark.Count - 1);
        }
    }

    void InstSpark()
    {
        GameObject inst = Instantiate(pref, transform.position, pref.transform.rotation, transform);
        spark.Add(inst);

        Vector3 pos = new Vector3(source.transform.position.x, source.transform.position.y + yOff, source.transform.position.z);

        for (int i = 0; i < inst.transform.childCount; i++)
        {
            inst.transform.GetChild(i).transform.localPosition = pos;
        }

        movingEnd = inst.transform.GetChild(0);
        spawned = true;
    }
}
