using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour {
   
    public GameObject[] carPrefabs;
    public float timer = 0;
    public float carBornRate = 10;
    public int initailSize = 20;
    private Queue<GameObject> m_pool = new Queue<GameObject>();
    public static CarManager instance;
    public CinemachinePathBase[] Path;
  
    // Use this for initialization
    void Start () {
		
	}
    void Awake()
    {
        instance = this;
        for (int cnt = 0; cnt < initailSize; cnt++)
        {
            int index = Random.Range(0, carPrefabs.Length);
            GameObject go = Instantiate(carPrefabs[index]) as GameObject;
            int pathrandom = Random.Range(0, Path.Length);
            go.GetComponent<CinemachineDollyCart>().m_Path = Path[pathrandom];
            go.transform.SetParent(transform);
            if (Path[pathrandom].name == "Path")
                go.GetComponent<Car>().RecyclePosition = 860;
            else
                go.GetComponent<Car>().RecyclePosition = 1014;
            
            go.transform.localPosition = transform.position;
            m_pool.Enqueue(go);
            go.SetActive(false);
            
        }
      
    }
    // Update is called once per frame
    void Update () {
        timer += Time.deltaTime;
        if(timer>= carBornRate)
        {
            int rate = Random.Range(0,2);
            if(rate==0)
                ReUse(Vector3.zero, Quaternion.Euler(0, 0, 0));
            else
                ReUse(Vector3.zero, Quaternion.Euler(0, 0, 0));

            timer = 0;
        }

    }
    public void ReUse(Vector3 position, Quaternion rotation )
    {
        if (m_pool.Count > 0)
        {
            GameObject reuse = m_pool.Dequeue();
            reuse.transform.SetParent(transform);
            reuse.SetActive(true);
           
        }
        else
        {
            int index = Random.Range(0, carPrefabs.Length);
            GameObject go = Instantiate(carPrefabs[index]) as GameObject;
            go.transform.SetParent(transform);
            go.transform.localPosition = transform.position;
            int pathrandom = Random.Range(0, Path.Length);
            go.GetComponent<CinemachineDollyCart>().m_Path = Path[pathrandom];
            if (Path[pathrandom].name == "Path")
                go.GetComponent<Car>().RecyclePosition = 860;
            else
                go.GetComponent<Car>().RecyclePosition = 1014;

        }
    }


    public void Recovery(GameObject recovery)
    {
        m_pool.Enqueue(recovery);
        recovery.SetActive(false);
    }
}
