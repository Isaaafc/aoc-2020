import re

lines = []

with open('../data/day_4.txt', 'r') as f:
    lines = [' ' + l.replace('\n', ' ') + ' ' for l in f.read().split('\n\n')]

fields_num = {
    r' byr:(\d{4})( )': lambda x, _: x >= 1920 and x <= 2002,
    r' iyr:(\d{4})( )': lambda x, _: x >= 2010 and x <= 2020,
    r' eyr:(\d{4})( )': lambda x, _: x >= 2020 and x <= 2030,
    r' hgt:(\d{2,3})((in)|(cm)) ': lambda x, unit: (x >= 150 and x <= 193) if unit == 'cm' else (x >= 59 and x <= 76)
}

fields_str = [r' hcl:#[0-9a-f]{6} ', r' ecl:((amb)|(blu)|(brn)|(gry)|(grn)|(hzl)|(oth)) ', r' pid:[0-9]{9} ']

cnt = 0

for line in lines:
    f_cnt = 0

    for f in fields_num:
        m = re.search(f, line)

        if m:
            num = int(m.group(1))
            unit = m.group(2)

            if fields_num[f](num, unit):
                f_cnt += 1
            else:
                break
    
    for f in fields_str:
        if re.search(f, line):
            f_cnt += 1
        else:
            break

    if f_cnt == len(fields_num) + len(fields_str):
        cnt += 1

print(cnt)

