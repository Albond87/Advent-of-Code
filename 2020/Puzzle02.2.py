file = open("Day2Input.txt","r")
lines = file.readlines()
file.close()

valid = 0
for l in lines:
    pos1 = ""
    i = 0
    while l[i] != "-":
        pos1 += l[i]
        i += 1
    pos1 = int(pos1)
    i += 1
    pos2 = ""
    while l[i] != " ":
        pos2 += l[i]
        i += 1
    pos2 = int(pos2)
    i += 1
    char = l[i]
    i += 2
    password = l[i:-1]
    if (password[pos1] == char) ^ (password[pos2] == char):
        valid += 1
        
print(valid)
