file = open("Day15Input.txt","r")
lines = file.readlines()
file.close()
lines[-1] = lines[-1]+"\n"

risk = []
for l in lines:
    risk.append([])
    for i in l[:-1]:
        risk[-1].append(int(i))
#print(risk)

paths = { (0,0):0 }
distances = [0]
shortest = 0
goalReached = False

while not goalReached:
    sp = (0,0)
    shortest = distances[-1]
    for p in paths:
        current = paths.get(p)
        if current == shortest:
            sp = p
            break
    if sp == (len(risk[0])-1,len(risk)-1):
        goalReached = True
        break
    distances.pop()
    for i in [(-1,0),(0,1),(0,-1),(1,0)]:
        x = sp[0] + i[0]
        y = sp[1] + i[1]
        if x < 0 or y < 0 or x >= len(risk[0]) or y >= len(risk):
            continue
        current = paths.get((x,y),0)
        new = shortest + risk[y][x]
        if current == 0 or (current>0 and new < current):
            paths[(x,y)] = new
            distances.append(new)

    paths[sp] = -1
    distances.sort(reverse=True)

print(shortest)
