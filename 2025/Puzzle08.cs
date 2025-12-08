using System.Collections.Immutable;

public class Puzzle08 : Puzzle
{
    int[][] boxes;
    List<KeyValuePair<int[], double>> sortedDists;

    public Puzzle08() : base("08") 
    { 
        boxes = inputs.Select(i=>i.Split(',').Select(int.Parse).ToArray()).ToArray();

        Dictionary<int[], double> distances = [];
        for (int i=0; i<boxes.Length; i++)
        {
            for (int j=i+1; j<boxes.Length; j++)
            {
                int[] box1 = boxes[i];
                int[] box2 = boxes[j];
                double dist = Math.Sqrt(Math.Pow(box1[0]-box2[0],2) + Math.Pow(box1[1]-box2[1],2) + Math.Pow(box1[2]-box2[2],2));
                distances[[i,j]] = dist;
            }
        }
        sortedDists = distances.ToList();
        sortedDists.Sort((d1,d2)=>d1.Value.CompareTo(d2.Value));
    }

    public override void Part1()
    {
        Dictionary<int,int> boxCircuitMap = [];
        List<int> circuitSizes = [];

        for (int i=0; i<1000; i++)
        {
            int box1 = sortedDists[i].Key[0];
            int box2 = sortedDists[i].Key[1];
            
            if (boxCircuitMap.TryGetValue(box1, out int circuit1) && boxCircuitMap.TryGetValue(box2, out int circuit2))
            {
                if (circuit1 != circuit2)
                {
                    // Join the two circuits together by remapping boxes in one of the circuits
                    foreach (int c in boxCircuitMap.Keys)
                    {
                        if (boxCircuitMap[c] == circuit2) boxCircuitMap[c] = circuit1;
                    }
                    circuitSizes[circuit1] += circuitSizes[circuit2];
                    circuitSizes[circuit2] = 0;
                }
            }
            else if (boxCircuitMap.TryGetValue(box1, out int circuit))
            {
                boxCircuitMap[box2] = circuit;
                circuitSizes[circuit]++;
            }
            else if (boxCircuitMap.TryGetValue(box2, out circuit))
            {
                boxCircuitMap[box1] = circuit;
                circuitSizes[circuit]++;
            }
            else
            {
                boxCircuitMap[box1] = circuitSizes.Count;
                boxCircuitMap[box2] = circuitSizes.Count;
                circuitSizes.Add(2);
            }
        }
        circuitSizes.Sort();
        // Multiply the 3 largest sizes
        Console.WriteLine(circuitSizes[^1] * circuitSizes[^2] * circuitSizes[^3]);
    }

    public override void Part2()
    {
        Dictionary<int,int> boxCircuitMap = [];
        List<int> circuitSizes = [];
        int result = 0;

        for (int i=0; i<sortedDists.Count; i++)
        {
            int box1 = sortedDists[i].Key[0];
            int box2 = sortedDists[i].Key[1];
            
            if (boxCircuitMap.TryGetValue(box1, out int circuit1) && boxCircuitMap.TryGetValue(box2, out int circuit2))
            {
                if (circuit1 != circuit2)
                {
                    // Join the two circuits together by remapping boxes in one of the circuits
                    foreach (int c in boxCircuitMap.Keys)
                    {
                        if (boxCircuitMap[c] == circuit2) boxCircuitMap[c] = circuit1;
                    }
                    circuitSizes[circuit1] += circuitSizes[circuit2];
                    circuitSizes[circuit2] = 0;
                }
            }
            else if (boxCircuitMap.TryGetValue(box1, out circuit1))
            {
                boxCircuitMap[box2] = circuit1;
                circuitSizes[circuit1]++;
            }
            else if (boxCircuitMap.TryGetValue(box2, out circuit1))
            {
                boxCircuitMap[box1] = circuit1;
                circuitSizes[circuit1]++;
            }
            else
            {
                boxCircuitMap[box1] = circuitSizes.Count;
                boxCircuitMap[box2] = circuitSizes.Count;
                circuit1 = circuitSizes.Count;
                circuitSizes.Add(2);
            }
            if (circuitSizes[circuit1] == boxes.Length)
            {
                result = boxes[box1][0] * boxes[box2][0];
                break;
            }
        }
        Console.WriteLine(result);
    }
}