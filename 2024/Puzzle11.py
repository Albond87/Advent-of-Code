import functools

@functools.cache
def evolvestone(n):
    if n == 0:
        return [1]
    else:
        s = str(n)
        if len(s) % 2 == 0:
            return [int(s[:len(s)//2]), int(s[len(s)//2:])]
        else:
            return [n*2024]

file = open("Inputs/input11.txt","r")
input = file.readline()
file.close()

stones = {}
for s in list(map(int, input.split(" "))):
    if s not in stones.keys():
        stones [s] = 1
    else:
        stones [s] += 1
        
blinks = 75
for b in range(blinks):
    nextstones = {}
    for s in stones.keys():
        evolved = evolvestone(s)
        for e in evolved:
            count = nextstones.get(e,0)
            nextstones[e] = count + stones [s]
    stones = nextstones
    
print(sum(stones.values()))