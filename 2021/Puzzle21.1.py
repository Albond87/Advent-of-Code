file = open("Day21Input.txt","r")
lines = file.readlines()
file.close()

players = [int(lines[0][-2]),int(lines[1][-1])]
scores = [0,0]
die = 2
rolls = 0

player = 1
while scores[player] < 1000:
    player = 1-player
    space = players[player]
    space += die*3
    space = ((space-1)%10)+1
    players[player] = space
    scores[player] += space
    die += 3
    if die > 100:
        die -= 100
    rolls += 3

print(player)
print(scores)
print(rolls)
print(scores[1-player]*rolls)
