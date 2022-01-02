file = open("Day6Input.txt","r")
lines = file.readlines()
file.close()
lines[-1] += "\n"
lines.append("\n")

answeredYes = []
total = 0
for l in lines:
    if l == "\n":
        total += len(answeredYes)
        answeredYes = []
    else:
        for c in l[:-1]:
            if c not in answeredYes:
                answeredYes.append(c)

print(total)
