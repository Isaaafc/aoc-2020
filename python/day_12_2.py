from day_12_1 import parse
import math

def move(lines):
    ship_x, ship_y = 0, 0
    wp_x, wp_y = 10, 1

    for cmd, arg in lines:
        if cmd == 'L':
            if arg == 90:
                wp_x, wp_y = -wp_y, wp_x
            elif arg == 180:
                wp_x, wp_y = -wp_x, -wp_y
            elif arg == 270:
                wp_x, wp_y = wp_y, -wp_x
        elif cmd == 'R':
            if arg == 90:
                wp_x, wp_y = wp_y, -wp_x
            elif arg == 180:
                wp_x, wp_y = -wp_x, -wp_y
            elif arg == 270:
                wp_x, wp_y = -wp_y, wp_x
        elif cmd == 'N':
            wp_y += arg
        elif cmd == 'S':
            wp_y -= arg
        elif cmd == 'W':
            wp_x -= arg
        elif cmd == 'E':
            wp_x += arg
        elif cmd == 'F':
            ship_x += wp_x * arg
            ship_y += wp_y * arg

    return ship_x, ship_y

if __name__ == '__main__':
    lines = []

    with open('../data/day_12.txt', 'r') as f:
        lines = [line.strip() for line in f.readlines()]

    lines = parse(lines)

    x, y = move(lines)

    print(x, y)
    print(abs(x) + abs(y))
