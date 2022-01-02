file = open("Day1Input.txt","r")
lines = file.readlines()
file.close()

for l in range(len(lines)):
    lines[l] = int(lines[l].replace("\n",""))

count = 0
window = lines[0:3]
prevSum = sum(window)
pos = 0
for i in lines[3:]:
    window[pos] = i
    newSum = sum(window)
    if (newSum > prevSum):
        count += 1
    prevSum = newSum
    pos = (pos+1)%3

print(count)
