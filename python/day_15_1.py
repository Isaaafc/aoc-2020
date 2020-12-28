import sys
import copy

def run(log, last_num, turn):
    if turn == 2020:
        return last_num

    new_log = copy.deepcopy(log)
    new_log[last_num] = turn

    if last_num not in log:
        return run(new_log, 0, turn + 1)
    else:
        return run(new_log, turn - log[last_num], turn + 1)

if __name__ == '__main__':
    sys.setrecursionlimit(3000)

    with open('../data/day_15.txt', 'r') as f:
        lines = [line.strip() for line in f.readlines()]

    for line in lines:
        log = {int(c):(i + 1) for i, c in enumerate(line.split(','))}
        print(run(log, 0, len(log) + 1))
