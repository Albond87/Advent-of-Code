file = open("Day24Input.txt","r")
flips = file.read().split("\n")
file.close()

flipped = {}
count = 0

xRange = [0,0]
yRange = [0,0]

for f in flips:
    pos = [0,0]
    i = 0
    while i < len(f):
        move = f[i]
        if move == "n":
            pos[1] += 1
            i += 1
            move = f[i]
            if move == "w":
                pos[0] -= 1
        elif move == "s":
            pos[1] -= 1
            i += 1
            move = f[i]
            if move == "e":
                pos[0] += 1
        elif move == "e":
            pos[0] += 1
        else:
            pos[0] -= 1
        i += 1
    pos = tuple(pos)
    if flipped.get(pos):
        flipped.pop(pos)
        count -= 1
    else:
        flipped[pos] = 1
        count += 1
        if pos[0] < xRange[0]:
            xRange[0] = pos[0]
        elif pos[0] > xRange[1]:
            xRange[1] = pos[0]
        if pos[1] < yRange[0]:
            yRange[0] = pos[1]
        elif pos[1] > yRange[1]:
            yRange[1] = pos[1]

days = 100

minX = xRange[0]
minY = yRange[0]
xRange = (xRange[1] - xRange[0])+1
yRange = (yRange[1] - yRange[0])+1

flipped = list(flipped.keys())
floor = []
for y in range(yRange+((days+1)*2)):
    floor.append([0]*(xRange+((days+1)*2)))

offsetX = (days+1)-minX
offsetY = (days+1)-minY

for f in flipped:
    floor[f[1]+offsetY][f[0]+offsetX] = 1

border = days

for d in range(days):
    changes = []
    for y in range(border,len(floor)-border):
        for x in range(border,len(floor[0])-border):
            blackCount = 0
            if floor[y+1][x-1] == 1: # north west
                blackCount += 1
            if floor[y+1][x] == 1:   # north east
                blackCount += 1
            if floor[y][x-1] == 1:   # west
                blackCount += 1
            if floor[y][x+1] == 1:   # east
                blackCount += 1
            if floor[y-1][x] == 1:   # south west
                blackCount += 1
            if floor[y-1][x+1] == 1: # south east
                blackCount += 1

            if floor[y][x] == 0:
                if blackCount == 2:
                    changes.append((x,y))
                    count += 1
            else:
                if blackCount == 0 or blackCount > 2:
                    changes.append((x,y))
                    count -= 1

    for c in changes:
        floor[c[1]][c[0]] = 1-floor[c[1]][c[0]]
    border -= 1

print(count)
