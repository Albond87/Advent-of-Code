def search_trail(x,y,h=0):
	global nines
	if h == 9:
		if nines.get((x,y), None) is None:
			nines[(x,y)] = 1
		else:
			nines[(x,y)] += 1
		return
	search_pos = []
	if y > 0:
		search_pos.append((x,y-1))
	if x > 0:
		search_pos.append((x-1,y))
	if x < width-1:
		search_pos.append((x+1,y))
	if y < height-1:
		search_pos.append((x,y+1))
	for p in search_pos:
		if heights[p[1]][p[0]] == h+1:
			search_trail(p[0], p[1],h+1)

file = open("Inputs/input10.txt","r")
inputs = file.readlines()
file.close()

heights = list(map(lambda i: list(map(int,i.replace("\n", ""))), inputs))

sum_scores = 0
sum_ratings = 0
width = len(heights[0])
height = len(heights)
nines = {}
for y in range(height):
	for x in range(width):
		if heights[y][x] == 0:
			nines = {}
			search_trail(x,y)
			sum_scores += len(nines.keys())
			sum_ratings += sum(nines.values())
			
print(sum_scores)
print(sum_ratings)