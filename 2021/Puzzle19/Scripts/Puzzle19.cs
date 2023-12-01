using System.Collections.Generic;
using UnityEngine;

public class Puzzle19 : MonoBehaviour
{
    public ParseInput parser;
    List<List<Vector3>> scanners;
    int scannerCount;

    public GameObject scanner;
    public GameObject scannerBig;
    public GameObject beacon;
    Vector3 scannerScale = new Vector3(20, 20, 20);
    Vector3 beaconScale = new Vector3(10, 10, 10);

    List<Transform> scannerObjects;
    List<bool> placed;
    int placeCount;
    public Quaternion[] rotations;
    Transform map;
    bool placeFound;

    //float last = 0;
    //int count = 1;

    void Start()
    {
        UnityEditor.SceneView.FocusWindowIfItsOpen(typeof(UnityEditor.SceneView));
        parser.Parse();
        scanners = parser.scanners;
        scannerCount = parser.scannerCount;

        scannerObjects = new List<Transform>();
        placed = new List<bool>();

        foreach (List<Vector3> s in scanners)
        {
            Transform newScanner = Instantiate(scanner).transform;
            Instantiate(scannerBig, newScanner);
            for (int c = 0; c < s.Count; c++)
            {
                GameObject b = Instantiate(beacon, newScanner);
                b.name = c.ToString();
                b.transform.localPosition = s[c];
            }
            scannerObjects.Add(newScanner);
            placed.Add(false);
        }

        placed[0] = true;
        placeCount = 1;
        setParent(scannerObjects[0], scannerObjects[0].GetChild(1));
        map = scannerObjects[0].GetChild(1);
    }

    void Update()
    {
        placeFound = false;
        for (int s = 0; s < scannerCount; s++)
        {
            if (placed[s]) continue;

            Transform current = scannerObjects[s];
            if (placeScanner(current))
            {
                placeFound = true;
                placed[s] = true;
                placeCount++;
                current.gameObject.SetActive(false);
                if (placeCount == scannerCount)
                {
                    Debug.Log(map.childCount+1);
                    gameObject.GetComponent<Puzzle19>().enabled = false;
                }
            }
        }
        //gameObject.GetComponent<Puzzle19>().enabled = false;
        if (!placeFound)
        {
            detachChildren(scannerObjects[0], map, scannerScale);
            setParent(scannerObjects[0], scannerObjects[0].GetChild(scannerObjects[0].childCount - 1));
            map = scannerObjects[0].GetChild(1);
        }
    }

    List<Transform> setParent(Transform o, Transform parent)
    {
        parent.localScale = new Vector3(1, 1, 1);
        List<Transform> children = new List<Transform>();
        int childCount = o.childCount;
        int i = 1;
        for (int c = 1; c < childCount; c++)
        {
            Transform child = o.GetChild(i);
            if (child == parent)
            {
                i++;
            }
            else
            {
                child.SetParent(parent, true);
                child.localPosition = new Vector3(Mathf.Round(child.localPosition.x), Mathf.Round(child.localPosition.y), Mathf.Round(child.localPosition.z));
                children.Add(child);
            }
        }
        return children;
    }

    void detachChildren(Transform o, Transform parent, Vector3 resetScale)
    {
        int childCount = parent.childCount;
        for (int c = 0; c < childCount; c++)
        {
            parent.GetChild(0).SetParent(o, true);
        }
        parent.localScale = resetScale;
    }

    bool placeScanner(Transform parent)
    {
        Transform rotator = parent.GetChild(0);
        foreach (Quaternion r in rotations)
        {
            setParent(parent, rotator);
            rotator.localRotation = r;
            detachChildren(parent, rotator, scannerScale);
            //Debug.Break();
            List<Transform> children;
            for (int i = 1; i < parent.childCount; i++)
            {
                parent.GetChild(i).localRotation = rotations[0];
                children = setParent(parent, parent.GetChild(i));
                //Debug.Break();
                int matches = 0;
                List<Transform> nonMatches = new List<Transform>();
                bool match;
                foreach (Transform c in children)
                {
                    match = false;
                    //Vector3 pos = new Vector3(Mathf.Round(c.localPosition.x), Mathf.Round(c.localPosition.y), Mathf.Round(c.localPosition.z));
                    for (int m = 0; m < map.childCount; m++)
                    {
                        if (map.GetChild(m).localPosition == c.localPosition)
                        {
                            matches++;
                            match = true;
                            break;
                        }
                    }
                    if (!match) nonMatches.Add(c);
                }
                if (matches >= 11)
                {
                    foreach (Transform c in nonMatches)
                    {
                        c.SetParent(map, false);
                    }
                    Transform s = parent.GetChild(0);
                    s.SetParent(parent.GetChild(1), true);
                    s.SetParent(map, false);
                    s.SetParent(transform, true);

                    return true;
                }
                detachChildren(parent, parent.GetChild(1), beaconScale);
                //Debug.Break();
            }
        }
        return false;
    }

    /*private void Update()
    {
        if (Time.realtimeSinceStartup > 5)
        {
            if (Time.realtimeSinceStartup - last > 1)
            {
                setParent(map.transform, map.transform.GetChild(count));
                detachChildren(map.transform, map.transform.GetChild(1));
                count++;
                last = Time.realtimeSinceStartup;
                /*transform.rotation = rotations[count];
                count = (count+1)%24;
                last = Time.realtimeSinceStartup;
            }
        }
    }*/
}
