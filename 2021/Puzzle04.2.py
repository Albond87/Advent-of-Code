def getWinScore(nums,boards):
    for n in nums:
        won = []
        for b in range(len(boards)):
            colWin = [True]*5
            rowWin = True
            for r in range(5):
                rowWin = True
                for c in range(5):
                    if boards[b][r][c] == n:
                        boards[b][r][c] = ""
                    elif boards[b][r][c]:
                        rowWin = False
                        colWin[c] = False
                if rowWin:
                    break
            if rowWin or True in colWin:
                if len(boards)-len(won) == 1:
                    sumU = 0
                    for r in boards[b]:
                        for i in r:
                            if i:
                                sumU += int(i)
                    print(sumU)
                    print(n)
                    print(sumU*int(n))
                    return
                else:
                    won.append(b)
        won.sort(reverse=True)
        for w in won:
            boards.pop(w)


file = open("Day4Input.txt","r")
lines = file.readlines()
file.close()

nums = lines[0][:-1].split(",")

boards = []
for b in range(int(len(lines)/6)):
    boards.append([])
    for r in range(b*6+2,b*6+7):
        boards[b].append(lines[r].split())

getWinScore(nums,boards)

