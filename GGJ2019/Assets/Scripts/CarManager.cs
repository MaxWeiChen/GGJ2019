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
    public int CarCount = 30;
    public List<Car> carlist = new List<Car>();
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
            
            go.transform.localPosition = transform.position;
            m_pool.Enqueue(go);
            go.gameObject.SetActive(false);
            
        }
      
    }
    void Start()
    {
        float lastPos = 0;
        for (int cnt = 0; cnt < CarCount; cnt++)
        {
            float route = Random.Range(5,20);
            lastPos += route;
            ReUse(Vector3.zero, Quaternion.Euler(0, 0, 0), lastPos);
        }
    }
    // Update is called once per frame
    void Update () {
        timer += Time.deltaTime;
        if(timer>= carBornRate)
        {
           
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
            carlist.Add(reuse);
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
            carlist.Add(go);
        }

    }


    public void Recovery(Car recovery)
    {
        carlist.Remove(recovery);
        m_pool.Enqueue(recovery);
        recovery.gameObject.SetActive(false);
        ReUse(Vector3.zero, Quaternion.Euler(0, 0, 0), 0);
    }
}
