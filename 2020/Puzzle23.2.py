class Node:
    def __init__(self,val,nextp):
        self.val = val
        self.next = nextp

file = open("Day23Input.txt","r")

cupsRaw = file.read()
file.close()

cupsRaw2 = [999999,1000000]
for c in cupsRaw:
    cupsRaw2.append(int(c))

cupsRaw2.append(10)
cupsRaw2.append(11)
cups = [0]*1000001
for c in range(1,12):
    cups[cupsRaw2[c]] = Node(cupsRaw2[c],cupsRaw2[c+1])

for c in range(11,1000000):
    cups[c] = Node(c,c+1)

current = cupsRaw2[2]

minCup = 1
maxCup = 1000000
moves = 10000000

for m in range(moves):
    pickUp = []
    pointer = cups[current].next
    for i in range(3):
        pickUp.append(cups[pointer].val)
        pointer = cups[pointer].next
    cups[current].next = pointer
    destination = cups[current].val - 1
    if destination < minCup:
        destination = maxCup
    while destination in pickUp:
        destination -= 1
        if destination < minCup:
            destination = maxCup
    finalDest = cups[destination].next
    cups[destination].next = pickUp[0]
    cups[pickUp[2]].next = finalDest
    current = cups[current].next

product = cups[1].next
product *= cups[product].next
print(product)
