file = open("Day23Input.txt","r")
cupsRaw = file.read()
file.close()

cups = []
for c in cupsRaw:
    cups.append(int(c))

minCup = 1
maxCup = 9
moves = 100
current = 0

for m in range(moves):
    pickUp = []
    for i in range(3):
        nextClockwise = (current+1)%len(cups)
        if nextClockwise < current:
            current -= 1
        pickUp.append(cups.pop(nextClockwise))
    destination = cups[current]-1
    if destination < minCup:
        destination = maxCup
    while destination in pickUp:
        destination -= 1
        if destination < minCup:
            destination = maxCup
    destId = cups.index(destination)+1
    for p in range(2,-1,-1):
        cups.insert(destId,pickUp[p])
    if destId <= current:
        current += 3
    current = (current+1)%len(cups)

print(cups)

upTo1 = ""
after1 = ""
i = 0
while cups[i] != 1:
    upTo1 += str(cups[i])
    i += 1
i += 1
while i < len(cups):
    after1 += str(cups[i])
    i += 1
print(after1+upTo1)
