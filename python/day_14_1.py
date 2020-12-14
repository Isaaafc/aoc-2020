import re

def to_bin(n, bits=36):
    return bin(n)[2:].zfill(bits)

def apply_mask(mask, val):
    val = [c for c in val]

    for i, m in enumerate(mask):
        if m != 'X':
            val[i] = m
    
    return ''.join(val)

def run_prog(prog):
    mask, cmds = prog['mask'], prog['cmds']
    mem = dict()

    for cmd in cmds:
        val = to_bin(cmd[1])
        val = apply_mask(mask, val)
        mem[cmd[0]] = val
    
    return mem

def parse(lines):
    mem_re = re.compile(r'mem\[(?P<addr>\d+)\] = (?P<val>\d+)')

    programs = []

    prog = None

    for line in lines:
        if 'mask' in line:
            if prog:
                programs.append(prog)

            prog = dict()
            prog['mask'] = line.split(' = ')[1]
            prog['cmds'] = []
        else:
            m = mem_re.match(line)
            prog['cmds'].append((int(m.group('addr')), int(m.group('val'))))

    programs.append(prog)

    return programs

if __name__ == '__main__':
    with open('../data/day_14.txt', 'r') as f:
        lines = [line.strip() for line in f.readlines()]
        programs = parse(lines)

    mem = dict()
        
    for prog in programs:
        mem.update(run_prog(prog))

    s = sum([int(v, 2) for v in mem.values()])
    print(s)
