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

    List<GameObject> m_maters;

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

    private void Awake()
    {
        this.m_maters = new List<GameObject>();
    }

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

                    RemoveMater(info.transform.gameObject, isCube);
                }
            }
        }
    }

    public void AddMater(GameObject go, bool isCube)
    {
        this.m_maters.Add(go);
        UpdateAmount(isCube, true);
    }

    public void RemoveMater(GameObject go, bool isCube)
    {
        this.m_maters.Remove(go);

        Destroy(go);

        UpdateAmount(isCube, false);

    }

    public void RemoveAllMaters()
    {
        int len = this.m_maters.Count;

        while (len > 0)
        {
            GameObject go = this.m_maters[0];

            this.m_maters.RemoveAt(0);

            RemoveMater(go, go.name.Contains("Cube"));

            len = this.m_maters.Count;
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

    public Transform CreateMater(Transform prefab, List<Material> materials, Vector3 pos, bool isCube)
    {
        pos.x += Random.Range(-3, 3);
        pos.y += Random.Range(1, 3);
        pos.z += Random.Range(-3, 3);

        Transform t = Instantiate(prefab, pos, Quaternion.identity);
        MeshRenderer mesh = t.GetComponent<MeshRenderer>();

        var randIdx = Mathf.FloorToInt(Random.Range(0, materials.Count));
        mesh.material = materials[randIdx];

        AddMater(t.gameObject, isCube);

        return t;

    }
}
