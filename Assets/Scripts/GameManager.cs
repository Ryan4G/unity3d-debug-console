using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Text m_gameTitle;

    private int cubeCount = 0;

    private int sphereCount = 0;

    public int CubeCount
    {
        get
        {
            return cubeCount;
        }
    }

    public int SphereCount
    {
        get
        {
            return sphereCount;
        }
    }

    public LayerMask m_layer;

    private Camera m_main;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        m_main = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Input.mousePosition;

            Ray ray = m_main.ScreenPointToRay(mousePos);

            RaycastHit info;

            bool hit = Physics.Raycast(ray, out info, 1000, m_layer);

            if (hit)
            {
                if (info.transform.tag.CompareTo("Mater") == 0)
                {
                    var isCube = info.transform.name.Contains("Cube");

                    Destroy(info.transform.gameObject);

                    GameManager.Instance.UpdateAmount(isCube, false);
                }
            }
        }
    }

    public void UpdateAmount(bool isCube, bool added)
    {
        if (isCube)
        {
            cubeCount += added ? 1 : -1;
        }
        else
        {
            sphereCount += added ? 1 : -1;
        }
        m_gameTitle.text = $"Cube:{cubeCount} Sphere:{sphereCount}";
    }
}
