file = open("Inputs/input05.txt","r")
inputs = file.readlines()
file.close()

gap = inputs.index("\n")
rules_raw = inputs[:gap]
pages = inputs[gap+1:]

rules = {}
for r in rules_raw:
    x, y = int(r[:2]), int(r[3:5])
    if x not in rules.keys():
        rules[x] = set()
    rules[x].add(y)

sum_correct = 0
sum_fixed = 0
for p in pages:
    numbers = list(map(int, p.replace("\n","").split(",")))
    for i in range(len(numbers) -1, -1, -1):
        if len(rules.get(numbers[i], set()).intersection(numbers[:i])) > 0:
            break
    else:
        sum_correct += numbers[int(len(numbers)/2)]
        continue
    fixed = sorted(map(lambda n: [rules.get(n,set()).intersection(numbers), n], numbers))
    sum_fixed += fixed[int(len(numbers)/2)][1]
    
print(sum_correct)
print(sum_fixed)