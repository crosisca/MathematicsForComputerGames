using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BitboardTests : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        long l = SetCellState(64, 1, 5);


        Debug.Log(l);

        Debug.Log(GetCellState(l, 1, 5));
        Debug.Log(GetCellState(l, 1, 6));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    long SetCellState(long bitboard, int row, int collum, int bitboardWidth = 8)
    {
        long newBit = 1L << (row * bitboardWidth + collum);

        return (bitboard |= newBit); // |= adds the bitsequences
    }

    bool GetCellState(long bitboard, int row, int col, int bitboardWidth = 8)
    {
        long mask = 1L << (row * bitboardWidth + col);

        return (bitboard & mask) != 0; //courses answer
        return (bitboard & mask) == mask; //mine
    }

    int CellCount(long bitboard)
    {
        int count = 0;
        long bb = bitboard;

        while (bb != 0)
        {
            bb &= bb - 1;
            count++;
        }

        return count;
    }
}
