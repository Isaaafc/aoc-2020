lines = []

with open('../data/day_6.txt', 'r') as f:
    lines = [l.replace('\n', ' ').strip() for l in f.read().split('\n\n')]

cnt = 0

for line in lines:
    s = set('abcdefghijklmnopqrstuvwxyz')

    for ans in line.split(' '):
        s = s.intersection(set(ans))
    
    cnt += len(s)

print(cnt)