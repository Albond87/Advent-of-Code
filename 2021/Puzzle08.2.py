file = open("Day8Input.txt","r")
lines = file.readlines()
file.close()

displays = { "abcefg"  : 0,
             "cf"      : 1,
             "acdeg"   : 2,
             "acdfg"   : 3,
             "bcdf"    : 4,
             "abdfg"   : 5,
             "abdefg"  : 6,
             "acf"     : 7,
             "abcdefg" : 8,
             "abcdfg"  : 9 }

total = 0
for l in lines:
    (signals,outputs) = l.split(" | ")
    signals = signals.split()
    outputs = outputs.split()
    signals.sort(key=len)
    mapping = [""]*10
    mapping[1] = sorted(signals[0])
    mapping[4] = sorted(signals[2])
    mapping[7] = sorted(signals[1])
    mapping[8] = list("abcdefg") #sorted(signals[9])
    for s in range(6,9):
        if mapping[1][0] not in signals[s] or mapping[1][1] not in signals[s]:
            mapping[6] = sorted(signals.pop(s))
            break
    for s in range(6,8):
        for i in mapping[4]:
            if i not in signals[s]:
                mapping[0] = sorted(signals.pop(s))
                break
        else:
            continue
        break
    mapping[9] = sorted(signals[6])
    for s in range(3,6):
        if (mapping[1][0] in signals[s]) and (mapping[1][1] in signals[s]):
            mapping[3] = sorted(signals.pop(s))
            break
    for s in range(3,5):
        for i in signals[s]:
            if i not in mapping[6]:
                mapping[2] = sorted(signals.pop(s))
                break
        else:
            continue
        break
    mapping[5] = sorted(signals[3])
    for o in range(4):
        total += (10**(3-o))*mapping.index(sorted(outputs[o]))

print(total)
