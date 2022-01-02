file = open("Day2Input.txt","r")
lines = file.readlines()
file.close()

for l in range(len(lines)):
    lines[l] = lines[l].replace("\n","")

pos = 0
depth = 0
aim = 0
for l in lines:
    move = l.split(" ")
    move[1] = int(move[1])
    if move[0] == "forward":
        pos += move[1]
        depth += move[1] * aim
    elif move[0] == "up":
        aim -= move[1]
    elif move[0] == "down":
        aim += move[1]

print(pos)
print(depth)
print(pos*depth)
