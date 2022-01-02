file = open("Day11Input.txt","r")
rows = file.read().split("\n")
file.close()

for r in range(len(rows)):
    rows[r] = list(rows[r])

maxRow = len(rows)
maxCol = len(rows[0])

change = True

while change:
    change = False
    new = []
    for row in rows:
        new.append(row[:])
    for r in range(maxRow):
        for c in range(maxCol):
            current = rows[r][c]
            if current != ".":
                ys = -1
                ye = 2
                if r == 0:
                    ys = 0
                elif r == maxRow-1:
                    ye = 1
                xs = -1
                xe = 2
                if c == 0:
                    xs = 0
                elif c == maxCol-1:
                    xe = 1
                occupied = 0
                for y in range(ys,ye):
                    for x in range(xs,xe):
                        if (x,y) != (0,0):
                            if rows[r+y][c+x] == "#":
                                occupied += 1
                if current == "L" and occupied == 0:
                    new[r][c] = "#"
                    change = True
                elif current == "#" and occupied > 3:
                    new[r][c] = "L"
                    change = True
    rows = new

count = 0
for r in rows:
    count += r.count("#")

print(count)
