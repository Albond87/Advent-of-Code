file = open("Day9Input.txt","r")
lines = file.readlines()
file.close()
lines[-1] = lines[-1]+"\n"

hmap = [[9]*(len(lines[0])+1)]
for l in lines:
    hmap.append([9])
    for i in l[:-1]:
        hmap[-1].append(int(i))
    hmap[-1].append(9)

hmap.append(hmap[0])

basins = []
for y in range(1,len(hmap)-1):
    for x in range(1,len(hmap[0])-1):
        h = hmap[y][x]
        if h < hmap[y-1][x] and h < hmap[y][x-1] and h < hmap[y][x+1] and h < hmap[y+1][x]:
            #size = 0
            boundaryLength = 0
            points = [(x,y)]
            toCheck = [(x,y)]
            while len(toCheck) > 0:
                nextCheck = []
                for p in toCheck:
                    coords = [(p[0],p[1]-1),(p[0]-1,p[1]),(p[0]+1,p[1]),(p[0],p[1]+1)]
                    for c in coords:
                        if c not in points:
                            if hmap[c[1]][c[0]] == 9:
                                points.append(c)
                                boundaryLength += 1
                            else:
                                points.append(c)
                                nextCheck.append(c)
                toCheck = nextCheck
            basins.append(len(points)-boundaryLength)

basins.sort(reverse=True)
print(basins)
print(basins[0]*basins[1]*basins[2])
