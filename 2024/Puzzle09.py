file = open("Inputs/input09.txt", "r")
input = file.readline()
file.close()

# For part 1
disk = []
gaps = []
# For part 2
address = 0
disk2 = {}
files = {}
gaps2 = []

filetoggle = True
id = 0
for c in input:
	c = int(c)
	if filetoggle:
		disk += [id]*c
		files[id] = address
		disk2[address] = {'size':c, 'id':id}
		id += 1
	else:
		gaps += list(range(len(disk),len(disk)+c))
		if c > 0:
			gaps2.append(address)
			disk2[address] = {'size':c, 'id':-1}
		disk += [None]*c
	address += c
	filetoggle = not filetoggle

# Part 1
while len(gaps) > 0:
	end = None
	while end is None:
		end = disk.pop()
	gap = gaps.pop(0)
	if gap >= len(disk):
		disk.append(end)
	else:
		disk[gap] = end

checksum = sum(map(lambda i: i[0] * i[1], zip(disk, range(len(disk)))))
print(checksum)

# Part 2
for f in range(id-1, -1, -1):
	fileaddress = files[f]
	filesize = disk2[fileaddress]['size']
	for g in range(len(gaps2)):
		gapaddress = gaps2[g]
		if gapaddress > fileaddress:
			break
		gapsize = disk2[gapaddress]['size']
		if gapsize == filesize:
			disk2[fileaddress]['id'] = -1
			files[f] = gapaddress
			disk2[gapaddress]['id'] = f
			gaps2.pop(g)
			break
		elif gapsize > filesize:
			disk2[fileaddress]['id'] = -1
			files[f] = gapaddress
			disk2[gapaddress]['size'] = filesize
			disk2[gapaddress]['id'] = f
			disk2[gapaddress + filesize] = {'size': gapsize - filesize, 'id':-1}
			gaps2[g] = gapaddress + filesize
			break

disk2list = []
for a in sorted(disk2.keys()):
	disk2list += [disk2[a]['id'] if disk2[a]['id'] > -1 else 0] * disk2[a]['size']

checksum = sum(map(lambda i: i[0] * i[1], zip(disk2list, range(len(disk2list)))))
print(checksum)