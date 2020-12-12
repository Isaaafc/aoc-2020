import math

def parse(lines):
    return [(line[:1], int(line[1:])) for line in lines]    

def move(lines):
    x, y, angle = 0, 0, 90

    for cmd, arg in lines:
        if cmd == 'L':
            angle = (angle - arg) % 360
        elif cmd == 'R':
            angle = (angle + arg) % 360
        elif cmd == 'N':
            y += arg
        elif cmd == 'S':
            y -= arg
        elif cmd == 'W':
            x -= arg
        elif cmd == 'E':
            x += arg
        elif cmd == 'F':
            x += arg * math.sin(math.radians(angle))
            y += arg * math.cos(math.radians(angle))

        x, y, angle = round(x), round(y), round(angle)

    return x, y, angle

if __name__ == '__main__':
    lines = []

    with open('../data/day_12.txt', 'r') as f:
        lines = [line.strip() for line in f.readlines()]

    lines = parse(lines)
    x, y, angle = move(lines)

    print(x, y, angle)
    print(abs(x) + abs(y))
