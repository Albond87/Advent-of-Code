def surroundGrid(val,grid):
    edge = [val]*(len(grid[0])+4)
    new = [edge,edge]
    for r in grid:
        new.append([val,val]+r[:]+[val,val])
    new.append(edge)
    new.append(edge)
    return new

file = open("Day20Input.txt","r")
lines = file.readlines()
file.close()
lines[-1] = lines[-1]+"\n"

algorithm = lines[0][:-1]
image = []
enhanced = []
for l in lines[2:]:
    enhanced.append([])
    for p in l[:-1]:
        if p == "#":
            enhanced[-1].append(1)
        else:
            enhanced[-1].append(0)
infinite = 0
enhances = 50
for e in range(enhances):
    image = surroundGrid(infinite,enhanced)
    enhanced = []
    for y in range(1,len(image)-1):
        enhanced.append([])
        for x in range(1,len(image[0])-1):
            index = 0
            power = 8
            for j in range(-1,2):
                for i in range(-1,2):
                    index += image[y+j][x+i] * (2**power)
                    power -= 1
            if algorithm[index] == "#":
                enhanced[-1].append(1)
            else:
                enhanced[-1].append(0)
    if algorithm[infinite*-1] == "#":
        infinite = 1
    else:
        infinite = 0

count = 0
for e in enhanced:
    count += e.count(1)
    '''for p in e:
        if p:
            print("#",end="")
            count += 1
        else:
            print(".",end="")
    print()'''
    

print(count)
