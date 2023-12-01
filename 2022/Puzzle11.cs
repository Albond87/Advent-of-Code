public static class Puzzle11
{
    struct Monkey {
        public List<long> items;
        public bool operation; // true: *  false: +
        public bool opOld; // true for operations like old+old
        public int operand;
        public int divTest;
        public int trueMonkey;
        public int falseMonkey;
    }

    public static int Part1(string[] lines)
    {
        int rounds = 20;
        List<Monkey> monkeys = new List<Monkey>();
        List<int> inspected = new List<int>();
        for (int i=1; i<lines.Length; i+=7) {
            Monkey m = new Monkey();
            m.items = lines[i].Split(": ")[1].Split(", ").Select(a=>long.Parse(a)).ToList();
            m.operation = lines[i+1][23]=='*' ? true : false;
            if (lines[i+1][25] == 'o')
                m.opOld = true;
            else 
                m.operand = int.Parse(lines[i+1].Substring(25));
            m.divTest = int.Parse(lines[i+2].Substring(21));
            m.trueMonkey = int.Parse(lines[i+3].Substring(29));
            m.falseMonkey = int.Parse(lines[i+4].Substring(30));
            inspected.Add(0);
            monkeys.Add(m);
        }

        for (int ml=0; ml<monkeys.Count(); ml++) {
            foreach (int i in monkeys[ml].items) {
                int mi = ml;
                long item = i;
                for (int r=0; r<rounds; r++) {
                    Monkey m = monkeys[mi];
                    inspected[mi]++;
                    if (m.operation) item *= m.opOld ? item : m.operand;
                    else item += m.opOld ? item : m.operand;
                    item /= 3;
                    int target;
                    if (item % m.divTest == 0) target = m.trueMonkey;
                    else target = m.falseMonkey;
                    if (target>mi) r--;
                    mi = target;
                }
            }
        }

        return inspected.OrderDescending().Take(2).Aggregate(1, (acc, val) => acc * val);
    }

    public static long Part2(string[] lines)
    {
        int rounds = 10000;
        int modField = 1;
        List<Monkey> monkeys = new List<Monkey>();
        List<long> inspected = new List<long>();
        for (int i=1; i<lines.Length; i+=7) {
            Monkey m = new Monkey();
            m.items = lines[i].Split(": ")[1].Split(", ").Select(a=>long.Parse(a)).ToList();
            m.operation = lines[i+1][23]=='*' ? true : false;
            if (lines[i+1][25] == 'o')
                m.opOld = true;
            else 
                m.operand = int.Parse(lines[i+1].Substring(25));
            m.divTest = int.Parse(lines[i+2].Substring(21));
            modField *= m.divTest;
            m.trueMonkey = int.Parse(lines[i+3].Substring(29));
            m.falseMonkey = int.Parse(lines[i+4].Substring(30));
            inspected.Add(0);
            monkeys.Add(m);
        }

        for (int ml=0; ml<monkeys.Count(); ml++) {
            foreach (int i in monkeys[ml].items) {
                int mi = ml;
                long item = i;
                for (int r=0; r<rounds; r++) {
                    Monkey m = monkeys[mi];
                    inspected[mi]++;
                    if (m.operation) item *= m.opOld ? item : m.operand;
                    else item += m.opOld ? item : m.operand;
                    item %= modField;
                    int target;
                    if (item % m.divTest == 0) target = m.trueMonkey;
                    else target = m.falseMonkey;
                    if (target>mi) r--;
                    mi = target;
                }
            }
        }

        List<long> top2 = inspected.OrderDescending().Take(2).ToList();
        return top2[0] * top2[1];
    }
}