file = open("Day4Input.txt","r")
lines = file.readlines()
file.close()
lines[len(lines)-1] += "\n"
lines.append("\n")

validCount = 0
fieldCount = 0
isCid = False

for l in lines:
    if l == "\n":
        #print(fieldCount)
        if fieldCount == 8:
            validCount += 1
        elif fieldCount == 7 and not isCid:
            validCount += 1
        fieldCount = 0
        isCid = False
    else:
        i = 0
        current = ""
        while l[i] != "\n":
            if l[i] == ":":
                fieldCount += 1
                if current == "cid":
                    isCid = True
                current = ""
                while l[i] != " " and l[i] != "\n":
                    i += 1
            else:
                current += l[i]
            if l[i] != "\n":
                i += 1
            #print(i)

print(validCount)
