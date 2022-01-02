class Queue:
    def __init__(self,maxSize):
        self.__array = [0]*maxSize
        self.__front = 0
        self.__size = 0
        self.__maxSize = maxSize

    def push(self,value):
        if self.__size == self.__maxSize:
            print("Cannot push - queue is full")
            return
        self.__array[(self.__front + self.__size)%self.__maxSize] = value
        self.__size += 1

    def pop(self):
        if self.__size == 0:
            #print("Cannot pop - queue is empty")
            return None
        value = self.__array[self.__front]
        self.__front = (self.__front + 1) % self.__maxSize
        self.__size -= 1
        return value

file = open("Day22Input.txt","r")
playersRaw = file.read().split("\n\n")
file.close()

playersRaw[0] = playersRaw[0].split("\n")[1:]
playersRaw[1] = playersRaw[1].split("\n")[1:]

deckSize = len(playersRaw[0])+len(playersRaw[1])

players = []
for i in range(2):
    players.append(Queue(deckSize))
    for c in playersRaw[i]:
        players[i].push(int(c))

winner = -1
while winner == -1:
    top = []
    for p in range(2):
        t = players[p].pop()
        if t == None:
            winner = 1-p
            break
        top.append(t)
    else:
        if top[0] > top[1]:
            players[0].push(top[0])
            players[0].push(top[1])
        else:
            players[1].push(top[1])
            players[1].push(top[0])

winHand = top
score = 0
for i in range(deckSize):
    winHand.append(players[winner].pop())
    score += winHand[i]*(deckSize-i)

print(winner+1)
print(score)
