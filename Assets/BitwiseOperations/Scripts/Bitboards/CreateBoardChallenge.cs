using UnityEngine;
using System;
using UnityEngine.UI;

public class CreateBoardChallenge : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public GameObject housePrefab;
    public GameObject treePrefab;

    public Text score;

    GameObject[] tiles;

    long dirtBB; // long = 64 bits
    long desertBB;
    long treeBB;
    long playerBB;

    // Start is called before the first frame update
    void Start ()
    {
        tiles = new GameObject[64];
        for (int row = 0; row < 8; row++)
        {
            for (int column = 0; column < 8; column++)
            {
                int randomTile = UnityEngine.Random.Range(0, tilePrefabs.Length);
                Vector3 pos = new Vector3(column, 0, row);
                GameObject tile = Instantiate(tilePrefabs[randomTile], pos, Quaternion.identity);
                tile.name = $"{tile.tag}_{row}_{column}";

                tiles[row * 8 + column] = tile;

                if (tile.tag == "Dirt")
                {
                    dirtBB = SetCellState(dirtBB, row, column);
                    //PrintBitboard("Dirt", dirtBB);
                }
                else if (tile.tag == "Desert")
                    desertBB = SetCellState(desertBB, row, column);
            }
        }
        Debug.Log($"Dirt cells = {CellCount(dirtBB)}");
        InvokeRepeating("PlantTree", 1, 1);
    }

    void PlantTree ()
    {
        int randomRow = UnityEngine.Random.Range(0, 8);
        int randomColumn = UnityEngine.Random.Range(0, 8);

        if (GetCellState(dirtBB & ~playerBB, randomRow, randomColumn)) // dont plant on dirt cells that are occupied by a house(playerBB)..remove playerBB from dirtBB
        {
            GameObject tree = Instantiate(treePrefab);
            tree.transform.parent = tiles[randomRow * 8 + randomColumn].transform;
            tree.transform.localPosition = Vector3.zero;
            treeBB = SetCellState(treeBB, randomRow, randomColumn);
        }
    }

    long SetCellState (long bitboard, int row, int column)
    {
        long newBitboard = 1L << (row * 8 + column); //8 = bitboard width

        return (bitboard |= newBitboard);
    }

    bool GetCellState (long bitboard, int row, int col)
    {
        long mask = 1L << (row * 8 + col);

        return (bitboard & mask) != 0;
    }

    void PrintBitboard (string name, long bitboard)
    {
        Debug.Log($"{name}: {Convert.ToString(bitboard, 2).PadLeft(64, '0')}");
    }

    int CellCount (long bitboard)
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

    void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                //Only put houses on dirt tiles
                int row = (int)hit.transform.position.z;
                int column = (int)hit.transform.position.x;

                if (GetCellState((dirtBB & ~treeBB) | desertBB, row, column)) // ignores check for trees on deserts (cuz they dont grow there anyway)
                //if (GetCellState((dirtBB | desertBB) & ~treeBB, row, column))//is dirt (or desert) and is not tree..(my solution), was also checking for trees on desert tiles
                {
                    
                    GameObject house = Instantiate(housePrefab);
                    house.transform.parent = hit.collider.gameObject.transform;
                    house.transform.localPosition = Vector3.zero;
                    playerBB = SetCellState(playerBB, row, column);
                    CalculateScore();
                }


                //if (GetCellState(dirtBB, row, column))//is a dirt tile
                //{
                //    if (GetCellState(treeBB, row, column) == false) //does not have a tree there
                //    {
                //        GameObject house = Instantiate(housePrefab);
                //        house.transform.parent = hit.collider.gameObject.transform;
                //        house.transform.localPosition = Vector3.zero;
                //        playerBB = SetCellState(playerBB, (int)hit.transform.position.z, (int)hit.transform.position.x);
                //    }
                //}
            }
        }
    }

    void CalculateScore ()
    {
        int points = 0;

        points += CellCount(playerBB & dirtBB) * 10;
        points += CellCount(playerBB & desertBB) * 2;

        score.text = $"Score: {points}";

        //build houses on dirt and desert - OK
        //house on dirt = 10 points
        //house on desert = 2 points
    }
}

