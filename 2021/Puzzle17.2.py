# Part 1 solved by hand

def triNum(x):
    return ((x**2)+x)/2

file = open("Day17Input.txt","r")
lines = file.readlines()
file.close()

area = lines[0][15:].split(", ")
xs = area[0].split("..")
minX = int(xs[0])
maxX = int(xs[1])
ys = area[1][2:].split("..")
minY = int(ys[0])
maxY = int(ys[1])

count = (maxX+1-minX)*(maxY+1-minY)
vels = []

xsteps = [[0 for _ in range(0)] for _ in range(maxX+1-minX)]
ysteps = [[0 for _ in range(0)] for _ in range(maxY+1-minY)]
  

for y in range(min(maxY+1,int((minY-1)/2)),abs(minY)):
    vel = y
    pos = y
    steps = 1
    while pos >= minY:
        vel -= 1
        pos += vel
        steps += 1
        if minY <= pos <= maxY:
            ysteps[pos-minY].append((steps,y))


x = int((maxX+1)/2)
while x > 0:
    vel = x
    pos = x
    steps = 1
    while pos <= maxX and vel > 1:
        vel -= 1
        pos += vel
        steps += 1
        if minX <= pos <= maxX:
            xsteps[pos-minX].append((steps,x))
            if vel == 1:
                for ys in ysteps:
                    for y in ys:
                        if y[0] >= steps:                            
                            if (x,y[1]) not in vels:
                                #print(str(x)+","+str(y[1]))
                                count += 1
                                vels.append((x,y[1]))
            else:
                for ys in ysteps:
                    for y in ys:
                        if y[0] == steps:
                            if (x,y[1]) not in vels:
                                #print(str(x)+","+str(y[1]))
                                count += 1
                                vels.append((x,y[1]))
    if pos < minX:
        x = 0
    else:
        x -= 1

print(count)
#print(xsteps)
#print(ysteps)
