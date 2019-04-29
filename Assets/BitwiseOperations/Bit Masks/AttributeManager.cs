using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttributeManager : MonoBehaviour
{
    public static int MAGIC = 16;
    public static int INTELLIGENCE = 8;
    public static int CHARISMA = 4;
    public static int FLY= 2;
    public static int INVISIBLE = 1;

    public Text attributeDisplay;
    public int attributes = 0;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MAGIC")
            attributes |= MAGIC; //Add!
        else if (other.gameObject.tag == "INTELLIGENCE")
            attributes |= INTELLIGENCE; //Add!
        else if (other.gameObject.tag == "CHARISMA")
            attributes |= CHARISMA; //Add!
        else if (other.gameObject.tag == "FLY")
            attributes |= FLY; //Add!
        else if (other.gameObject.tag == "INVISIBLE")
            attributes |= INVISIBLE; //Add!
        else if (other.gameObject.tag == "ANTIMAGIC")
            attributes &= ~MAGIC; //Remove!
        else if (other.gameObject.tag == "ADD3")
            attributes |= (INTELLIGENCE | MAGIC | CHARISMA);//Add Multiple!
        else if (other.gameObject.tag == "REMOVE2")
            attributes &= ~(INTELLIGENCE | MAGIC); //Remove Multiple!
        else if (other.gameObject.tag == "WIPE")
            attributes = 0; //Remove All
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
