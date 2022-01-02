def tribonacci(n):
    sequence = [0,1,1]
    for i in range(n-1):
        sequence.append(sum(sequence[i:i+3]))
    return sequence[-1]

file = open("Day10Input.txt","r")
adapters = file.read().split("\n")
file.close()

for a in range(len(adapters)):
    adapters[a] = int(adapters[a])

adapters.sort()

oneRuns = []
if adapters[0] == 1:
    oneRuns.append(1)
    isOne = True
else:
    isOne = False
    
for i in range(len(adapters)-1):
    if adapters[i+1]-adapters[i] == 1:
        if isOne:
            oneRuns[-1] += 1
        else:
            oneRuns.append(1)
            isOne = True
    else:
        isOne = False

product = 1
for o in oneRuns:
    product *= tribonacci(o)

print(product)
