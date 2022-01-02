class Queue:
    def __init__(self,maxSize,array=[]):
        if array == []:
            self.__array = [0]*maxSize
            self.__size = 0
        else:
            self.__size = len(array)
            self.__array = array+([0]*(maxSize-len(array)))
        self.__front = 0
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

    def getArray(self):
        a = self.__array[self.__front : self.__front+self.__size]
        if len(a) < self.__size:
            a += self.__array[:(self.__front + self.__size)%self.__maxSize]
        return a

    def getSize(self):
        return self.__size

def playGame(players,gameNo):
    previous = []
    while True:
        current = (players[0].getArray(),players[1].getArray())
        if current in previous[:-1]:
            #print(len(previous))
            if gameNo == 1:
                return calculateScore(current[0])
            else:
                return 0
        else:
            previous.append(current)
            top = []
            for p in range(2):
                t = players[p].pop()
                if t == None:
                    #print(len(previous))
                    if gameNo == 1:
                        return calculateScore(current[1-p])
                    else:
                        return 1-p
                top.append(t)
                
            if players[0].getSize() >= top[0] and players[1].getSize() >= top[1]:
                newP1 = Queue(top[0]+top[1],current[0][1:top[0]+1])
                newP2 = Queue(top[0]+top[1],current[1][1:top[1]+1])
                winner = playGame([newP1,newP2],gameNo+1)
            else:
                if top[0] > top[1]:
                    winner = 0
                else:
                    winner = 1
            if winner == 0:
                players[0].push(top[0])
                players[0].push(top[1])
            else:
                players[1].push(top[1])
                players[1].push(top[0])

def calculateScore(hand):
    score = 0
    for c in range(len(hand)):
        score += hand[c]*(len(hand)-c)
    return score

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

print(playGame(players,1))
