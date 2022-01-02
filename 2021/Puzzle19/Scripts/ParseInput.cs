using System.Collections.Generic;
using UnityEngine;

public class ParseInput : MonoBehaviour
{
    public TextAsset input;
    List<string> lines;
    public List<List<Vector3>> scanners;
    public int scannerCount;

    public void Parse()
    {
        lines = new List<string>();
        lines.AddRange(input.text.Split('\n'));

        scanners = new List<List<Vector3>>();
        scannerCount = 0;
        foreach (string l in lines)
        {
            if (l.Length < 2)
            {
                scannerCount++;
            }
            else if (l[1] == '-')
            {
                scanners.Add(new List<Vector3>());
            }
            else
            {
                string[] coords = l.Split(',');
                scanners[scannerCount].Add(new Vector3(int.Parse(coords[0]), int.Parse(coords[1]), int.Parse(coords[2])));
            }
        }
        scannerCount++;
    }
}
