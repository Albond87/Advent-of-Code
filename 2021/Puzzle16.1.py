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

def parse(packet):
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
        return (version,pos,packet[pos:])
    else:
        lengthTypeID = packet[6]
        if lengthTypeID == "0":
            totalLength = binToDec(packet[7:22])
            #print(totalLength)
            remaining = packet[22:]
            parsed = 0
            while (parsed != totalLength):
                parseResult = parse(remaining)
                version += parseResult[0]
                parsed += parseResult[1]
                remaining = parseResult[2]
            return (version,22+parsed,remaining)
        else:
            subPackets = binToDec(packet[7:18])
            #print(subPackets)
            remaining = packet[18:]
            parsed = 0
            for p in range(subPackets):
                parseResult = parse(remaining)
                version += parseResult[0]
                parsed += parseResult[1]
                remaining = parseResult[2]
            return (version,18+parsed,remaining)

file = open("Day16Input.txt","r")
lines = file.readlines()
file.close()

print(parse(hexToBin(lines[0])))
