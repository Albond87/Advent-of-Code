file = open("Day5Input.txt","r")
lines = file.readlines()
file.close()
lines[-1] = lines[-1] + "\n"

vents = {}
overlapCount = 0

for l in lines:
    coords = l.split(" -> ")
    coord1 = coords[0].split(",")
    x1 = int(coord1[0])
    y1 = int(coord1[1])
    coord2 = coords[1][:-1].split(",")
    x2 = int(coord2[0])
    y2 = int(coord2[1])
    if x1 == x2:
        for y in range(min(y1,y2),max(y1,y2)+1):
            key = x1+(y/1000)
            current = vents.get(key,0)
            if current == 1:
                overlapCount += 1
            vents[key] = current + 1
    elif y1 == y2:
        for x in range(min(x1,x2),max(x1,x2)+1):
            key = x+(y1/1000)
            current = vents.get(key,0)
            if current == 1:
                overlapCount += 1
            vents[key] = current + 1
    else:
        xstep = 1
        if x1 > x2:
            xstep = -1
        ystep = 1
        if y1 > y2:
            ystep = -1
        xs = range(x1,x2+xstep,xstep)
        ys = range(y1,y2+ystep,ystep)
        for i in range(len(xs)):
            key = xs[i]+(ys[i]/1000)
            current = vents.get(key,0)
            if current == 1:
                overlapCount += 1
            vents[key] = current + 1

print(overlapCount)
