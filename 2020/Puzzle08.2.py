file = open("Day8Input.txt","r")
lines = file.readlines()
file.close()
lines[-1] += "\n"

end = len(lines)
success = False

change = 0

while not success:
    fixed = lines[:]
    while lines[change][0] == "a":
        change += 1
    cmd = lines[change][:3]
    rest = lines[change][3:]
    if cmd == "jmp":
        cmd = "nop"
    else:
        cmd = "jmp"
    fixed[change] = cmd + rest
    change += 1
    
    acc = 0
    executed = [0]*end
    pc = 0
    while executed[pc] == 0:
        executed[pc] = 1
        cmd = fixed[pc][:3]
        oper = int(fixed[pc][4:-1])
        if cmd == "acc":
            acc += oper
        elif cmd == "jmp":
            pc += oper - 1
        pc += 1
        if pc >= end:
            success = True
            pc = 0

print(acc)
print(change-1)
