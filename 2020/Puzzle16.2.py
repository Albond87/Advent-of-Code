def removePossibilities(val,fields):
    for f in range(len(fields)):
        if len(fields[f]) != 1:
            try:
                fields[f].remove(val)
                if len(fields[f]) == 1:
                    removePossibilities(fields[f][0],fields)
            except:
                print("wut")
                pass
    return fields

file = open("Day16Input.txt","r")
lines = file.read()
file.close()

lines = lines.split("\n\n")
fields = lines[0].split("\n")
myTicket = lines[1].split("\n")[1]
tickets = lines[2].split("\n")[1:]
tickets.append(myTicket)

fieldRanges = []
for f in range(len(fields)):
    ranges = fields[f].split(" or ")
    ranges[0] = ranges[0][ranges[0].index(":")+2:].split("-")
    ranges[0] = (int(ranges[0][0]),int(ranges[0][1]))
    ranges[1] = ranges[1].split("-")
    ranges[1] = (int(ranges[1][0]),int(ranges[1][1]))
    fieldRanges.append((ranges[0],ranges[1]))

validTickets = []
for t in tickets:
    values = t.split(",")
    for v in range(len(values)):
        values[v] = int(values[v])
        v = values[v]
        valid = False
        for f in fieldRanges:
            if (v >= f[0][0] and v <= f[0][1]) or (v >= f[1][0] and v <= f[1][1]):
                valid = True
                break
        if not valid:
            break
    else:
        validTickets.append(values)

possibleFields = []
fields = range(len(fields))
for f in fields:
    possibleFields.append(list(fields))

for t in range(len(validTickets)):
    for v in range(len(validTickets[t])):
        if len(possibleFields[v]) > 1:
            c = v
            v = validTickets[t][v]
            fit = possibleFields[c][:]
            for f in fit:
                r1 = fieldRanges[f][0]
                r2 = fieldRanges[f][1]
                if (v < r1[0] or v > r1[1]) and (v < r2[0] or v > r2[1]):
                    possibleFields[c].remove(f)
            if len(possibleFields[c]) == 1:
                possibleFields = removePossibilities(possibleFields[c][0],possibleFields)
                '''remove = possibleFields[c][0]
                for p in range(len(possibleFields)):
                    if p != c:
                        try:
                            possibleFields[p].remove(remove)
                        except:
                            pass'''

print(possibleFields)
product = 1
for p in range(len(possibleFields)):
    if possibleFields[p][0] < 6:
        product *= validTickets[-1][p]

print(product)
