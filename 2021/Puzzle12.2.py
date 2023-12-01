import time

def getPaths(cave,visited,duplicate):
    global conns
    paths = []
    for c in conns.get(cave):
        newDuplicate = duplicate
        if c == "end":
            paths.append(cave+",end")
            continue
        elif c == "start":
            continue
        elif c == c.lower() and c in visited:
            if duplicate:
                continue
            else:
                newDuplicate = True
        for p in getPaths(c,visited+[cave],newDuplicate):
            paths.append(cave+","+p)
    return paths
            
file = open("Day12Input.txt","r")
lines = file.readlines()
file.close()
lines[-1] = lines[-1]+"\n"

conns = {}
for l in lines:
    conn = l[:-1].split("-")
    conns[conn[0]] = conns.get(conn[0],[])+[conn[1]]
    conns[conn[1]] = conns.get(conn[1],[])+[conn[0]]

print(len(getPaths("start",[],False)))
