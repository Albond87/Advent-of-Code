file = open("Day7Input.txt","r")
lines = file.readlines()
file.close()

colours = []
for l in lines:
    new = []
    word = ""
    outColour = ""
    i = 0
    while word != "bags":
        word += l[i]
        if l[i] == " ":
            outColour += word
            word = ""
        i += 1
    new.append(outColour[:-1])
    i += 9
    if l[i] == "n":
        colours.append([outColour[:-1]])
        continue
    while i < len(l)-2:
        new.append(int(l[i]))
        i += 2
        word = ""
        inColour = ""
        while word != "bag":
            word += l[i]
            if l[i] == " ":
                inColour += word
                word = ""
            i += 1
        if inColour == "shiny gold ":
            i = 200
            continue
        else:
            new.append(inColour[:-1])
        
        if l[i] == "s":
            i += 1
        i += 2
    if outColour == "shiny gold ":
        shinyGold = new[1:]
    elif i != 200:
        colours.append(new)

total = 0
c = 1
while c < len(shinyGold):
    total += shinyGold[c-1]
    r = 0
    while colours[r][0] != shinyGold[c]:
        r += 1
    for b in range(int(len(colours[r][1:])/2)):
        shinyGold.append(colours[r][1+(b*2)]*shinyGold[c-1])
        shinyGold.append(colours[r][2+(b*2)])
    c += 2

print(total)
