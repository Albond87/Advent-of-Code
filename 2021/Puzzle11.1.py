def flash(x,y):
    global octopi
    octopi[y][x] = 0
    flashes = 1
    for j in range(-1,2):
        if y+j < 0 or y+j >= len(octopi):
            continue
        for i in range(-1,2):
            if x+i < 0 or x+i >= len(octopi[y+j]):
                continue
            new = octopi[y+j][x+i]
            if new != 0:
                new += 1
                octopi[y+j][x+i] = new
                if new > 9:
                    flashes += flash(x+i,y+j)
    return flashes
            

file = open("Day11Input.txt","r")
lines = file.readlines()
file.close()
lines[-1] = lines[-1]+"\n"

octopi = []
for l in lines:
    octopi.append([])
    for i in l[:-1]:
        octopi[-1].append(int(i))
#print(octopi)

steps = 100

flashes = 0
for s in range(steps):
    flashPos = []
    for y in range(len(octopi)):
        for x in range(len(octopi[y])):
            new = octopi[y][x] + 1
            if new > 9:
                flashPos.append((x,y))
            octopi[y][x] = new

    for p in flashPos:
        if octopi[p[1]][p[0]] != 0:
            flashes += flash(p[0],p[1])

print(flashes)
