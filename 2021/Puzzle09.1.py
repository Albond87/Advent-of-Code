file = open("Day9Input.txt","r")
lines = file.readlines()
file.close()
lines[-1] = lines[-1]+"\n"

heightmap = [[9]*(len(lines[0])+1)]
for l in lines:
    heightmap.append([9])
    for i in l[:-1]:
        heightmap[-1].append(int(i))
    heightmap[-1].append(9)

heightmap.append(heightmap[0])
#print(heightmap)

risk = 0
for y in range(1,len(heightmap)-1):
    for x in range(1,len(heightmap[0])-1):
        h = heightmap[y][x]
        if h < heightmap[y-1][x] and h < heightmap[y][x-1] and h < heightmap[y][x+1] and h < heightmap[y+1][x]:
            risk += h+1

print(risk)
