file = open("Day13Input.txt","r")
lines = file.read().split("\n\n")
file.close()

rawDots = lines[0].split("\n")
folds = lines[1].split("\n")

dots = []
for d in rawDots:
    coord = d.split(",")
    dots.append([int(coord[0]),int(coord[1])])

print(len(dots))
for f in folds:
    foldPos = int(f[13:])
    flip = 0
    if f[11] == "y":
        flip = 1
    for d in range(len(dots)-1,-1,-1):
        new = dots[d][:]
        if new[flip] > foldPos:
            new[flip] = foldPos-(new[flip]-foldPos)
            if new in dots:
                dots.pop(d)
            else:
                dots[d] = new
    print(len(dots))

for y in range(6):
    for x in range(40):
        if [x,y] in dots:
            print("#",end="")
        else:
            print(".",end="")
    print()
