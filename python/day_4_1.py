import re

lines = []

with open('../data/day_4.txt', 'r') as f:
    lines = [' ' + l.replace('\n', ' ').strip() for l in f.read().split('\n\n')]

fields = ['byr', 'iyr', 'eyr', 'hgt', 'hcl', 'ecl', 'pid']

cnt = 0

for line in lines:
    f_cnt = 0

    for f in fields:
        if re.search(r'\s' + '{}:'.format(f), line):
            f_cnt += 1

    if f_cnt == len(fields):
        cnt += 1

print(cnt)

