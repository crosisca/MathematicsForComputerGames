using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttributeManager2 : MonoBehaviour
{
    public static int BLUE_KEY = 1;
    public static int RED_KEY = 2;

    public Text attributeDisplay;
    public int attributes = 0;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BLUE_KEY")
        {
            attributes |= BLUE_KEY; //Add!
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "RED_KEY")
        {
            attributes |= RED_KEY; //Add!
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "GOLD_KEY")
        {
            attributes |= RED_KEY | BLUE_KEY; //Add both
            Destroy(other.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(this.transform.position);
        attributeDisplay.transform.position = screenPoint + new Vector3(0,-50,0);

        attributeDisplay.text = Convert.ToString(attributes, 2).PadLeft(8,'0');
    }
}
