def hexToBin(h):
    conversion = { "0" : "0000",
                   "1" : "0001",
                   "2" : "0010",
                   "3" : "0011",
                   "4" : "0100",
                   "5" : "0101",
                   "6" : "0110",
                   "7" : "0111",
                   "8" : "1000",
                   "9" : "1001",
                   "A" : "1010",
                   "B" : "1011",
                   "C" : "1100",
                   "D" : "1101",
                   "E" : "1110",
                   "F" : "1111" }
    b = ""
    for i in h:
        b += conversion.get(i)
    return b

def binToDec(b):
    d = 0
    for i in range(len(b)):
        d += (2**(len(b)-i-1))*int(b[i])
    return d

def product(xs):
    prod = 1
    for x in xs:
        prod *= x
    return prod

def isGreater(xs):
    if xs[0] > xs[1]:
        return 1
    else:
        return 0

def isLess(xs):
    if xs[0] < xs[1]:
        return 1
    else:
        return 0

def isEqual(xs):
    if xs[0] == xs[1]:
        return 1
    else:
        return 0

def parse(packet):
    global operations
    #print(packet)
    version = binToDec(packet[:3])
    typeID = binToDec(packet[3:6])

    if typeID == 4:
        literal = ""
        pos = 6
        prefix = "1"
        while prefix == "1":
            prefix = packet[pos]
            literal += packet[pos+1:pos+5]
            pos += 5
        value = binToDec(literal)
        return (value,pos,packet[pos:])
    else:
        lengthTypeID = packet[6]
        values = []
        if lengthTypeID == "0":
            totalLength = binToDec(packet[7:22])
            #print(totalLength)
            remaining = packet[22:]
            parsed = 0
            while (parsed != totalLength):
                parseResult = parse(remaining)
                values.append(parseResult[0])
                parsed += parseResult[1]
                remaining = parseResult[2]
            parsed += 22
        else:
            subPackets = binToDec(packet[7:18])
            #print(subPackets)
            remaining = packet[18:]
            parsed = 18
            for p in range(subPackets):
                parseResult = parse(remaining)
                values.append(parseResult[0])
                parsed += parseResult[1]
                remaining = parseResult[2]
        value = operations[typeID](values)
        return (value,parsed,remaining)

file = open("Day16Input.txt","r")
lines = file.readlines()
file.close()

operations = [sum,product,min,max,sum,isGreater,isLess,isEqual]
print(parse(hexToBin(lines[0])))
