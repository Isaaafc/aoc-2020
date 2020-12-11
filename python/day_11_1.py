import numpy as np

def matrix_sum(matrix):
    # print(matrix)
    # print(np.clip(matrix, 0, 1))
    # print(np.sum(np.clip(matrix, 0, 1)))
    # input('Enter')

    return np.sum(np.clip(matrix, 0, 1))

def evolve(matrix):
    change = False

    new_matrix = np.array(matrix)

    for i in range(len(matrix)):
        for j in range(len(matrix[0])):
            el = matrix[i, j]

            if el == -1:
                continue

            l, r, t, b = max(0, j - 1), min(len(matrix[0]), j + 2), max(0, i - 1), min(len(matrix), i + 2)

            ms = matrix_sum(matrix[t:b, l:r]) - el 

            if el == 0 and ms == 0:
                new_matrix[i, j] = 1
                change = True
            elif el == 1 and ms > 3:
                new_matrix[i, j] = 0
                change = True

    return new_matrix, change

def display(matrix):
    disp = []

    for i in range(len(matrix)):
        row = ''

        for j in range(len(matrix[0])):
            el = matrix[i, j]
            
            if el == -1:
                row = row + '.'
            elif el == 0:
                row = row + 'L'
            else:
                row = row + '#'
            
        disp.append(row)
    
    return '\n'.join(disp)

def parse(lines):
    res = []

    for l in lines:
        row = []

        for c in l:
            if c == '.':
                row.append(-1)
            elif c == 'L':
                row.append(0)
            else:
                row.append(1)
        
        res.append(row)

    return np.array(res)

if __name__ == '__main__':
    lines = []

    with open('../data/day_11.txt', 'r') as f:
        lines = [line.strip() for line in f.readlines()]
        
    matrix = parse(lines)
    change = True
    i = 0

    while change:
        matrix, change = evolve(matrix)
        i += 1

    print(i)
    print(matrix_sum(matrix))