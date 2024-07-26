using UnityEngine;

public class BulbManager : MonoBehaviour
{
    private Renderer rd;
    public bool on = false;

    // Start is called before the first frame update
    void Start()
    {
        rd = GetComponent<MeshRenderer>();

        if (on)
            TurnOn();
    }

    public void TurnOn()
    {
        on = true;
        rd.material.color = Color.yellow;

    }
    public void TurnOff()
    {
        on = false;
        rd.material.color = Color.white;
    }
}
