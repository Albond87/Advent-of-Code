def getChunkEnd(line,start):
    global score, scores, closes
    if start >= len(line)-2:
        # incomplete line
        return -2
    openb = line[start]
    while line[start+1] in ['(','[','{','<']:
        start = getChunkEnd(line,start+1)
        if start == -2:
            # propagate error
            return -2
        elif start >= len(line)-2:
            # incomplete line
            return -2
    if line[start+1] == closes.get(openb):
        return start+1
    else:
        # bracket mismatch
        score += scores.get(line[start+1])
        return -2
    print("wut")
    

file = open("Day10Input.txt","r")
lines = file.readlines()
file.close()
lines[-1] = lines[-1]+"\n"

closes = { '(':')', '[':']', '{':'}', '<':'>' }
scores = { ')':3, ']':57, '}':1197, '>':25137 }
score = 0

for l in lines:
    c = 0
    while c >= 0:
        c = getChunkEnd(l,c)
        c += 1

print(score)
