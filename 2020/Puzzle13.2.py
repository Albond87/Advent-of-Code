file = open("Day13Input.txt","r")
lines = file.read().split("\n")
file.close()

buses = lines[1].split(",")

for b in range(len(buses)):
    if buses[b] != "x":
        buses[b] = int(buses[b])

m = int(buses[0])
lcm = m
for b in range(1,len(buses)):
    if buses[b] != "x":
        success = False
        while not success:
            if (m+b) % buses[b] == 0:
                success = True
            else:
                m += lcm
        lcm *= buses[b]
        
print(m)
