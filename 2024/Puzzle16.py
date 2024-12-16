file = open("Inputs/input16.txt","r")
maze = file.readlines()
file.close()

dirvectors = [(1,0),(0,1),(-1,0),(0,-1)]

for y in range(len(maze)):
	for x in range(len(maze[y])):
		if maze[y][x] == 'S':
			(startx, starty) = (x,y)
		elif maze[y][x] == 'E':
			(goalx, goaly) = (x,y)

#print(pathfind(startx, starty, 0))
'''
shortest = {(startx, starty): (0,0)}
frontier = set([(startx, starty)])
explored = set()

while len(frontier) > 0:
	(x,y) = min(frontier,key=lambda x: shortest[x][0])
	frontier.remove((x,y))
	(dist,dir) = shortest[(x,y)]
	#if (x,y) in explored:
	#	continue
	# print(x,y,dir)
	if (x,y) == (goalx, goaly):
		# explored.add((x,y))
		break
	for d in range(dir-1, dir+2):
		newdir = d % 4
		newx = x + dirs[newdir][0]
		newy = y + dirs[newdir][1]
		if maze[newy][newx] == '#':
			continue
		if (newx, newy) in explored:
			continue
		turncost = 1000 if newdir != dir else 0
		newdist = dist + 1 + turncost
		currentshortest = shortest.get((newx,newy),(0,))[0]
		if currentshortest == 0 or newdist < currentshortest:
			shortest[(newx, newy)] = (newdist, newdir)
		frontier.add((newx,newy))
		
	explored.add((x,y))
'''

shortest = {(startx, starty):(0,[0])}
frontier = set([(startx, starty)])
explored = set()

while len(frontier) > 0:
	(x,y) = min(frontier, key=lambda x: shortest[x][0])
	frontier.remove((x,y))
	(dist, dirs) = shortest[(x,y)]
	dir = dirs[0]
	if (x,y) == (goalx, goaly):
		continue
	for d in range(dir-1,dir+2):
		newdir = d % 4
		newx = x + dirvectors[newdir][0]
		newy = y + dirvectors[newdir][1]
		if maze[newy][newx] == '#':
			continue
		turncost = 1000 if newdir != dir else 0
		newdist = dist + 1 + turncost
		currentshortest = shortest.get((newx, newy),(0,))[0]
		if currentshortest == 0:
			shortest[(newx,newy)] = (newdist, [newdir])
		elif newdist == currentshortest + 1000 and (newx,newy) != (goalx,goaly):
			shortest[(newx,newy)][1].append(newdir)
		elif newdist == currentshortest:
			ds = shortest[(newx, newy)][1]
			shortest[(newx,newy)] = (newdist, [newdir]+ds)
		elif newdist < currentshortest:
			shortest[(newx,newy)] = (newdist, [newdir])
		if (newx, newy) not in explored:
			frontier.add((newx, newy))
			
	explored.add((x,y))

onshortestpath = set()
frontier = set([(goalx,goaly)])
while len(frontier) > 0:
	(x,y) = frontier.pop()
	onshortestpath.add((x,y))
	if (x,y) == (startx,starty):
		continue
	for d in shortest[(x,y)][1]:
		newx = x - dirvectors[d][0]
		newy = y - dirvectors[d][1]
		if (newx,newy) not in onshortestpath:
			frontier.add((newx,newy))

print(shortest[(goalx, goaly)][0])
for y in range(len(maze)):
	for x in range(len(maze[y])):
		if (x,y) == (goalx,goaly):
			print('E',end='')
		elif (x,y) == (startx,starty):
			print('S',end='')
		elif maze[y][x] == '#':
			print('▒',end='')
		elif (x,y) in onshortestpath:
			print('█',end='')
		else:
			print(' ',end='')
	print()
print(len(onshortestpath))