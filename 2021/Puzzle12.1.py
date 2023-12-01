def getPaths(cave,visited):
    global conns
    paths = []
    for c in conns.get(cave):
        if c == "end":
            paths.append(cave+",end")
        elif not (c == c.lower() and c in visited):
            for p in getPaths(c,visited+[cave]):
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

print(len(getPaths("start",[])))
