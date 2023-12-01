file = open("Day11Input.txt","r")
rows = file.read().split("\n")
file.close()

for r in range(len(rows)):
    rows[r] = list(rows[r])

maxRow = len(rows)
maxCol = len(rows[0])

change = True
rounds = 0

while change:
    change = False
    new = []
    for row in rows:
        new.append(row[:])
    for r in range(maxRow):
        for c in range(maxCol):
            current = rows[r][c]
            if current != ".":
                occupied = 0
                for x in range(-1,2,2):
                    checkx = c+x
                    while 0 <= checkx < maxCol:
                        check = rows[r][checkx]
                        if check == ".":
                            checkx += x                            
                        else:
                            if check == "#":
                                occupied += 1
                            checkx = -1                            

                for y in range(-1,2,2):
                    checky = r+y
                    while 0 <= checky < maxRow:
                        check = rows[checky][c]
                        if check == ".":
                            checky += y
                        else:
                            if check == "#":
                                occupied += 1
                            checky = -1
                    
                for y in range(-1,2,2):
                    for x in range(-1,2,2):
                        checkx = c+x
                        checky = r+y
                        while 0 <= checkx < maxCol and 0 <= checky < maxRow:
                            check = rows[checky][checkx]
                            if check == ".":
                                checkx += x
                                checky += y
                            else:
                                if check == "#":
                                    occupied += 1
                                checkx = -1
                                
                        #print((checkx,checky))
                                
                if current == "L" and occupied == 0:
                    new[r][c] = "#"
                    change = True
                elif current == "#" and occupied > 4:
                    new[r][c] = "L"
                    change = True
    rows = new
    rounds += 1
    
count = 0
for r in rows:
    count += r.count("#")

print(rounds)
print(count)
