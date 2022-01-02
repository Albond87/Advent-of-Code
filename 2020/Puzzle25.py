file = open("Day25Input.txt","r")
keys = file.read().split("\n")
file.close()

subjectNum = 7
loopSize = []

for k in keys:
    k = int(k)
    value = 1
    loop = 0
    while value != k:
        value *= subjectNum
        value = value % 20201227
        loop += 1
    loopSize.append(loop)

value = 1
subjectNum = int(keys[1])

for l in range(loopSize[0]):
    value *= subjectNum
    value = value % 20201227
    
print(value)
