import re

def parse_fields(s):
    ptn = re.compile(r'(?P<field>[a-z\s]+): (?P<llmt_1>\d{1,3})-(?P<ulmt_1>\d{1,3}) or (?P<llmt_2>\d{1,3})-(?P<ulmt_2>\d{1,3})')
    s = s.split('\n')

    fields = dict()
    valid_dict = dict()

    for field in s:
        match = ptn.match(field)
        lmt_1 = int(match.group('llmt_1')), int(match.group('ulmt_1'))
        lmt_2 = int(match.group('llmt_2')), int(match.group('ulmt_2'))

        for i in range(lmt_1[0], lmt_1[1] + 1):
            valid_dict[i] = True
        
        for i in range(lmt_2[0], lmt_2[1] + 1):
            valid_dict[i] = True

        fields[match.group('field')] = lmt_1, lmt_2

    return fields, valid_dict

def to_int(s):
    return [int(x) for x in s.strip().split(',')]

def parse_input(s):
    spl = s.split('\n\n')

    fields, valid_dict = parse_fields(spl[0])
    ticket = to_int(spl[1].split('\n')[1])
    nearby = [to_int(line) for line in spl[2].split('\n')[1:-1]]

    return fields, valid_dict, ticket, nearby

if __name__ == '__main__':
    s = None

    with open('../data/day_16.txt', 'r') as f:
        s = f.read()

    fields, valid_dict, ticket, nearby = parse_input(s)

    error = 0

    for nt in nearby:
        for i in nt:
            if i not in valid_dict:
                error += i
    
    print(error)