def split(line, l, r, lc, rc):
    for c in line:
        half = 1 + (r - l) // 2

        if c == lc:
            l += half
        elif c == rc:
            r -= half

    return l

def seat(line):
    row = split(line[:7], 0, 127, 'B', 'F')
    col = split(line[7:], 0, 7, 'R', 'L')

    return row * 8 + col

lines = []

with open('../data/day_5.txt', 'r') as f:
    lines = f.readlines()

seats = [seat(line) for line in lines]

print(max(seats))