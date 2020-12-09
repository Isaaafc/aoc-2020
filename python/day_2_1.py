import re

def parse_line(s):
    ptn = re.compile(r'(\d+)\-(\d+) ([a-z]): (.+)$')
    match = ptn.match(s)

    return int(match.group(1)), int(match.group(2)), match.group(3), match.group(4)
    
pws = []

with open('../data/day_2.txt', 'r') as f:
    pws = [parse_line(l) for l in f.readlines()]

cnt = 0

for l, r, c, pw in pws:
    c_cnt = 0

    for a in pw:
        if a == c:
            c_cnt += 1
    
    if c_cnt >= l and c_cnt <= r:
        cnt += 1

print(cnt)