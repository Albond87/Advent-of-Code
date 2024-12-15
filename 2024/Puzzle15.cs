public class Puzzle15 : Puzzle
{
    HashSet<int> walls = [];
    HashSet<int> boxes = [];
    int robotpos = 0;
    HashSet<int> walls2 = [];
    HashSet<int> boxes2 = [];
    int robotpos2 = 0;
    string moves;

    public Puzzle15() : base("15") {
        moves = "";
        for (int y=0; y<inputs.Length; y++) {
            if (inputs[y] == "") {
                moves = string.Join("", inputs[(y+1)..]);
            }
            for (int x=0; x<inputs[y].Length; x++) {
                if (inputs[y][x] == '#') {
                    walls.Add(y*100 + x);
                    walls2.Add((y*100) + (x*2));
                    walls2.Add((y*100) + (x*2) + 1);
                }
                else if (inputs[y][x] == 'O') {
                    boxes.Add(y*100 + x);
                    boxes2.Add((y*100) + (x*2));
                }
                else if (inputs[y][x] == '@') {
                    robotpos = y*100 + x;
                    robotpos2 = (y*100) + (x*2);
                }
            }
        }
    }

    public override void Part1()
    {
        foreach (var m in moves) {
            int deltapos = 0;
            switch (m) {
                case '>':
                    deltapos = 1;
                    break;
                case '<':
                    deltapos = -1;
                    break;
                case '^':
                    deltapos = -100;
                    break;
                case 'v':
                    deltapos = 100;
                    break;
            }
            int newpos = robotpos + deltapos;
            int checkpos = newpos;
            bool pushbox = false;
            while (true) {
                if (walls.Contains(checkpos)) {
                    // Hit a wall - can't move in that direction
                    break;
                }
                else if (boxes.Contains(checkpos)) {
                    pushbox = true;
                    checkpos += deltapos;
                }
                else {
                    // Empty space - move in the direction and push boxes if applicable
                    robotpos = newpos;
                    if (pushbox) {
                        boxes.Remove(newpos);
                        boxes.Add(checkpos);
                    }
                    break;
                }
            }
        }
        Console.WriteLine(boxes.Sum());
    }

    public override void Part2()
    {
        foreach (var m in moves) {
            bool vertical = false;
            int deltapos = 0;
            int boxcheckpos = 0;
            switch (m) {
                case '>':
                    deltapos = 1;
                    boxcheckpos = robotpos2 + 1;
                    break;
                case '<':
                    deltapos = -1;
                    boxcheckpos = robotpos2 - 2;
                    break;
                case '^':
                    deltapos = -100;
                    vertical = true;
                    break;
                case 'v':
                    deltapos = 100;
                    vertical = true;
                    break;
            }
            int newpos = robotpos2 + deltapos;
            if (!vertical) {
                // Moving horizontally
                if (walls2.Contains(newpos)) {
                    // Hit a wall - can't move in that direction
                    continue;
                }
                else if (!boxes2.Contains(boxcheckpos)) {
                    // Empty space - can simply move into it with no boxes to push
                    robotpos2 = newpos;
                    continue;
                }
                List<int> pushboxes = [boxcheckpos];
                boxcheckpos += deltapos * 2;
                int checkpos = newpos + deltapos * 2;
                while (true) {
                    if (walls2.Contains(checkpos)) {
                        // Hit a wall - can't move in that direction
                        break;
                    }
                    else if (boxes2.Contains(boxcheckpos)) {
                        // Keep track of every box to push (if possible)
                        pushboxes.Add(boxcheckpos);
                        boxcheckpos += deltapos * 2;
                        checkpos += deltapos * 2;
                    }
                    else {
                        // Empty space - move in the direction and push boxes
                        robotpos2 = newpos;
                        foreach (var b in pushboxes) {
                            boxes2.Remove(b);
                            boxes2.Add(b + deltapos);
                        }
                        break;
                    }
                }
            }
            else {
                // Moving vertically
                if (walls2.Contains(newpos)) {
                    // Hit a wall - can't move in that direction
                    continue;
                }
                if (!boxes2.Contains(newpos) && !boxes2.Contains(newpos-1)) {
                    // Empty space - can simply move into it with no boxes to push
                    robotpos2 = newpos;
                    continue;
                }
                List<int[]> pushboxes = [];
                pushboxes.Add([boxes2.Contains(newpos) ? newpos : newpos - 1]);
                bool done = false;
                while (!done) {
                    HashSet<int> boxlayer = [];
                    foreach (var b in pushboxes.Last()) {
                        // Check if each box in the furthest "layer" can move
                        int checkpos = b + deltapos;
                        if (walls2.Contains(checkpos) || walls2.Contains(checkpos+1)) {
                            // Hit a wall - can't move in that direction
                            done = true;
                            break;
                        }
                        // Three possible ways for a box to push another box
                        if (boxes2.Contains(checkpos-1)) boxlayer.Add(checkpos-1);
                        if (boxes2.Contains(checkpos)) boxlayer.Add(checkpos);
                        if (boxes2.Contains(checkpos+1)) boxlayer.Add(checkpos+1);
                    }
                    if (!done) {
                        if (boxlayer.Count == 0) {
                            // All spaces ahead of the furthest boxes are empty so move and push boxes
                            robotpos2 = newpos;
                            for (int layer=pushboxes.Count-1; layer>=0; layer--) {
                                foreach (var b in pushboxes[layer]) {
                                    boxes2.Remove(b);
                                    boxes2.Add(b + deltapos);
                                }
                            }
                            done = true;
                        }
                        else {
                            // More boxes to push so add another layer
                            pushboxes.Add(boxlayer.ToArray());
                        }
                    }
                }
            }
        }
        // Output final layout
        // for (int y=0; y<50; y++) {
        //     for (int x=0; x<100; x++) {
        //         int p = y*100 + x;
        //         if (p == robotpos2) Console.Write("@");
        //         else if (walls2.Contains(p)) Console.Write("#");
        //         else if (boxes2.Contains(p)) {
        //             Console.Write("[]");
        //             x++;
        //         }
        //         else Console.Write(".");
        //     }
        //     Console.Write("\n");
        // }
        Console.WriteLine(boxes2.Sum());
    }
}