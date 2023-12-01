file = open("Day6Input.txt","r")
lines = file.readlines()
file.close()

fish = []
for i in range(int((len(lines[0])+1)/2)):
    fish.append(int(lines[0][i*2]))

counts = [0]*7
for f in fish:
    counts[f] = counts[f]+1

delay = [0]*7
days = 256

for d in range(days):
    i = d%7
    delay[(i+2)%7] = counts[i]
    counts[i] = counts[i] + delay[i]

#print(counts)
print(sum(counts)+delay[(days+1)%7]+delay[days%7])
