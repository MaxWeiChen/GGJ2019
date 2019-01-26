using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour {
   
    public Car[] carPrefabs;
    public float timer = 0;
    public float carBornRate = 10;
    public int initailSize = 30;
    private Queue<Car> m_pool = new Queue<Car>();
    public static CarManager instance;
    public CinemachinePathBase[] Path;
  
    // Use this for initialization
   
    void Awake()
    {
        instance = this;
        for (int cnt = 0; cnt < initailSize; cnt++)
        {
            int index = Random.Range(0, carPrefabs.Length);
            Car go = Instantiate(carPrefabs[index]) ;
            int pathrandom = Random.Range(0, Path.Length);
            go.GetComponent<CinemachineDollyCart>().m_Path = Path[pathrandom];
            go.transform.SetParent(transform);
            if (Path[pathrandom].name == "Path")
                go.GetComponent<Car>().RecyclePosition = 860;
            else
                go.GetComponent<Car>().RecyclePosition = 1014;
            
            go.transform.localPosition = transform.position;
            m_pool.Enqueue(go);
            go.gameObject.SetActive(false);
            
        }
      
    }
    void Start()
    {
        for (int cnt = 0; cnt < 10; cnt++)
        {
           float route = Random.Range(30, 790);
           ReUse(Vector3.zero, Quaternion.Euler(0, 0, 0),route);
        }
    }
    // Update is called once per frame
    void Update () {
        timer += Time.deltaTime;
        if(timer>= carBornRate)
        {
            ReUse(Vector3.zero, Quaternion.Euler(0, 0, 0),0);
            timer = 0;
        }

    }
    public void ReUse(Vector3 position, Quaternion rotation,float Position )
    {
        if (m_pool.Count > 0)
        {
            Car reuse = m_pool.Dequeue();
            reuse.transform.SetParent(transform);
            reuse.gameObject.SetActive(true);
            reuse.dollyCart.m_Position = Position;
        }
        else
        {
            int index = Random.Range(0, carPrefabs.Length);
            Car go = Instantiate(carPrefabs[index]) as Car;
            go.transform.SetParent(transform);
            go.transform.localPosition = transform.position;
            int pathrandom = Random.Range(0, Path.Length);
            go.GetComponent<CinemachineDollyCart>().m_Path = Path[pathrandom];
            go.GetComponent<Car>().dollyCart.m_Position = Position;
            if (Path[pathrandom].name == "Path")
                go.GetComponent<Car>().RecyclePosition = 860;
            else
                go.GetComponent<Car>().RecyclePosition = 1014;

        }

    }


    public void Recovery(Car recovery)
    {
        m_pool.Enqueue(recovery);
        recovery.gameObject.SetActive(false);
    }
}
