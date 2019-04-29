using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager2 : MonoBehaviour
{
    int doorType = AttributeManager.MAGIC;

    void OnCollisionEnter(Collision col)
    {
        if ((col.gameObject.GetComponent<AttributeManager2>().attributes & doorType) == doorType) // has both keys (in case purple)
        //if ((col.gameObject.GetComponent<AttributeManager2>().attributes & doorType) != 0) // has Any key (in case purple)
        {
            //go trhough
            GetComponent<BoxCollider>().isTrigger = true;

            col.gameObject.GetComponent<AttributeManager2>().attributes &= ~doorType;// (use key -> trigger bit back to 0)
        }
    }

    void OnTriggerExit(Collider other)
    {
        GetComponent<BoxCollider>().isTrigger = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.tag == "BLUE_DOOR")
            doorType = AttributeManager2.BLUE_KEY;
        else if (gameObject.tag == "RED_DOOR")
            doorType = AttributeManager2.RED_KEY;
        else if (gameObject.tag == "PURPLE_DOOR")
            doorType = AttributeManager2.BLUE_KEY | AttributeManager2.RED_KEY;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
