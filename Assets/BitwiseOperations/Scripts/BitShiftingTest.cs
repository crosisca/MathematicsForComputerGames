using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BitShiftingTest : MonoBehaviour
{
    void Awake()
    {
        string A = "110111";
        string B = "10001";
        string C = "1101";
        int aBits = Convert.ToInt32(A, 2);
        int bBits = Convert.ToInt32(B, 2);
        int cBits = Convert.ToInt32(C, 2);

        int packed = 0;

        packed = packed | (aBits << 26);
        packed = packed | (bBits << 21);//A(26) - lenght of B
        packed = packed | (cBits << 17);//B(21) - lenght of C

        Debug.Log("Awake: " + Convert.ToString(packed, 2).PadLeft(32, '0'));
    }

    void Start ()
    {
        string A = "1111";
        string B = "101";
        string C = "11011";
        int aBits = Convert.ToInt32(A, 2);
        int bBits = Convert.ToInt32(B, 2);
        int cBits = Convert.ToInt32(C, 2);

        int packed = 0;

        packed = packed | (aBits << 28);
        packed = packed | (bBits << 25);//A(26) - lenght of B
        packed = packed | (cBits << 20);//B(21) - lenght of C

        Debug.Log("Start:  " + Convert.ToString(packed, 2).PadLeft(32, '0'));

        Unpacking();
    }

    void Unpacking()
    {
        //A = 0011001
        //B = 11000
        //C = 0101
        string X =  "0011001110000101";//packed A+B+C
        int x = Convert.ToInt32(X, 2);
        int aMask = Convert.ToInt32("1111111000000000");
        int bMask = Convert.ToInt32("0000000111110000");
        int cMask = Convert.ToInt32("0000000000001111");

        int A = (x & aMask) >> 9;
        int B = (x & bMask) >> 4;
        int C = x & cMask;
        Debug.Log("A: " + A);
        Debug.Log("B: " + B);
        Debug.Log("C: " + C);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
