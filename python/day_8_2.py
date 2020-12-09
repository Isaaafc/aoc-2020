lines = []

with open('../data/day_8.txt', 'r') as f:
    lines = [(l.strip().split(' ')[0], int(l.strip().split(' ')[1])) for l in f.readlines()]

update_queue = []

for j in range(len(lines)):
    if lines[j][0] == 'jmp':
        update_queue.append((j, 'nop'))
    elif lines[j][0] == 'nop':
        update_queue.append((j, 'jmp'))

infinite = True

while infinite and len(update_queue) > 0:
    upd = update_queue.pop()

    lines_copy = list(lines)
    lines_copy[upd[0]] = (upd[1], lines_copy[upd[0]][1])

    i, acc = 0, 0
    visited = []

    while i >= 0 and i < len(lines_copy):
        if i in visited:
            break

        visited.append(i)

        if lines_copy[i][0] == 'acc':
            acc += lines_copy[i][1]
            i += 1
        elif lines_copy[i][0] == 'jmp':
            i += lines_copy[i][1]
        else:
            i += 1
    
    if i == len(lines_copy):
        infinite = False

print(infinite, i, acc)
