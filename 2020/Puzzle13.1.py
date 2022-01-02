file = open("Day13Input.txt","r")
lines = file.read().split("\n")
file.close()

earliest = int(lines[0])
buses = lines[1].split(",")
while "x" in buses:
    buses.remove("x")

waitTimes = []
for b in buses:
    b = int(b)
    if earliest % b == 0:
        waitTimes.append((0,b))
    else:
        waitTimes.append((b - (earliest % b),b))

waitTimes.sort()
print(waitTimes)
print(waitTimes[0][0] * waitTimes[0][1])
