def binary(decimal):
    b = ""
    for i in range(35,-1,-1):
        if 2**i <= decimal:
            b += "1"
            decimal -= 2**i
        else:
            b += "0"
    return b

def decimal(binary):
    d = 0
    for i in range(0,36):
        if binary[i] == "1":
            d += 2**(35-i)
    return d

file = open("Day14Input.txt","r")
lines = file.read().split("\n")
file.close()

memLocs = []
memVals = []
mask = []

for l in lines:
    if l[1] == "a": # setting mask
        newMask = l[7:]
        mask = []
        for m in range(len(newMask)):
            if newMask[m] != "X":
                mask.append((m,newMask[m]))
    else:
        memAddress = int(l.split("]")[0][4:])
        rawVal = l.split("=")[1][1:]
        rawVal = binary(int(rawVal))


        newVal = ""
        pos = 0
        for m in mask:
            newVal += rawVal[pos:m[0]]
            newVal += m[1]
            pos = m[0] + 1
        newVal += rawVal[pos:]
        newVal = decimal(newVal)

        if memAddress in memLocs:
            memVals[memLocs.index(memAddress)] = newVal
        else:
            memLocs.append(memAddress)
            memVals.append(newVal)

print(sum(memVals))
