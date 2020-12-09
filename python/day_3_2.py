lines = []

with open('../data/day_3.txt', 'r') as f:
    lines = [l.strip() for l in f.readlines()]

slopes = [(1, 1), (3, 1), (5, 1), (7, 1), (1, 2)]

mul = 1

for slope in slopes:
    i, cnt = 0, 0

    for j in range(slope[1], len(lines), slope[1]):
        i = (i + slope[0]) % len(lines[0])

        if lines[j][i] == '#':
            cnt += 1

    mul *= cnt

print(mul)
