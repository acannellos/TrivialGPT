using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public List<Transform> tileList = new List<Transform>();

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        FillTiles();

        for (int i = 0; i < tileList.Count - 1; i++)
        {
            Gizmos.DrawLine(tileList[i].position, tileList[i + 1].position);
        }
    }

    void FillTiles()
    {
        tileList.Clear();

        foreach (Transform tile in GetComponentsInChildren<Transform>())
        {
            if (tile != transform)
            {
                tileList.Add(tile);
            }
        }
    }
}
