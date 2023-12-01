def getChunkEnd(line,start):
    global scores, scoreTable, closes
    if start >= len(line)-1:
        # end of line
        return -3
    openb = line[start]
    while line[start+1] not in [')',']','}','>']:
        start = getChunkEnd(line,start+1)
        if start == -2:
            # propagate error
            return -2
        elif start == -3:
            # incomplete line
            scores[-1] = (scores[-1]*5)+scoreTable.get(openb)
            return -3
    if line[start+1] == closes.get(openb):
        return start+1
    else:
        # bracket mismatch
        return -2
    print("wut")
    

file = open("Day10Input.txt","r")
lines = file.readlines()
file.close()
lines[-1] = lines[-1]+"\n"

closes = { '(':')', '[':']', '{':'}', '<':'>' }
scoreTable = { '(':1, '[':2, '{':3, '<':4 }
scores = []

for l in lines:
    scores.append(0)
    c = 0
    while c >= 0:
        c = getChunkEnd(l,c)
        c += 1
    if scores[-1] == 0:
        scores.pop()

print(scores)
scores.sort()
print(scores[int(len(scores)/2)])
