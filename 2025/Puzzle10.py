from z3 import *

file = open("Inputs/input10.txt", "r")
inputs = file.readlines()
file.close()

lights = []
buttons = []
joltages = []

for line in inputs:
    line = line.replace("\n", "")
    i1 = line.index(']')
    i2 = line.index('{')
    lights.append([c=='#' for c in line[1:i1]])
    buttons.append([[int(b) for b in bs[1:-1].split(',')] for bs in line[i1+2:i2-1].split()])
    joltages.append([int(j) for j in line[i2+1:-1].split(',')])

total = 0
for i in range(len(joltages)):
    solver = Optimize()
    buttonVars = []
    for b in range(len(buttons[i])):
        buttonVars.append(Int('b'+str(b)))
        solver.add(buttonVars[-1] >= 0)

    for j in range(len(joltages[i])):
        buttonsAffecting = []
        for b in buttons[i]:
            if j in b:
                buttonsAffecting.append(1)
            else:
                buttonsAffecting.append(0)
        solver.add(sum([buttonsAffecting[b] * buttonVars[b] for b in range(len(buttons[i]))])==joltages[i][j])
    
    solver.minimize(sum(buttonVars))
    solver.check()
    result = solver.model()
    total += sum([result[b].as_long() for b in buttonVars])

print(total)