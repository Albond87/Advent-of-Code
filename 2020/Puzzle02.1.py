file = open("Day2Input.txt","r")
lines = file.readlines()
file.close()

valid = 0
for l in lines:
    minimum = ""
    i = 0
    while l[i] != "-":
        minimum += l[i]
        i += 1
    minimum = int(minimum)
    i += 1
    maximum = ""
    while l[i] != " ":
        maximum += l[i]
        i += 1
    maximum = int(maximum)
    i += 1
    char = l[i]
    i += 3
    password = l[i:-1]
    count = password.count(char)
    if count >= minimum and count <= maximum:
        valid += 1

print(valid)
