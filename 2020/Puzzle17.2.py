file = open("Day17Input.txt","r")
start = file.read()
file.close()

cubeCount = start.count("#")
start = start.split("\n")
for s in range(len(start)):
    start[s] = list(start[s])

initial = len(start)
cycles = 7
space = []
for w in range(-cycles,cycles+1):
    space.append([])
    for z in range(-cycles,cycles+1):
        space[-1].append([])
        for y in range(initial+(cycles*2)):
            space[-1][-1].append([])
            for x in range(initial+(cycles*2)):
                if w == 0 and z == 0 and (y >= cycles and y < cycles+initial) and (x >= cycles and x < cycles+initial):
                    space[-1][-1][-1].append(start[y-cycles][x-cycles])
                else:
                    space[-1][-1][-1].append(".")
#print(space)

for c in range(cycles-1):
    changes = []
    for w in range(cycles-c-1,cycles+c+2):
        for z in range(cycles-c-1,cycles+c+2):
            for y in range(cycles-c-1,cycles+initial+c+1):
                for x in range(cycles-c-1,cycles+initial+c+1):
                    nearbyActive = 0
                    for l in range(-1,2):
                        for k in range(-1,2):
                            for j in range(-1,2):
                                for i in range(-1,2):
                                    if not (l==0 and i==0 and j==0 and k==0):
                                        if space[w+l][z+k][y+j][x+i] == "#":
                                            nearbyActive += 1
                    if space[w][z][y][x] == ".":
                        if nearbyActive == 3:
                            changes.append((x,y,z,w))
                    else:
                        if nearbyActive < 2 or nearbyActive > 3:
                            changes.append((x,y,z,w))

    for i in changes:
        if space[i[3]][i[2]][i[1]][i[0]] == "#":
            space[i[3]][i[2]][i[1]][i[0]] = "."
            cubeCount -= 1
        else:
            space[i[3]][i[2]][i[1]][i[0]] = "#"
            cubeCount += 1

    '''for w in space[cycles-c-1:cycles+c+2]:
        for z in w[cycles-c-1:cycles+c+2]:
            for y in z[cycles-c-1:cycles+initial+c+1]:
                print(y[cycles-c-1:cycles+initial+c+1])
            print()
        print()
    print()'''
    

print(cubeCount)
