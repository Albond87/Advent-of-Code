file = open("Day3Input.txt","r")
lines = file.readlines()
file.close()

count = 0
column = 0

for i in lines[1:]:
    i = i.replace("\n","")
    column += 3
    if column >= len(i):
        column -= len(i)
        
    if i[column] == "#":
        count += 1

print(count)
