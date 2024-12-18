def combo_operand(operand):
    global reg_a, reg_b, reg_c
    if operand < 4:
        return operand
    if operand == 4:
        return reg_a
    if operand == 5:
        return reg_b
    if operand == 6:
        return reg_c
    return None

def run_program(a=None,runwhole=True):
    global reg_a, reg_a_init, reg_b, reg_b_init, reg_c, reg_c_init, program
    reg_a = reg_a_init
    reg_b = reg_b_init
    reg_c = reg_c_init
    if a is not None:
        reg_a = a
    ip = 0
    out = ""
    while ip < len(program):
        opcode = program[ip]
        operand = program[ip+1]
        if opcode == 0:
            reg_a = reg_a // (2**combo_operand(operand))
        elif opcode == 1:
            reg_b = reg_b ^ operand
        elif opcode == 2:
            reg_b = combo_operand(operand) % 8
        elif opcode == 3:
            if reg_a != 0:
                ip = operand - 2
        elif opcode == 4:
            reg_b = reg_b ^ reg_c
        elif opcode == 5:
            out += str(combo_operand(operand)%8)
            if not runwhole:
                return out
        elif opcode == 6:
            reg_b = reg_a // (2**combo_operand(operand))
        elif opcode == 7:
            reg_c = reg_a // (2**combo_operand(operand))
        ip += 2
    return ",".join(out)

file = open("Inputs/input17.txt","r")
inputs = file.readlines()
file.close()

reg_a_init = int(inputs[0][12:-1])
reg_b_init = int(inputs[1][12:-1])
reg_c_init = int(inputs[2][12:-1])
reg_a, reg_b, reg_c = 0, 0, 0

program_str = inputs[4][9:].replace(",","")
program = list(map(int,list(program_str)))

# Part 1 - run the program for the given initial register values
print(run_program())

# Part 2 - find the initial A value which causes the program to output itself
# Find candidates for each output digit in turn, in reverse order
# This assumes the program is a looping program which divides A by 8 at each iteration
# and outputs one value every iteration

# First find the value for A in the last iteration which outputs the last digit
# Can't be 0 as the program would have ended at the previous iteration
a_candidates = []
for a in range(1,8):
    if run_program(a,False) == program_str[-1]:
        for i in range(8):
            a_candidates.append((a*8+i,len(program_str)-2))
        break

# For every value A that produces the correct output for the current iteration,
# the possible values for the previous iteration are any which divide by 8 to give A
# This could be A*8 up to (A*8)+7 (due to integer division rounding down)
while True:
    (a,ind) = a_candidates.pop(0)
    if run_program(a,False) == program_str[ind]:
        if ind == 0:
            # Correct first digit found, so the current A value must output the program itself
            break
        for i in range(8):
            a_candidates.append((a*8+i,ind-1))

print(a)