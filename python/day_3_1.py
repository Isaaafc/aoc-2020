lines = []

with open('../data/day_3.txt', 'r') as f:
    lines = [l.strip() for l in f.readlines()]

i, cnt = 0, 0

for j in range(1, len(lines)):
    i = (i + 3) % len(lines[0])

    if lines[j][i] == '#':
        cnt += 1

print(cnt)
