public static class Puzzle13
{
    static int InOrder(string left, string right) {
        int lc, rc; 
        while (true) {
            lc = 0; rc = 0;
            if (left[0] == ']') {
                if (right[0] == ']') return 0;
                else return 1;
            }
            else if (right[0] == ']') return -1;
            else if (left[0] == '[') {
                int level = 1;
                while (level > 0) {
                    lc++;
                    if (left[lc] == '[') level++;
                    else if (left[lc] == ']') level--;
                }
                if (right[0] == '[') {
                    level = 1;
                    while (level > 0) {
                        rc++;
                        if (right[rc] == '[') level++;
                        else if (right[rc] == ']') level--;
                    }
                    int comp = InOrder(left.Substring(1,lc),right.Substring(1,rc));
                    if (comp != 0) return comp;
                    rc++;
                }
                else {
                    while (right[rc] != ',' && right[rc] != ']') rc++;
                    int comp = InOrder(left.Substring(1,lc),right.Substring(0,rc)+"]");
                    if (comp != 0) return comp;
                }
                lc++;
            }            
            else if (right[0] == '[') {
                int level = 1;
                while (level > 0) {
                    rc++;
                    if (right[rc] == '[') level++;
                    else if (right[rc] == ']') level--;
                }
                while (left[lc] != ',' && left[lc] != ']') lc++;
                int comp = InOrder(left.Substring(0,lc)+"]",right.Substring(1,rc));
                if (comp != 0) return comp;
                rc++;
            }            
            else {
                while (left[lc]!=',' && left[lc]!=']') lc++;
                while (right[rc]!=',' && right[rc]!=']') rc++;
                int l=int.Parse(left.Substring(0,lc));
                int r=int.Parse(right.Substring(0,rc));
                if (l<r) return 1;
                else if (l>r) return -1;
                else {
                    
                }
            }
            if (left[lc] == ']') {
                if (right[rc] == ']') return 0;
                else return 1;
            }
            else if (right[rc] == ']') return -1;
            left = left.Substring(lc+1);
            right = right.Substring(rc+1);            
        }
    }

    class Packet : IComparable {
        string data;

        public Packet(string s) {
            this.data = s;
        }

        public int CompareTo(object? obj) {
            if (obj == null) return 1;
            Packet? otherPacket = obj as Packet;
            if (otherPacket != null)
                return InOrder(otherPacket.data.Substring(1),this.data.Substring(1));
            else 
                throw new ArgumentException("Object is not a Packet");
        }
    }

    public static int Part1(string[] lines)
    {
        int sum = 0;
        for (int i=0; i<lines.Length; i+=3) {
            if (InOrder(lines[i].Substring(1), lines[i+1].Substring(1))==1) sum+=i/3+1;
        }
        return sum;
    }

    public static int Part2(string[] lines)
    {
        List<Packet> packets = lines.Where(l => l!="").Select(l => new Packet(l)).ToList();
        Packet divider1 = new Packet("[[2]]");
        Packet divider2 = new Packet("[[6]]");
        packets.Add(divider1);
        packets.Add(divider2);
        packets.Sort();
        return (packets.IndexOf(divider1)+1)*(packets.IndexOf(divider2)+1);
    }
}