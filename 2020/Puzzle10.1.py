file = open("Day10Input.txt","r")
adapters = file.read().split("\n")
file.close()

for a in range(len(adapters)):
    adapters[a] = int(adapters[a])

adapters.sort()

differences = [0]*4
differences[adapters[0]] += 1
for i in range(len(adapters)-1):
    differences[adapters[i+1]-adapters[i]] += 1

differences[3] += 1

print(differences)
print(differences[1]*differences[3])
