def getBorder(tile,side):
    if side%2 == 0:
        return tile[int(side*4.5)]
    else:
        if side == 1:
            index = 9
        else:
            index = 0
        border = ""
        for r in tile:
            border += r[index]
        return border

def flip(string):
    flipped = ""
    for s in range(len(string)-1,-1,-1):
        flipped += string[s]
    return flipped

def flipVertical(tile):
    tile.reverse()

def flipHorizontal(tile):
    flipped = []
    for r in tile:
        flipped.append(flip(r))
    return flipped

def rotateClockwise(tile):
    rotated = []
    for x in range(len(tile)):
        rotated.append("")
        for y in range(len(tile)-1,-1,-1):
            rotated[-1] += tile[y][x]
    return rotated

def rotateAntiClockwise(tile):
    rotated = []
    for x in range(len(tile)-1,-1,-1):
        rotated.append("")
        for y in range(len(tile)):
            rotated[-1] += tile[y][x]
    return rotated

def rotate180(tile):
    tile = flipHorizontal(tile)
    flipVertical(tile)
    return tile

def outputGrid(grid):
    for r in grid:
        for n in r:
            if n == 0:
                print("----",end = " ")
            else:
                print(n,end = " ")
        print()

file = open("Day20Input.txt","r")
tiles = file.read().split("\n\n")
file.close()

tileIds = []
for t in range(len(tiles)):
    tileInfo = tiles[t].split("\n")
    tileIds.append(int(tileInfo[0][5:-1]))
    tiles[t] = tileInfo[1:]

borders = []
for t in tiles:
    borders.append([])
    for b in range(0,4):
        borders[-1].append(getBorder(t,b))

start = 0

locked = [start]
free = list(range(len(tileIds)))
free.remove(locked[0])
#print(borders)

imageSize = int(len(tileIds)**0.5)
maxGrid = int((imageSize*2)-1)
grid = []
for g in range(maxGrid):
    grid.append([0]*maxGrid)

coords = [0]*len(tileIds)
center = imageSize-1
coords[start] = (center,center)
grid[center][center] = tileIds[start]

top = center
bottom = center+1
left = center
right = center+1

t = 0

while len(locked) < len(tileIds):
    tile = tiles[locked[t]]
    pos = coords[locked[t]]

    if bottom-top < imageSize or pos[1] != top:
        if grid[pos[1]-1][pos[0]] == 0:
            match = borders[locked[t]][0]
            matchFound = False
            for f in free:
                matchFound = True
                if borders[f][0] == match:
                    flipVertical(tiles[f])
                elif flip(borders[f][0]) == match:
                    tiles[f] = rotate180(tiles[f])
                elif borders[f][1] == match:
                    flipVertical(tiles[f])
                    tiles[f] = rotateClockwise(tiles[f])
                elif flip(borders[f][1]) == match:
                    tiles[f] = rotateClockwise(tiles[f])
                elif borders[f][2] == match:
                    pass
                elif flip(borders[f][2]) == match:
                    tiles[f] = flipHorizontal(tiles[f])
                elif borders[f][3] == match:
                    tiles[f] = rotateAntiClockwise(tiles[f])
                elif flip(borders[f][3]) == match:
                    flipVertical(tiles[f])
                    tiles[f] = rotateAntiClockwise(tiles[f])
                else:
                    matchFound = False

                if matchFound:
                    for b in range(0,4):
                        borders[f][b] = getBorder(tiles[f],b)
                    
                    grid[pos[1]-1][pos[0]] = tileIds[f]
                    coords[f] = (pos[0],pos[1]-1)
                    if pos[1] == top:
                        top -= 1
                    free.remove(f)
                    locked.append(f)
                    break
                    
            else:
                top = pos[1]
                bottom = top + imageSize
            
                
    if bottom-top < imageSize or pos[1]+1 != bottom:
        if grid[pos[1]+1][pos[0]] == 0:
            match = borders[locked[t]][2]
            matchFound = False
            for f in free:
                matchFound = True
                if borders[f][0] == match:
                    pass
                elif flip(borders[f][0]) == match:
                    tiles[f] = flipHorizontal(tiles[f])
                elif borders[f][1] == match:
                    tiles[f] = rotateAntiClockwise(tiles[f])
                elif flip(borders[f][1]) == match:
                    flipVertical(tiles[f])
                    tiles[f] = rotateAntiClockwise(tiles[f])
                elif borders[f][2] == match:
                    flipVertical(tiles[f])
                elif flip(borders[f][2]) == match:
                    tiles[f] = rotate180(tiles[f])
                elif borders[f][3] == match:
                    flipVertical(tiles[f])
                    tiles[f] = rotateClockwise(tiles[f])
                elif flip(borders[f][3]) == match:
                    tiles[f] = rotateClockwise(tiles[f])
                else:
                    matchFound = False

                if matchFound:
                    for b in range(0,4):
                        borders[f][b] = getBorder(tiles[f],b)
                        
                    grid[pos[1]+1][pos[0]] = tileIds[f]
                    coords[f] = (pos[0],pos[1]+1)
                    if pos[1]+1 == bottom:
                        bottom += 1
                    free.remove(f)
                    locked.append(f)
                    break
            
            else:
                bottom = pos[1]+1
                top = bottom - imageSize
                
    if right-left < imageSize or pos[0] != left:
        if grid[pos[1]][pos[0]-1] == 0:
            match = borders[locked[t]][3]
            matchFound = False
            for f in free:
                matchFound = True
                if borders[f][0] == match:
                    tiles[f] = rotateClockwise(tiles[f])
                elif flip(borders[f][0]) == match:
                    tiles[f] = rotateClockwise(tiles[f])
                    flipVertical(tiles[f])
                elif borders[f][1] == match:
                    pass
                elif flip(borders[f][1]) == match:
                    flipVertical(tiles[f])
                elif borders[f][2] == match:
                    tiles[f] = rotateAntiClockwise(tiles[f])
                    flipVertical(tiles[f])
                elif flip(borders[f][2]) == match:
                    tiles[f] = rotateAntiClockwise(tiles[f])
                elif borders[f][3] == match:
                    tiles[f] = flipHorizontal(tiles[f])
                elif flip(borders[f][3]) == match:
                    tiles[f] = rotate180(tiles[f])
                else:
                    matchFound = False

                if matchFound:
                    for b in range(0,4):
                        borders[f][b] = getBorder(tiles[f],b)
                        
                    grid[pos[1]][pos[0]-1] = tileIds[f]
                    coords[f] = (pos[0]-1,pos[1])
                    if pos[0] == left:
                        left -= 1
                    free.remove(f)
                    locked.append(f)
                    break
            
            else:
                left = pos[0]
                right = left + imageSize

    if right-left < imageSize or pos[0]+1 != right:
        if grid[pos[1]][pos[0]+1] == 0:
            match = borders[locked[t]][1]
            matchFound = False
            for f in free:
                matchFound = True
                if borders[f][0] == match:
                    tiles[f] = rotateAntiClockwise(tiles[f])
                    flipVertical(tiles[f])
                elif flip(borders[f][0]) == match:
                    tiles[f] = rotateAntiClockwise(tiles[f])
                elif borders[f][1] == match:
                    tiles[f] = flipHorizontal(tiles[f])
                elif flip(borders[f][1]) == match:
                    tiles[f] = rotate180(tiles[f])
                elif borders[f][2] == match:
                    tiles[f] = rotateClockwise(tiles[f])
                elif flip(borders[f][2]) == match:
                    tiles[f] = rotateClockwise(tiles[f])
                    flipVertical(tiles[f])
                elif borders[f][3] == match:
                    pass
                elif flip(borders[f][3]) == match:
                    flipVertical(tiles[f])
                else:
                    matchFound = False

                if matchFound:
                    for b in range(0,4):
                        borders[f][b] = getBorder(tiles[f],b)
                    
                    grid[pos[1]][pos[0]+1] = tileIds[f]
                    coords[f] = (pos[0]+1,pos[1])
                    if pos[0]+1 == right:
                        right += 1
                    free.remove(f)
                    locked.append(f)
                    break
            
            else:
                right = pos[0]+1
                left = right - imageSize

    t += 1

outputGrid(grid)

product = grid[top][left]*grid[top][right-1]*grid[bottom-1][left]*grid[bottom-1][right-1]
print(product)

image = []
for y in range(top,bottom):
    for r in range(1,9):
        image.append("")
        for x in range(left,right):
            image[-1] += tiles[tileIds.index(grid[y][x])][r][1:9]

seaMonster = ["                  # ",
              "#    ##    ##    ###",
              " #  #  #  #  #  #   "]

monsterParts = []
for y in range(len(seaMonster)):
    for x in range(len(seaMonster[0])):
        if seaMonster[y][x] == "#":
            monsterParts.append((x,y))

transformCount = 1
monsters = 0
while monsters == 0:
    for y in range(len(image)-(len(seaMonster)-1)):
        for x in range(len(image)-(len(seaMonster[0])-1)):
            for m in monsterParts:
                if image[y+m[1]][x+m[0]] != "#":
                    break
            else:
                monsters += 1
    if monsters == 0:
        if transformCount == 4:
            flipVertical(image)
        else:
            image = rotateClockwise(image)
        transformCount += 1

hashCount = 0
for r in image:
    print(r)
    hashCount += r.count("#")
    
print(monsters)
print(hashCount - (monsters*len(monsterParts)))
