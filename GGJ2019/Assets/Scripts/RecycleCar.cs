using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecycleCar : MonoBehaviour {

	
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Car")
        {
            var car = other.gameObject.GetComponent<Car>();
            CarManager.instance.Recovery(car);
            print("回收");
        }
    }
}
