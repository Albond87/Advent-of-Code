import re
    
def generatePattern(rules,rule):
    if len(rule) == 2:
        return "("+generatePattern(rules,rule[:1])+"|"+generatePattern(rules,rule[1:])+")"
    else:
        rule = rule[0]
        if type(rule[0]) == str:
            return rule[0]
        pattern = ""
        for r in rule:
            pattern += generatePattern(rules,rules[r])
        return pattern
            

file = open("Day19Input.txt","r")
parts = file.read().split("\n\n")
file.close()

rawRules = parts[0].split("\n")
rules = []
for r in rawRules:
    r = r.split(": ")
    ruleNo = int(r[0])
    r = r[1].split(" | ")
    for i in range(len(r)):
        r[i] = r[i].split(" ")
        for j in range(len(r[i])):
            if r[i][j][0] != "\"":
                r[i][j] = int(r[i][j])
            else:
                r[i][j] = r[i][j][1]
        r[i] = tuple(r[i])
    r = tuple(r)
    while ruleNo >= len(rules):
        rules.append("")
    rules[ruleNo] = r

rules = tuple(rules)
#print(rules)

pattern = "^"+generatePattern(rules,rules[0])+"$"
#print(pattern)
    
messages = parts[1].split("\n")
#print(messages)


validCount = 0
for m in messages:
    if re.search(pattern,m) != None:
        validCount += 1

print(validCount)
