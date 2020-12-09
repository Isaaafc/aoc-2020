from day_9_1 import search

def find_contiguous_sum(lines, n):
    l, r = 0, 1

    while r > l and r <= len(lines):
        window = lines[l:r]

        if sum(window) < n:
            r += 1
        elif sum(window) > n:
            l += 1
        else:
            return window

    return []

if __name__ == '__main__':
    lines = []

    with open('../data/day_9.txt', 'r') as f:
        lines = [int(line.strip()) for line in f.readlines()]

    first_invalid = search(lines, 25)

    window = find_contiguous_sum(lines, first_invalid)
    window.sort()

    print(window[0] + window[-1])