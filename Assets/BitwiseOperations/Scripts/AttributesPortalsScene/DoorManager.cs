using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    int doorType = AttributeManager.MAGIC;

    void OnCollisionEnter(Collision col)
    {
        if ((col.gameObject.GetComponent<AttributeManager>().attributes & doorType) != 0)
        {
            //go trhough
            GetComponent<BoxCollider>().isTrigger = true;

            //col.gameObject.GetComponent<AttributeManager>().attributes &= ~doorType;// (use key -> trigger bit back to 0)
        }
    }

    void OnTriggerExit(Collider other)
    {
        GetComponent<BoxCollider>().isTrigger = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
