from day_14_1 import parse, to_bin

def run_prog(prog):
    mask, cmds = prog['mask'], prog['cmds']
    mem = dict()

    n_comb = 2 ** len([c for c in mask if c == 'X'])
    bits = len(to_bin(n_comb, 0)) - 1

    for cmd in cmds:
        addr = to_bin(cmd[0])
        val = to_bin(cmd[1])

        masked_addr = apply_mask(mask, addr)

        for comb in range(n_comb):
            masked_comb = apply_comb(comb, masked_addr, bits)
            mem[int(masked_comb, 2)] = val

    return mem

def apply_mask(mask, val):
    val = [c for c in val]

    for i, m in enumerate(mask):
        if m != '0':
            val[i] = m
    
    return ''.join(val)

def apply_comb(comb, val, bits):
    val = [c for c in val]
    comb = [c for c in to_bin(comb, bits)]
    
    for i, c in enumerate(val):
        if c == 'X':
            val[i] = comb.pop(0)

    return ''.join(val)

if __name__ == '__main__':    
    with open('../data/day_14.txt', 'r') as f:
        lines = [line.strip() for line in f.readlines()]
        programs = parse(lines)

    mem = dict()

    for prog in programs:
        mem.update(run_prog(prog))

    s = sum([int(v, 2) for v in mem.values()])

    print(s)