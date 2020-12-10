def chain(lines):
    lines = list(lines)
    lines.sort()

    lines = [0] + lines + [lines[-1] + 3]

    count = [0, 0]

    for i in range(1, len(lines)):
        diff = lines[i] - lines[i - 1]

        if diff == 1:
            count[0] += 1
        elif diff == 3:
            count[1] += 1
        elif diff < 1 or diff > 3:
            print('Invalid: idx {} values {} {}'.format(i, lines[i], lines[i - 1]))
            break
    
    return count

if __name__ == '__main__':
    lines = []

    with open('../data/day_10.txt', 'r') as f:
        lines = [int(line.strip()) for line in f.readlines()]

    count = chain(lines)
    print(count[0] * count[1])