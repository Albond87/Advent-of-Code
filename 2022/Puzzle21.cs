public static class Puzzle21
{
    public static long Part1(string[] lines)
    {
        Dictionary<string,long> numbers = new Dictionary<string, long>();
        Dictionary<string, string[]> monkeys = new Dictionary<string, string[]>();
        foreach (string line in lines) {
            string[] parts = line.Split(": ");
            long num=0;
            if (long.TryParse(parts[1],out num)) numbers[parts[0]] = num;
            else monkeys[parts[0]] = parts[1].Split(" ");
        }
        while (true) {
            foreach (KeyValuePair<string, string[]> monkey in monkeys) {
                if (numbers.ContainsKey(monkey.Value[0]) && numbers.ContainsKey(monkey.Value[2])) {
                    long res=0;
                    switch (monkey.Value[1]) {
                        case "+":
                            res = numbers[monkey.Value[0]] + numbers[monkey.Value[2]];
                            break;
                        case "-":
                            res = numbers[monkey.Value[0]] - numbers[monkey.Value[2]];
                            break;
                        case "*":
                            res = numbers[monkey.Value[0]] * numbers[monkey.Value[2]];
                            break;
                        case "/":
                            res = numbers[monkey.Value[0]] / numbers[monkey.Value[2]];
                            break;
                    }
                    if (monkey.Key == "root") {
                        return res;
                    }
                    numbers[monkey.Key] = res;
                    monkeys.Remove(monkey.Key);
                }
            }
        }
    }

    public static long Part2(string[] lines)
    {
        Dictionary<string,long> numbers = new Dictionary<string, long>();
        Dictionary<string, string[]> monkeys = new Dictionary<string, string[]>();
        Dictionary<string, string[]> opMonkeys = new Dictionary<string, string[]>();
        foreach (string line in lines) {
            string[] parts = line.Split(": ");
            if (parts[0] == "humn") opMonkeys.Add("humn",new string[]{});
            else {
                long num=0;
                if (long.TryParse(parts[1],out num)) numbers[parts[0]] = num;
                else monkeys[parts[0]] = parts[1].Split(" ");
            }
        }
        while (true) {
            foreach (KeyValuePair<string, string[]> monkey in monkeys) {
                if (monkey.Key == "root") {
                    long target = 0;
                    if (numbers.ContainsKey(monkey.Value[0])) target = numbers[monkey.Value[0]];
                    else if (numbers.ContainsKey(monkey.Value[2])) target = numbers[monkey.Value[2]];
                    if (target != 0 && (opMonkeys.ContainsKey(monkey.Value[2]) || opMonkeys.ContainsKey(monkey.Value[0]))) {
                        foreach (KeyValuePair<string, string[]> om in opMonkeys.Reverse()) {
                            if (om.Key == "humn") return target;
                            if (numbers.ContainsKey(om.Value[0])) {
                                switch (om.Value[1]) {
                                    case "+":
                                        target -= numbers[om.Value[0]];
                                        break;
                                    case "-":
                                        target = numbers[om.Value[0]] - target;
                                        break;
                                    case "*":
                                        target = target / numbers[om.Value[0]];
                                        break;
                                    case "/":
                                        target = numbers[om.Value[0]] / target;
                                        break;
                                }
                            } 
                            else {
                                switch (om.Value[1]) {
                                    case "+":
                                        target -= numbers[om.Value[2]];
                                        break;
                                    case "-":
                                        target += numbers[om.Value[2]];
                                        break;
                                    case "*":
                                        target = target/numbers[om.Value[2]];
                                        break;
                                    case "/":
                                        target *= numbers[om.Value[2]];
                                        break;
                                }
                            }
                        }
                    }
                }
                else if (opMonkeys.ContainsKey(monkey.Value[0]) || opMonkeys.ContainsKey(monkey.Value[2])) {
                    opMonkeys.Add(monkey.Key, new string[]{monkey.Value[0],monkey.Value[1],monkey.Value[2]});
                    monkeys.Remove(monkey.Key);
                }
                else if (numbers.ContainsKey(monkey.Value[0]) && numbers.ContainsKey(monkey.Value[2])) {
                    long res=0;
                    switch (monkey.Value[1]) {
                        case "+":
                            res = numbers[monkey.Value[0]] + numbers[monkey.Value[2]];
                            break;
                        case "-":
                            res = numbers[monkey.Value[0]] - numbers[monkey.Value[2]];
                            break;
                        case "*":
                            res = numbers[monkey.Value[0]] * numbers[monkey.Value[2]];
                            break;
                        case "/":
                            res = numbers[monkey.Value[0]] / numbers[monkey.Value[2]];
                            break;
                    }
                    numbers[monkey.Key] = res;
                    monkeys.Remove(monkey.Key);
                }
            }
        }
    }
}