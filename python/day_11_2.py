from day_11_1 import matrix_sum, display, parse
import numpy as np

def evolve(matrix):
    change = False

    new_matrix = np.array(matrix)

    for i in range(len(matrix)):
        for j in range(len(matrix[0])):
            el = matrix[i, j]

            if el == -1:
                continue

            cnt = 0

            # Left
            for k in range(j - 1, -1, -1):
                if matrix[i, k] >= 0:
                    cnt += matrix[i, k]
                    break
            
            # Right
            for k in range(j + 1, len(matrix[0])):
                if matrix[i, k] >= 0:
                    cnt += matrix[i, k]
                    break
            
            # Top
            for k in range(i - 1, -1, -1):
                if matrix[k, j] >= 0:
                    cnt += matrix[k, j]
                    break

            # Bottom
            for k in range(i + 1, len(matrix)):
                if matrix[k, j] >= 0:
                    cnt += matrix[k, j]
                    break
            
            # Top left
            k, l = i - 1, j - 1
            
            while k >= 0 and l >= 0:
                if matrix[k, l] >= 0:
                    cnt += matrix[k, l]
                    break

                k -= 1
                l -= 1
            
            # Top right
            k, l = i - 1, j + 1
            
            while k >= 0 and l < len(matrix[0]):
                if matrix[k, l] >= 0:
                    cnt += matrix[k, l]
                    break

                k -= 1
                l += 1

            # Bottom left
            k, l = i + 1, j - 1

            while k < len(matrix) and l >= 0:
                if matrix[k, l] >= 0:
                    cnt += matrix[k, l]
                    break

                k += 1
                l -= 1
            
            # Bottom right
            k, l = i + 1, j + 1

            while k < len(matrix) and l < len(matrix[0]):
                if matrix[k, l] >= 0:
                    cnt += matrix[k, l]
                    break

                k += 1
                l += 1

            if el == 0 and cnt == 0:
                new_matrix[i, j] = 1
                change = True
            elif el == 1 and cnt >= 5:
                new_matrix[i, j] = 0
                change = True

    return new_matrix, change

if __name__ == '__main__':
    lines = []

    with open('../data/day_11.txt', 'r') as f:
        lines = [line.strip() for line in f.readlines()]
        
    matrix = parse(lines)
    change = True

    while change:
        matrix, change = evolve(matrix)

    print(matrix_sum(matrix))