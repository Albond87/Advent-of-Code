def expandPair(first,second,step,steps):
    if step>steps:
        return ""
    global rules
    global chars

    #print(step)
    #print(first+second)
    rule = rules.get(first+second,"")
    #print(rule)
    if (rule):
        expandPair(first,rule,step+1)
        expandPair(rule,second,step+1)
        chars[rule] += 1

def countChars(polymer,times):
    global chars, rulesExp, ruleCounts
    if times == 1:
        for p in range(1,len(polymer)):
            rule = polymer[p-1]+polymer[p]
            for c in chars:
                chars[c] += ruleCounts[rule][c]
            if (p != len(polymer)-1):
                chars[polymer[p]] -= 1
        return

    for p in range(1,len(polymer)):
        countChars(rulesExp.get(polymer[p-1]+polymer[p]),times-1)
        if (p != len(polymer)-1):
            chars[polymer[p]] -= 1

file = open("Day14Input.txt","r")
lines = file.readlines()
file.close()
lines[-1] = lines[-1]+"\n"

polymer = lines[0][:-1]
rules = {}

chars = {}
for p in polymer:
    if chars.get(p) == None:
        chars[p] = 0
for l in lines[2:]:
    rule = l[:-1].split(" -> ")
    rules[rule[0]] = rule[1]
    if chars.get(rule[0][0]) == None:
        chars[rule[0][0]] = 0
    if chars.get(rule[0][1]) == None:
        chars[rule[0][1]] = 0
    if chars.get(rule[1]) == None:
        chars[rule[1]] = 0


print(chars)

#print(rules)
rulesExp = {}
for rl in rules:
    r = rl
    for s in range(20):
        last = r[0]
        new = last
        for p in range(1,len(r)):
            current = r[p]
            rule = rules.get(last+current,"")
            if (rule):
                new += rule
            new += current
            last = current
        r = new
    rulesExp[rl] = r

ruleCounts = {}
for r in rules:
    ruleCounts[r] = {}
    for c in chars:
        ruleCounts[r][c] = rulesExp[r].count(c)

#print(rulesExp)
countChars(polymer,2)
print(chars)
