public static class Puzzle07
{
    public static int Part1(string[] lines)
    {
        string currentDir = "/";
        List<string> dirs = new List<string>{"/"};
        int dirNameSize = 0;
        Dictionary<string, int> dirSizes = new System.Collections.Generic.Dictionary<string, int>();
        dirSizes.Add("/",0);
        for (int i=0; i<lines.Length; i++) {
            if (lines[i].StartsWith("$ cd")) {
                string dir = lines[i].Substring(5);
                if (dir == "/") {
                    currentDir = "/";
                    dirs = new List<string>{"/"};
                }
                else if (dir == "..") {
                    dirs.RemoveAt(dirs.Count - 1);
                    currentDir = dirs.ElementAt(dirs.Count - 1);
                }
                else {
                    dirNameSize = dir.Length;
                    currentDir += dir + "/";
                    dirs.Add(currentDir);
                }
            } 
            else if (lines[i] == "$ ls") {
                i += 1;
                while (!lines[i].StartsWith("$")) {
                    if (lines[i].StartsWith("dir")) dirSizes.Add(currentDir + lines[i].Substring(4) + "/", 0);
                    else {
                        foreach (string d in dirs)
                            dirSizes[d] += int.Parse(lines[i].Split(" ")[0]);
                    }
                    i += 1;
                    if (i == lines.Length) break;
                }
                i -=1 ;
            }
        }
        int total = 0;
        foreach (string k in dirSizes.Keys) {
            int size = dirSizes[k];
            if (size <= 100000) total += size;
        }            
        return total;
    }

    public static int Part2(string[] lines)
    {
        string currentDir = "/";
        List<string> dirs = new List<string>{"/"};
        int dirNameSize = 0;
        Dictionary<string, int> dirSizes = new System.Collections.Generic.Dictionary<string, int>();
        dirSizes.Add("/",0);
        for (int i=0; i<lines.Length; i++) {
            if (lines[i].StartsWith("$ cd")) {
                string dir = lines[i].Substring(5);
                if (dir == "/") {
                    currentDir = "/";
                    dirs = new List<string>{"/"};
                }
                else if (dir == "..") {
                    dirs.RemoveAt(dirs.Count - 1);
                    currentDir = dirs.ElementAt(dirs.Count - 1);
                }
                else {
                    dirNameSize = dir.Length;
                    currentDir += dir + "/";
                    dirs.Add(currentDir);
                }
            } 
            else if (lines[i] == "$ ls") {
                i += 1;
                while (!lines[i].StartsWith("$")) {
                    if (lines[i].StartsWith("dir")) dirSizes.Add(currentDir + lines[i].Substring(4) + "/", 0);
                    else {
                        foreach (string d in dirs)
                            dirSizes[d] += int.Parse(lines[i].Split(" ")[0]);
                    }
                    i += 1;
                    if (i == lines.Length) break;
                }
                i -=1 ;
            }
        }
        int target = 30000000-(70000000-dirSizes["/"]);
        int smallest = dirSizes["/"];
        foreach (string k in dirSizes.Keys) {
            int size = dirSizes[k];
            if (size > target && size < smallest) smallest = size;
        }            
        return smallest;
    }
}