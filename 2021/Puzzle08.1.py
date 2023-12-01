file = open("Day8Input.txt","r")
lines = file.readlines()
file.close()

unique = 0
for l in lines:
    outputs = l.split(" | ")[1].split()
    for o in outputs:
        if len(o) in [2,3,4,7]:
            unique += 1

print(unique)
