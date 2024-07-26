using UnityEngine;

public class TouchManager : MonoBehaviour
{
    private float yOffset = 0;
    public Vector3 startPos = Vector3.zero;

    public GameObject sparks;

    private void Start()
    {
        startPos = sparks.transform.GetChild(0).transform.position;
        yOffset = startPos.y;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Vector3 pos = GetWorldPosition(touch.position);
                sparks.transform.GetChild(0).position = pos;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                Vector3 pos = GetWorldPosition(touch.position);
                sparks.transform.GetChild(0).position = pos;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                sparks.transform.GetChild(0).position = startPos;
            }
        }
    }

    Vector3 GetWorldPosition(Vector3 pos)
    {
        Ray ray = Camera.main.ScreenPointToRay(pos);

        RaycastHit hit;
        Vector3 ret = new Vector3();

        if (Physics.Raycast(ray, out hit))
        {
            ret = hit.point;
        }
        return new Vector3(ret.x, yOffset, ret.z);
    }
}
