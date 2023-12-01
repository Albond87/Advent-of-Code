def overlapCuboids(existing,new):
    # ((x1,x2),(y1,y2),(z1,z2))
    ((ex1,ex2),(ey1,ey2),(ez1,ez2)) = existing
    ((nx1,nx2),(ny1,ny2),(nz1,nz2)) = new

    '''intersecting = False
    for x in (ex1,ex2):
        for y in (ey1,ey2):
            for z in (ez1,ez2):
                if (nx1 <= x <= nx2) and (ny1 <= y <= ny2) and (nz1 <= z <= nz2):
                    intersecting = True
                    break
    if not intersecting:
        for x in (nx1,nx2):
            for y in (ny1,ny2):
                for z in (nz1,nz2):
                    if (ex1 <= x <= ex2) and (ey1 <= y <= ey2) and (ez1 <= z <= ez2):
                        intersecting = True
                        break
        if not intersecting:
            return [existing]'''

    xLeft = (ex1,min(ex2,nx1-1))
    xMiddle = (max(ex1,nx1),min(ex2,nx2))
    xRight = (max(ex1,nx2+1),ex2)
    yBelow = (ey1,min(ey2,ny1-1))
    yMiddle = (max(ey1,ny1),min(ey2,ny2))
    yAbove = (max(ey1,ny2+1),ey2)
    zFront = (ez1,min(ez2,nz1-1))
    zMiddle = (max(ez1,nz1),min(ez2,nz2))
    zBehind = (max(ez1,nz2+1),ez2)
    excessRegions = [ ((xLeft[0],xRight[1]),yBelow,(zFront[0],zBehind[1])),
                      (xLeft,yMiddle,zMiddle), (xRight,yMiddle,zMiddle),
                      ((xLeft[0],xRight[1]),yMiddle,zFront),
                      ((xLeft[0],xRight[1]),yMiddle,zBehind),
                      ((xLeft[0],xRight[1]),yAbove,(zFront[0],zBehind[1])) ]
    for e in range(5,-1,-1):
        for a in range(3):
            if excessRegions[e][a][1] < excessRegions[e][a][0]:
                excessRegions.pop(e)
                break

    return excessRegions

    #for y in (yBelow,yMiddle,yAbove):
    #    for z in (zFront,zMiddle,zBehind):
    #        for x in (xLeft,xMiddle,xRight):
    #            if not (y==yMiddle and z==zMiddle and x==xMiddle):
    #                excessRegions.append((x,y,z))

    #excessRegions = [ (xLeft,yBelow,zFront),(xMiddle,yBelow,zFront),(xRight,yBelow,zFront),
    #                  (xLeft,yBelow,zMiddle),(xMiddle,yBelow,zMiddle),(xRight,yBelow,zMiddle),
    #                  (xLeft,yBelow,zBehind),(xMiddle,yBelow,zBehind),(xRight,yBelow,zBehind)
    
                      

file = open("Day22Input.txt","r")
lines = file.readlines()
 

file.close()
lines[-1] = lines[-1]+"\n"

onRegions = []

for l in lines:
    newRegions = []
    on = l[1] == 'n'
    axes = l.split(",")
    xs = axes[0].split("=")[1].split("..")
    x1 = int(xs[0])
    x2 = int(xs[1])
    ys = axes[1][2:].split("..")
    y1 = int(ys[0])
    y2 = int(ys[1])
    zs = axes[2][2:-1].split("..")
    z1 = int(zs[0])
    z2 = int(zs[1])
    region = ((x1,x2),(y1,y2),(z1,z2))

    if on:
        newRegions.append(region)
    for r in range(len(onRegions)):
        for e in overlapCuboids(onRegions[r],region):
            newRegions.append(e)
    onRegions = newRegions[:]

onCount = 0
for r in onRegions:
    onCount += (r[0][1]+1-r[0][0])*(r[1][1]+1-r[1][0])*(r[2][1]+1-r[2][0])

print(onCount)
