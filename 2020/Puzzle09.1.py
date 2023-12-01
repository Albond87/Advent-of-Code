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
        print(numbers[n])
        break
