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

    private Transform m_tranform;

    // Start is called before the first frame update
    void Start()
    {
        m_tranform = this.transform;

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

        yield return GameManager.Instance.CreateMater(m_prefab, m_materials, m_tranform.position, isCube);

        m_timer = Random.Range(4, 6);

        StartCoroutine(CreateSpawn());
    }
}
