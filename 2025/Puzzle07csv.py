# Convert input into a csv that can be used in Excel solution
file = open("Inputs/input07.txt","r")
lines = file.readlines()
file.close()

lines = list(map(lambda l: ",".join(list(l.replace("\n","")))+"\n", lines))
file = open("Inputs/input07.csv","w")
file.writelines(lines)
file.close()