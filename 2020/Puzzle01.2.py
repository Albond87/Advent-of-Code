import sys

file = open("Day1Input.txt","r")
entries = file.readlines()
file.close()

for i in entries:
    i = int(i.replace("\n",""))
    for j in entries:
        j = int(j.replace("\n",""))
        for k in entries:
            k = int(k.replace("\n",""))
            if i+j+k == 2020:
                print(i)
                print(j)
                print(k)
                print(i*j*k)
                sys.exit()
