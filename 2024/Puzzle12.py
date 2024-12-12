def findregion(x,y,c):
    global region, walls
    if (x,y) in region:
        return 0
    else:
        region.add((x,y))
    # Check if adjacent elements are the right character
    # If they are, continue traversal that way
    # If not, add a wall in that direction
    perimeter = 0
    if y > 1 and plots[y-2][x] == c:
        perimeter += findregion(x,y-2,c)
    else:
        perimeter += 1
        walls.add((x,y-1))
        walls.add((x-1,y-1))
        walls.add((x+1,y-1))
    if y < height-2 and plots[y+2][x] == c:
        perimeter += findregion(x,y+2,c)
    else:
        perimeter += 1
        walls.add((x,y+1))
        walls.add((x-1,y+1))
        walls.add((x+1,y+1))
    if x > 1 and plots[y][x-2] == c:
        perimeter += findregion(x-2,y,c)
    else:
        perimeter += 1
        walls.add((x-1,y))
        walls.add((x-1,y-1))
        walls.add((x-1,y+1))
    if x < width-2 and plots[y][x+2] == c:
        perimeter += findregion(x+2,y,c)
    else:
        perimeter += 1
        walls.add((x+1,y))
        walls.add((x+1,y-1))
        walls.add((x+1,y+1))
    return perimeter
    
file = open("Inputs/input12.txt","r")
inputs = file.readlines()
file.close()

plots = list(map(lambda p: list(p.replace("\n","")), inputs))
height = len(plots)*2+1
width = len(plots[0])*2+1
# Space out input elements so there is an empty row and column between each one
for j in range(height):
    if j % 2 == 0:
        plots.insert(j,[' ']*width)
    else:
        for i in range(0,width,2):
            plots[j].insert(i,' ')

inregion = set()
total1 = 0
total2 = 0

# Traverse from each element to find regions, starting in the top left
# Don't traverse any element that is already part of a region
for y in range(1,height,2):
    for x in range(1,width,2):
        if (x,y) in inregion:
            continue
        region = set()
        walls = set()
        perimeter = findregion(x,y,plots[y][x])
        
        # Count the walls by finding the number of wall corners
        wallcount = 0
        for (wx, wy) in walls:
            adjacent = []
            if (wx, wy-1) in walls:
                adjacent.append(0)
            if (wx, wy+1) in walls:
                adjacent.append(0)
            if (wx-1, wy) in walls:
                adjacent.append(1)
            if (wx+1, wy) in walls:
                adjacent.append(1)
            # 2 adjacent wall elements at a right angle make a corner
            if len(adjacent) == 2 and sum(adjacent) == 1:
                wallcount += 1
            # 4 adjacent wall elements make 2 corners
            elif len(adjacent) == 4:
                wallcount += 2
        total1 += len(region) * perimeter
        total2 += len(region) * wallcount
        for (rx, ry) in region:
            inregion.add((rx, ry))
    
print(total1)
print(total2)