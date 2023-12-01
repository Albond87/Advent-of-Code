file = open("Day22Input.txt","r")
lines = file.readlines()
file.close()
lines[-1] = lines[-1]+"\n"

onCubes = {}

for l in lines:
    on = l[1] == 'n'
    axes = l.split(",")
    xs = axes[0].split("=")[1].split("..")
    x1 = int(xs[0])
    x2 = int(xs[1])
    if (x1 < -50 and x2 < -50) or (x2 > 50 and x2 > 50):
        continue
    x1 = max(x1,-50)
    x2 = min(x2,50)
    ys = axes[1][2:].split("..")
    y1 = int(ys[0])
    y2 = int(ys[1])
    if (y1 < -50 and y2 < -50) or (y2 > 50 and y2 > 50):
        continue
    y1 = max(y1,-50)
    y2 = min(y2,50)
    zs = axes[2][2:-1].split("..")
    z1 = int(zs[0])
    z2 = int(zs[1])
    if (z1 < -50 and z2 < -50) or (z2 > 50 and z2 > 50):
        continue
    z1 = max(z1,-50)
    z2 = min(z2,50)
    for x in range(x1,x2+1):
        for y in range(y1,y2+1):
            for z in range(z1,z2+1):
                onCubes[(x,y,z)] = on

onCount = 0
for c in onCubes:
    if onCubes[c]:
        onCount += 1

print(onCount)
