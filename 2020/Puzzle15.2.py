file = open("Day15Input.txt","r")
starting = file.read().split(",")
file.close()

spoken1 = {}
spoken2 = {}
for s in range(len(starting)):
    spoken1[int(starting[s])] = s

last = int(starting[-1])
for i in range(len(starting),30000000):
    if spoken2.get(last) == None:
        last = 0
        spoken2[0] = spoken1.get(0)
        spoken1[0] = i
    else:
        last = spoken1.get(last) - spoken2.get(last)
        spoken2[last] = spoken1.get(last)
        spoken1[last] = i

print(last)
