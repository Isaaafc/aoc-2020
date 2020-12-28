def run_loop(log, last_num):
    turn = len(log) + 1
    arr = [None for _ in range(30000000)]
    
    for k in log:
        arr[k] = log[k]

    while turn < 30000000:
        if not arr[last_num]:
            new_last_num = 0
        else:
            new_last_num = turn - arr[last_num]
        
        arr[last_num] = turn
        turn += 1
        last_num = new_last_num

    return last_num

if __name__ == '__main__':
    with open('../data/day_15.txt', 'r') as f:
        lines = [line.strip() for line in f.readlines()]

    for line in lines:
        log = {int(c):(i + 1) for i, c in enumerate(line.split(',')[:-1])}
        print(run_loop(log, int(line.split(',')[-1])))