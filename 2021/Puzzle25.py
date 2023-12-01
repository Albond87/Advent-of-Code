file = open("Day25Input.txt","r")
lines = file.readlines()
file.close()
lines[-1] = lines[-1] + "\n"

width = len(lines[0])-1
height = len(lines)

cucumbers = []
#easts = []
#souths = []
for l in lines: #y in range(height):
    cucumbers.append([])
    for p in l[:-1]: #x in range(width):
        if p == ".":
            cucumbers[-1].append(0)
        elif p == ">":
            cucumbers[-1].append(1)
        else:
            cucumbers[-1].append(2)

width = len(cucumbers[0])
height = len(cucumbers)

steps = 0
moved = True
while moved:
    steps += 1
    moved = False

    for y in range(height):
        movingPos = 0
        nowEmpty = []
        for x in range(width):
            if cucumbers[y][x] == 0:
                if movingPos:
                    cucumbers[y][x] = 1
                    nowEmpty.append(movingPos-1)
                    movingPos = 0
                    moved = True
            elif cucumbers[y][x] == 1:
                movingPos = x+1
            else:
                movingPos = 0
        if movingPos and cucumbers[y][0] == 0:
            cucumbers[y][0] = 1
            nowEmpty.append(movingPos-1)
        for e in nowEmpty:
            cucumbers[y][e] = 0

    for x in range(width):
        movingPos = 0
        nowEmpty = []
        for y in range(height):
            if cucumbers[y][x] == 0:
                if movingPos:
                    cucumbers[y][x] = 2
                    nowEmpty.append(movingPos-1)
                    movingPos = 0
                    moved = True
            elif cucumbers[y][x] == 2:
                movingPos = y+1
            else:
                movingPos = 0
        if movingPos and cucumbers[0][x] == 0:
            cucumbers[0][x] = 2
            nowEmpty.append(movingPos-1)
        for e in nowEmpty:
            cucumbers[e][x] = 0

print(steps)
