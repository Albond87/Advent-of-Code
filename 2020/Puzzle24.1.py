file = open("Day24Input.txt","r")
flips = file.read().split("\n")
file.close()

flipped = {}
count = 0

for f in flips:
    pos = [0,0]
    i = 0
    while i < len(f):
        move = f[i]
        if move == "n":
            pos[1] += 1
            i += 1
            move = f[i]
            if move == "w":
                pos[0] -= 1
        elif move == "s":
            pos[1] -= 1
            i += 1
            move = f[i]
            if move == "e":
                pos[0] += 1
        elif move == "e":
            pos[0] += 1
        else:
            pos[0] -= 1
        i += 1
    pos = tuple(pos)
    if flipped.get(pos):
        flipped.pop(pos)
        count -= 1
    else:
        flipped[pos] = 1
        count += 1

print(count)
