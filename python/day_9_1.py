def is_valid(n, preamble):
    for i in range(len(preamble) - 1):
        for j in range(i + 1, len(preamble)):
            if preamble[i] != preamble[j] and preamble[i] + preamble[j] == n:
                return True
    
    return False

def search(lines, n_preamble):
    for i in range(n_preamble, len(lines)):
        preamble = lines[i - n_preamble:i]

        if not is_valid(lines[i], preamble):
            return lines[i]

if __name__ == '__main__':
    lines = []

    with open('../data/day_9.txt', 'r') as f:
        lines = [int(line.strip()) for line in f.readlines()]

    print(search(lines, 25))