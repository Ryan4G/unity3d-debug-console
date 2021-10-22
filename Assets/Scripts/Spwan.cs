using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Spwan : MonoBehaviour
{
    public List<Material> m_materials;

    public Transform m_prefab;

    int m_timer = 5;

    private bool isCube = false;

    // Start is called before the first frame update
    void Start()
    {
        isCube = m_prefab.name.CompareTo("Cube") == 0;
        StartCoroutine(CreateSpawn());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator CreateSpawn()
    {
        do
        {
            yield return new WaitForSeconds(1);
        }
        while (--m_timer > 0);

        Vector3 pos = this.transform.position;

        pos.x += Random.Range(-3, 3);
        pos.y += Random.Range(1, 3);
        pos.z += Random.Range(-3, 3);

        Transform t = Instantiate(m_prefab, pos, Quaternion.identity);
        MeshRenderer mesh = t.GetComponent<MeshRenderer>();

        var randIdx = Mathf.FloorToInt(Random.Range(0, m_materials.Count));
        mesh.material = m_materials[randIdx];

        GameManager.Instance.UpdateAmount(isCube, true);

        yield return t;

        m_timer = Random.Range(4, 6);

        StartCoroutine(CreateSpawn());
    }
}
