from day_16_1 import parse_input

def is_valid(ticket, valid_dict):
    for i in ticket:
        if i not in valid_dict:
            return False
    
    return True

def in_range(i, lmt_1, lmt_2):
    return (i >= lmt_1[0] and i <= lmt_1[1]) or (i >= lmt_2[0] and i <= lmt_2[1])

if __name__ == '__main__':
    with open('../data/day_16.txt', 'r') as f:
        s = f.read()

    fields, valid_dict, ticket, nearby = parse_input(s)

    valid_tickets = [nt for nt in nearby if is_valid(nt, valid_dict)]
    valid_fields = [[k for k in fields] for _ in range(len(ticket))]

    taken = set()

    for nt in valid_tickets:
        for i in range(len(nt)):
            val = nt[i]
            valid_fields[i] = [vf for vf in valid_fields[i] if in_range(val, fields[vf][0], fields[vf][1])]

            if len(valid_fields[i]) == 1:
                taken.add(valid_fields[i][0])

    while len(taken) < len(ticket):
        for i in range(len(valid_fields)):
            if len(valid_fields[i]) == 1:
                continue

            valid_fields[i] = [vf for vf in valid_fields[i] if vf not in taken]
            
            if len(valid_fields[i]) == 1:
                taken.add(valid_fields[i][0])

    mul = 1

    for i in range(len(valid_fields)):
        f = valid_fields[i][0]
        
        if 'departure' in f:
            mul *= ticket[i]
    
    print(mul)
