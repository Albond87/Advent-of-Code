file = open("Day14Input.txt","r")
lines = file.readlines()
file.close()
lines[-1] = lines[-1]+"\n"

polymer = lines[0][:-1]
rules = {}

chars = []
for p in polymer:
    if p not in chars:
        chars.append(p)
for l in lines[2:]:
    rule = l[:-1].split(" -> ")
    rules[rule[0]] = rule[1]
    if rule[0][0] not in chars:
        chars.append(rule[0][0])
    if rule[0][1] not in chars:
        chars.append(rule[0][1])
    if rule[1] not in chars:
        chars.append(rule[1])

steps = 10

for s in range(steps):
    last = polymer[0]
    new = last
    for p in range(1,len(polymer)):
        current = polymer[p]
        rule = rules.get(last+current,"")
        if (rule):
            new += rule
        new += current
        last = current
    polymer = new

print(len(polymer))
high = 0
low = len(polymer)
for c in chars:
    ct = polymer.count(c)
    if ct > high:
        high = ct
    if ct < low:
        low = ct

print(high)
print(low)
print(high-low)
