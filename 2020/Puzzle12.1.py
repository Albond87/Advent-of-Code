file = open("Day12Input.txt","r")
moves = file.read().split("\n")
file.close()

compass = ["E","S","W","N"]
dirs = [(1,0),(0,-1),(-1,0),(0,1)]

pos = [0,0]
d = 0

for m in moves:
    move = m[0]
    amount = int(m[1:])
    if move == "F":
        pos[0] += dirs[d][0]*amount
        pos[1] += dirs[d][1]*amount
    elif move == "L":
        d = int((d-(amount/90)) % 4)
    elif move == "R":
        d = int((d+(amount/90)) % 4)
    else:
        c = dirs[compass.index(move)]
        pos[0] += c[0]*amount
        pos[1] += c[1]*amount

print(pos)
print(abs(pos[0])+abs(pos[1]))
