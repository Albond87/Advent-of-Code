file = open("Day7Input.txt","r")
lines = file.readlines()
file.close()

colours = []
for l in lines:
    #print("new")
    word = ""
    outColour = ""
    i = 0
    while word != "bags":
        word += l[i]
        if l[i] == " ":
            outColour += word
            word = ""
        i += 1
    i += 11
    if l[i] == " ":
        continue
    while i < len(l)-2:
        word = ""
        inColour = ""
        while word != "bag":
            word += l[i]
            if l[i] == " ":
                inColour += word
                word = ""
            i += 1
        colours.append([inColour[:-1],outColour[:-1]])
        
        if l[i] == "s":
            i += 1
        i += 4

containers = ["shiny gold"]
checked = 0
while checked < len(containers):
    for r in colours:
        if r[0] == containers[checked]:
            if r[1] not in containers:
                containers.append(r[1])
    checked += 1

print(containers)
