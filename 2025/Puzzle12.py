import numpy as np

def fitShapes(grid, gx, gy, shapeCounts):
    global shapes

    if sum(shapeCounts) == 0: 
        print(grid)
        return True
    
    si = 0
    while shapeCounts[si] == 0:
        si += 1
    svs = shapes[si]
    newShapeCounts = shapeCounts[:]
    newShapeCounts[si] -= 1
    for s in svs:
        sy, sx = np.shape(s)
        for y in range(gy-sy+1):
            for x in range(gx-sx+1):
                if not (grid[y:y+sy, x:x+sx] & s).any():
                    # Shape fits with no overlap, so recursively place next shape
                    newGrid = np.array(grid)
                    newGrid[y:y+sy, x:x+sx] |= s
                    if fitShapes(newGrid, gx, gy, newShapeCounts):
                        return True
    
    return False

if (__name__ == "__main__"):
    file = open("Inputs/input12.txt", "r")
    inputs = file.readlines()
    file.close()
    inputs = [i.replace("\n","") for i in inputs]

    shapes = []
    shapeSizes = []
    i = 0
    while inputs[i][-1] == ':':
        j = i+1
        while inputs[j] != "":
            j += 1
        
        shape = np.array([[x=='#' for x in list(y)] for y in inputs[i+1:j]])
        shapeSizes.append("".join(inputs[i+1:j]).count('#'))
        shapeVariations = [np.array(shape)]

        # Find all unique configurations of the shape by rotating and flipping
        for r in range(3):
            shape = np.rot90(shape)
            for s in shapeVariations:
                if (s==shape).all():
                    break
            else:
                shapeVariations.append(np.array(shape))
        
        shape = np.flip(shape, 0)
        for r in range(4):
            shape = np.rot90(shape)
            for s in shapeVariations:
                if (s==shape).all():
                    break
            else:
                shapeVariations.append(np.array(shape))    

        shapes.append(shapeVariations)
        i = j+1

    count = 0
    for line in inputs[i:]:
        dimensions, shapeCounts = line.split(": ")
        x, y = [int(n) for n in dimensions.split("x")]
        shapeCounts = [int(s) for s in shapeCounts.split()]

        # Check easy case - all pieces fit in a grid of 3x3 regions with no intersecting
        if (x//3) * (y//3) >= sum(shapeCounts):
            count += 1
            continue

        # Check easy case - the listed shapes have more units than the area of the region
        if sum([shapeCounts[si] * shapeSizes[si] for si in range(6)]) > x*y:
            continue
        
        # Otherwise try to fit the shapes - all of the given inputs are easy cases though
        grid = np.full((y,x), False, dtype=bool)
        if fitShapes(grid, x, y, shapeCounts):
            count += 1
    
    print(count)