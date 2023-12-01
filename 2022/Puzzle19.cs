public static class Puzzle19
{
    struct BlueprintCosts {
        public BlueprintCosts(int ore, int clay, int[] obsidian, int[] geode) {
            this.ore = ore;
            this.clay = clay;
            this.obsidian = obsidian;
            this.geode = geode;
        }

        public int ore;
        public int clay;
        public int[] obsidian;
        public int[] geode;
    }

    static int[] AddIncrements(int[] original, int[] increments) {
        int[] result = new int[]{0,0,0,0};
        original.CopyTo(result,0);
        for (int i=0; i<result.Length; i++) {
            result[i] += increments[i];
        }
        return result;
    }

    static int MaxGeodes(ref BlueprintCosts b, int[] robots, int[] materials, int minute, ref Dictionary<string,int> cache) {
        if (minute==0) return materials[3];
        string key=minute.ToString()+","+robots[0]+","+robots[1]+","+robots[2]+","+robots[3]+","+materials[0]+","+materials[1]+","+materials[2]+","+materials[3];
        if (cache.ContainsKey(key)) return cache[key];
        List<int> results = new List<int>();
        if (materials[0] >= b.geode[0] && materials[2] >= b.geode[1]) {
            int[] materialsNext = AddIncrements(materials, robots);
            materialsNext[0] -= b.geode[0];
            materialsNext[2] -= b.geode[1];
            results.Add(MaxGeodes(ref b, new int[] {robots[0], robots[1], robots[2], robots[3]+1}, materialsNext, minute-1, ref cache));
        }
        else if (minute>2 && materials[0] >= b.obsidian[0] && materials[1] >= b.obsidian[1] && robots[2] < b.geode[1]) {
            int[] materialsNext = AddIncrements(materials, robots);
            materialsNext[0] -= b.obsidian[0];
            materialsNext[1] -= b.obsidian[1];
            results.Add(MaxGeodes(ref b, new int[] {robots[0], robots[1], robots[2]+1, robots[3]}, materialsNext, minute-1, ref cache));
        }
        else {
            if (materials[0] >= b.ore && (robots[0] < b.ore || robots[0] < b.clay || robots[0] < b.obsidian[0] || robots[0] < b.geode[0])) {
                int[] materialsNext = AddIncrements(materials, robots);
                materialsNext[0] -= b.ore;
                results.Add(MaxGeodes(ref b, new int[] {robots[0]+1, robots[1], robots[2], robots[3]}, materialsNext, minute-1, ref cache));
            }
            if (materials[0] >= b.clay && robots[1] < b.obsidian[1]) {
                int[] materialsNext = AddIncrements(materials, robots);
                materialsNext[0] -= b.clay;
                results.Add(MaxGeodes(ref b, new int[] {robots[0], robots[1]+1, robots[2], robots[3]}, materialsNext, minute-1, ref cache));
            }            
            if (minute>0) {
                int[] materialsNext = AddIncrements(materials, robots);
                results.Add(MaxGeodes(ref b, new int[] {robots[0], robots[1], robots[2], robots[3]}, materialsNext, minute-1, ref cache));
            }
        }
        int max = results.Max();
        cache[key] = max;
        return max;
    }

    public static int Part1(string[] lines)
    {
        List<BlueprintCosts> blueprints = new List<BlueprintCosts>();
        foreach (string line in lines) {
            string[] parts = line.Split(new string[]{" costs ", " ore.", " ore and ", " clay.", " obsidian."},StringSplitOptions.RemoveEmptyEntries);
            BlueprintCosts b = new BlueprintCosts(
                    int.Parse(parts[1]),
                    int.Parse(parts[3]),
                    new int[]{int.Parse(parts[5]),int.Parse(parts[6])},
                    new int[]{int.Parse(parts[8]),int.Parse(parts[9])});
            blueprints.Add(b);
        }
        int sum=0;
        for (int i=0; i<blueprints.Count(); i++) {
            BlueprintCosts b = blueprints[i];
            Dictionary<string,int> cache = new Dictionary<string, int>();
            int geodes = MaxGeodes(ref b,new int[]{1,0,0,0},new int[]{0,0,0,0},24,ref cache);
            sum+=geodes*(i+1);
        }
        return sum;
    }

    public static int Part2(string[] lines)
    {
        List<BlueprintCosts> blueprints = new List<BlueprintCosts>();
        foreach (string line in lines) {
            string[] parts = line.Split(new string[]{" costs ", " ore.", " ore and ", " clay.", " obsidian."},StringSplitOptions.RemoveEmptyEntries);
            BlueprintCosts b = new BlueprintCosts(
                    int.Parse(parts[1]),
                    int.Parse(parts[3]),
                    new int[]{int.Parse(parts[5]),int.Parse(parts[6])},
                    new int[]{int.Parse(parts[8]),int.Parse(parts[9])});
            blueprints.Add(b);
        }
        int product=1;
        for (int i=0; i<3; i++) {
            BlueprintCosts b = blueprints[i];
            Dictionary<string,int> cache = new Dictionary<string, int>();
            int geodes = MaxGeodes(ref b,new int[]{1,0,0,0},new int[]{0,0,0,0},32,ref cache);
            product*=geodes;
        }
        return product;
    }
}