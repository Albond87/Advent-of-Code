file = open("Day16Input.txt","r")
lines = file.read()
file.close()

lines = lines.split("\n\n")
fields = lines[0].split("\n")
myTicket = lines[1].split("\n")[1]
tickets = lines[2].split("\n")[1:]

fieldRanges = []
for f in range(len(fields)):
    ranges = fields[f].split(" or ")
    ranges[0] = ranges[0][ranges[0].index(":")+2:].split("-")
    ranges[0] = (int(ranges[0][0]),int(ranges[0][1]))
    ranges[1] = ranges[1].split("-")
    ranges[1] = (int(ranges[1][0]),int(ranges[1][1]))
    fieldRanges.append((ranges[0],ranges[1]))

totalInvalid = 0
for t in tickets:
    values = t.split(",")
    for v in values:
        v = int(v)
        valid = False
        for f in fieldRanges:
            if (v >= f[0][0] and v <= f[0][1]) or (v >= f[1][0] and v <= f[1][1]):
                valid = True
                break
        if not valid:
            totalInvalid += v

print(totalInvalid)
        
