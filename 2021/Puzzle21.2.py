def getOutcomes(players,scores,turn,roll):
    space = players[turn]
    space += roll
    if space > 10:
        space -= 10
    #space = ((space-1)%10)+1
    players[turn] = space
    scores[turn] += space
    if scores[turn] >= 21:
        return (1-turn,turn)
    else:
        o3 = getOutcomes(players[:],scores[:],1-turn,3)
        o4 = getOutcomes(players[:],scores[:],1-turn,4)
        o5 = getOutcomes(players[:],scores[:],1-turn,5)
        o6 = getOutcomes(players[:],scores[:],1-turn,6)
        o7 = getOutcomes(players[:],scores[:],1-turn,7)
        o8 = getOutcomes(players[:],scores[:],1-turn,8)
        o9 = getOutcomes(players[:],scores[:],1-turn,9)
        return (o3[0]+(o4[0]*3)+(o5[0]*6)+(o6[0]*7)+(o7[0]*6)+(o8[0]*3)+o9[0],o3[1]+(o4[1]*3)+(o5[1]*6)+(o6[1]*7)+(o7[1]*6)+(o8[1]*3)+o9[1])

file = open("Day21Input.txt","r")
lines = file.readlines()
file.close()

players = [int(lines[0][-2]),int(lines[1][-1])]
print(players)
scores = [0,0]

o3 = getOutcomes(players[:],scores[:],0,3)
o4 = getOutcomes(players[:],scores[:],0,4)
o5 = getOutcomes(players[:],scores[:],0,5)
o6 = getOutcomes(players[:],scores[:],0,6)
o7 = getOutcomes(players[:],scores[:],0,7)
o8 = getOutcomes(players[:],scores[:],0,8)
o9 = getOutcomes(players[:],scores[:],0,9)
print(o3[0]+(o4[0]*3)+(o5[0]*6)+(o6[0]*7)+(o7[0]*6)+(o8[0]*3)+o9[0],o3[1]+(o4[1]*3)+(o5[1]*6)+(o6[1]*7)+(o7[1]*6)+(o8[1]*3)+o9[1])
