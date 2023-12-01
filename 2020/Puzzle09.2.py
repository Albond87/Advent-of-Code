def sumExists(nums,target):
    for i in range(24):
        for j in range(i+1,25):
            if nums[i] + nums[j] == target:
                return True

file = open("Day9Input.txt","r")
numbers = file.read()
file.close()
numbers = numbers.split("\n")
for n in range(len(numbers)):
    numbers[n] = int(numbers[n])

for n in range(25,len(numbers)):   
    if not sumExists(numbers[n-25:n],numbers[n]):
        invalid = numbers[n]
        break

done = False
n = 0
m = 1
total = numbers[n] + numbers[m]
while not done:
    if total < invalid:
        m += 1
        total += numbers[m]
    elif total > invalid:
        total -= numbers[n]
        n += 1
    if total == invalid:
        minimum = numbers[n]
        maximum = numbers[n]
        for i in numbers[n:m+1]:
            if i < minimum:
                minimum = i
            elif i > maximum:
                maximum = i
        print(minimum + maximum)
        done = True
