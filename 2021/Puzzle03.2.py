def filterNums(nums,pos,least):
    split = [[],[]]
    for i in nums:
        split[int(i[pos])].append(i)

    zeros = len(split[0])
    ones = len(split[1])
    mostRet = 1
    if zeros > ones:
        mostRet = 0
    return split[least-mostRet]

file = open("Day3Input.txt","r")
lines = file.readlines()
file.close()

bits = len(lines[0])-1
o2GenRating = 0
o2Filter = lines[:]
co2ScrubRating = 0
co2Filter = lines

for b in range(bits):
    if len(o2Filter) > 1:
        o2Filter = filterNums(o2Filter,b,0)
    o2GenRating += (2**(bits-b-1))*int(o2Filter[0][b])
    if len(co2Filter) > 1:
        co2Filter = filterNums(co2Filter,b,1)
    co2ScrubRating += (2**(bits-b-1))*int(co2Filter[0][b])

print(o2Filter)
print(o2GenRating)
print(co2Filter)
print(co2ScrubRating)
print(o2GenRating * co2ScrubRating)
