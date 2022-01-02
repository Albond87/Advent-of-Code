file = open("Day5Input.txt","r")
passes = file.readlines()
file.close()

maxId = 0
for p in passes:
    row = 0
    for r in range(7):
        if p[r] == "B":
            row += 2**(6-r)
    column = 0
    for c in range(7,10):
        if p[c] == "R":
            column += 2**(9-c)
    seatId = (row*8) + column
    if seatId > maxId:
        maxId = seatId

print(maxId)
