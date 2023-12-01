file = open("Day6Input.txt","r")
lines = file.readlines()
file.close()
lines[-1] += "\n"
lines.append("\n")

questions = "abcdefghijklmnopqrstuvwxyz"
answeredYes = [0]*26
total = 0
groupSize = 0
for l in lines:
    if l == "\n":
        for a in answeredYes:
            if a == groupSize:
                total += 1
        answeredYes = [0]*26
        groupSize = 0
    else:
        groupSize += 1
        for c in l[:-1]:
            answeredYes[questions.index(c)] += 1

print(total)
