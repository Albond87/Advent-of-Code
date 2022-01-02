def triNum(x):
    return ((x**2)+x)/2

file = open("Day7Input.txt","r")
lines = file.readlines()
file.close()

crabs = lines[0].split(",")
minX = 1000
maxX = 0
for c in range(len(crabs)):
    x = int(crabs[c])
    if x < minX:
        minX = x
    if x > maxX:
        maxX = x
    crabs[c] = x

positions = maxX+1-minX
distances = [0]*positions
for c in crabs:
    for d in range(positions):
        distances[d] += triNum(abs(c-(minX+d)))

print(int(min(distances)))
