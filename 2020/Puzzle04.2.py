def isValidYear(year, minYear, maxYear):
    intY = int(year)
    return len(year) == 4 and intY >= minYear and intY <= maxYear

file = open("Day4Input.txt","r")
lines = file.readlines()
file.close()
lines[len(lines)-1] += "\n"
lines.append("\n")

validCount = 0
fieldCount = 0
isCid = False

hexChars = "0123456789abcdef"
eyeColours = ["amb","blu","brn","gry","grn","hzl","oth"]

l = 0
while l < len(lines):
    if lines[l] == "\n":
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
        while lines[l][i] != "\n":
            if lines[l][i] == ":":
                check = ""
                i += 1
                while lines[l][i] != " " and lines[l][i] != "\n":
                    check += lines[l][i]
                    i += 1
                thisValid = False
                if current == "cid":
                    isCid = True
                    thisValid = True
                elif current == "byr":
                    thisValid = isValidYear(check, 1920, 2002)
                elif current == "iyr":
                    thisValid = isValidYear(check, 2010, 2020)
                elif current == "eyr":
                    thisValid = isValidYear(check, 2020, 2030)
                elif current == "hgt":
                    try:
                        num = int(check[:-2])
                        unit = check[-2:]
                        if unit == "cm":
                            if num >= 150 and num <= 193:
                                thisValid = True
                        elif unit == "in":
                            if num >= 59 and num <= 76:
                                thisValid = True
                    except:
                        pass
                elif current == "hcl":
                    if check[0] == "#" and len(check) == 7:
                        thisValid = True
                        for c in check[1:]:
                            if c not in hexChars:
                                thisValid = False
                                break
                elif current == "ecl":
                    thisValid = check in eyeColours
                elif current == "pid":
                    if len(check) == 9:
                        try:
                            check = int(check)
                            thisValid = True
                        except:
                            pass

                if thisValid:
                    fieldCount += 1
                else:
                    while lines[l] != "\n":
                        l += 1
                    l -= 1
                    i = -1
                current = ""
                
            else:
                current += lines[l][i]
            if lines[l][i] != "\n":
                i += 1
            #print(i)
    l += 1

print(validCount)
