def dp(lines):
    lines = list(lines)
    lines.sort()
    lines = [0] + lines

    count = [0 for _ in lines]

    # There is always 1 way for the last adapter to go to the device
    count[-1] = 1

    # For the ith adapter, the number of ways to the last adapter is the sum of the number of ways to each successive adapter with diff 1..3
    for i in range(len(lines) - 2, -1, -1):
        cnt = 0

        for j in range(i + 1, len(lines)):
            if lines[j] - lines[i] in [1, 2, 3]:
                cnt += count[j]
        
        count[i] = cnt

    return count[0]

if __name__ == '__main__':
    lines = []

    with open('../data/day_10.txt', 'r') as f:
        lines = [int(line.strip()) for line in f.readlines()]

    print(dp(lines))