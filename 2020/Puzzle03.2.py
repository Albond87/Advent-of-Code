file = open("Day3Input.txt","r")
lines = file.readlines()
file.close()

counts = [0,0,0,0,0]
columns = [0,0,0,0,0]
offsets = [1,3,5,7,0.5]

for i in lines[1:]:
    i = i.replace("\n","")
    for c in range(len(columns)):
        columns[c] += offsets[c]
        if columns[c] >= len(i):
            columns[c] -= len(i)
        
        if c == 4:
            if columns[4] % 1 != 0:
                continue
        if i[int(columns[c])] == "#":
            counts[c] += 1

product = 1
for x in counts:
    print(x)
    product *= x

print(product)
