file = open("Day3Input.txt","r")
lines = file.readlines()
file.close()

bits = len(lines[0])-1
bitCounts = [0]*bits
for l in lines:
    for b in range(bits):
        bitCounts[b] += int(l[b])

gammaRate = 0
for i in range(bits):
    if (bitCounts[i] > len(lines)/2):
        gammaRate += 2**(bits-i-1)

epsilonRate = (2**bits)-1-gammaRate
print(gammaRate)
print(epsilonRate)
print(gammaRate * epsilonRate)
