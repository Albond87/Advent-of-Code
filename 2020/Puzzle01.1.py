import sys

file = open("Day1Input.txt","r")
entries = file.readlines()
file.close()

for i in entries:
    i = int(i.replace("\n",""))
    for j in entries:
        j = int(j.replace("\n",""))
        if i+j == 2020:
            print(i)
            print(j)
            print(i*j)
            sys.exit()
