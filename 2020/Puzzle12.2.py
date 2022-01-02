file = open("Day12Input.txt","r")
moves = file.read().split("\n")
file.close()

compass = ["E","S","W","N"]
dirs = [(1,0),(0,-1),(-1,0),(0,1)]

pos = [0,0]
d = 0
waypoint = [10,1]

for m in moves:
    move = m[0]
    amount = int(m[1:])
    if move == "F":
        pos[0] += waypoint[0]*amount
        pos[1] += waypoint[1]*amount
    elif move == "L":
        d = int((d-(amount/90)) % 4)
        for i in range(int(amount/90)):
            temp = waypoint[0]
            waypoint[0] = waypoint[1]*-1
            waypoint[1] = temp
    elif move == "R":
        d = int((d+(amount/90)) % 4)
        for i in range(int(amount/90)):
            temp = waypoint[0]
            waypoint[0] = waypoint[1]
            waypoint[1] = temp*-1
    else:
        c = dirs[compass.index(move)]
        waypoint[0] += c[0]*amount
        waypoint[1] += c[1]*amount
    #print(pos)
    #print(waypoint)

print(pos)
print(abs(pos[0])+abs(pos[1]))
