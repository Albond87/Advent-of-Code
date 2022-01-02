def removeConfirmed(remove,i):
    global allergenIngreds
    global confirmed
    allergenIngreds[i] = allergenIngreds[i].difference(remove)
    if len(allergenIngreds[i]) == 1:
        confirmed = confirmed.union(allergenIngreds[i])
        for j in list(allergenIngreds):
            if j != i and len(allergenIngreds[j]) > 1:
                removeConfirmed(allergenIngreds[i],j)


file = open("Day21Input.txt","r")
foods = file.read().split("\n")
file.close()

allergenIngreds = {}
allIngreds = []
confirmed = set()
for f in foods:
    split = f.split(" (contains ")
    ingreds = split[0].split(" ")
    allIngreds.append(ingreds)
    allergens = split[1][:-1].split(", ")

    for a in allergens:
        if allergenIngreds.get(a) == None:
            allergenIngreds[a] = set(ingreds).difference(confirmed)
        else:
            if len(allergenIngreds[a]) > 1:
                allergenIngreds[a].intersection_update(set(ingreds).difference(confirmed))
                if len(allergenIngreds[a]) == 1:
                    if not allergenIngreds[a].issubset(confirmed):
                        confirmed = confirmed.union(allergenIngreds[a])
                        for i in list(allergenIngreds):
                            if i != a and len(allergenIngreds[i]) > 1:
                                removeConfirmed(allergenIngreds[a],i)

count = 0    
for l in allIngreds:
    for i in l:
        if not i in confirmed:
            count += 1

print(count)
