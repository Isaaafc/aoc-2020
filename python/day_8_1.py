lines = []

with open('../data/day_8.txt', 'r') as f:
    lines = [(l.strip().split(' ')[0], int(l.strip().split(' ')[1])) for l in f.readlines()]

acc = 0
visited = []

i = 0

while i >= 0 and i < len(lines):
    if i in visited:
        break

    visited.append(i)

    if lines[i][0] == 'acc':
        acc += lines[i][1]
        i += 1
    elif lines[i][0] == 'jmp':
        i += lines[i][1]
    else:
        i += 1

print(i, acc)
