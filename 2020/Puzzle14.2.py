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

def possibleOffsets(indexes):
    if len(indexes) == 0:
        return [0]
    r = []    
    for o in possibleOffsets(indexes[1:]):
        r.append(o)
        r.append(o + 2**(35-indexes[0]))
    return r
        

file = open("Day14Input.txt","r")
lines = file.read().split("\n")
file.close()

memLocs = []
memVals = []
mask = []
floats = []

for l in lines:
    if l[1] == "a": # setting mask
        newMask = l[7:]
        mask = []
        floats = []
        for m in range(len(newMask)):
            if newMask[m] == "1":
                mask.append((m,"1"))
            elif newMask[m] == "X":
                mask.append((m,"0"))
                floats.append(m)
        floats = possibleOffsets(floats)
    else:
        rawAdd = l.split("]")[0][4:]
        rawAdd = binary(int(rawAdd))
        val = int(l.split("=")[1][1:])

        newAdd = ""
        pos = 0
        for m in mask:
            newAdd += rawAdd[pos:m[0]]
            newAdd += m[1]
            pos = m[0] + 1
        newAdd += rawAdd[pos:]
        newAdd = decimal(newAdd)

        for f in floats:
            memAddress = newAdd + f
            if memAddress in memLocs:
                memVals[memLocs.index(memAddress)] = val
            else:
                memLocs.append(memAddress)
                memVals.append(val)

print(sum(memVals))
