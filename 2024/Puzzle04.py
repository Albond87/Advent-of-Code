import numpy as np

file = open("Inputs/input04.txt","r")
inputs = file.readlines()
file.close()

rows = np.array(list(map(lambda i: list(i.replace("\n","")), inputs)))

# Part 1
xmas_checks = [np.array([["X","M","A","S"]]),
               np.array([["X","","",""],
                         ["","M","",""],
                         ["","","A",""],
                         ["","","","S"]])]

# Part 2
xmas_checks = [np.array([["M","","S"],
                         ["","A",""],
                         ["M","","S",]])]

count = 0
for xmas in xmas_checks:
    for i in range(4):
        check = np.rot90(xmas,i)
        check_bool = check == ""
        (height, width) = check.shape
        for y in range(0,rows.shape[0]-height+1):
            for x in range(0,rows.shape[1]-width+1):
                match = rows[y:y+height,x:x+width] == check
                match = np.logical_or(match, check_bool)
                if match.all():
                    count += 1

print(count)