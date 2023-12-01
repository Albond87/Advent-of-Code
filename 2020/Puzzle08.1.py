file = open("Day8Input.txt","r")
lines = file.readlines()
file.close()

acc = 0
executed = [0]*len(lines)
pc = 0

while executed[pc] == 0:
    executed[pc] = 1
    cmd = lines[pc][:3]
    oper = int(lines[pc][4:-1])
    if cmd == "acc":
        acc += oper
    elif cmd == "jmp":
        pc += oper - 1
    pc += 1

print(acc)
