def crt(bus_times):
    

if __name__ == '__main__':
    bus_times = []

    with open('../data/day_13.txt', 'r') as f:
        i = 0
        x = 0

        for l in f.readlines()[1].split(','):
            if l.strip() != 'x':
                bus_times.append([int(l.strip()), x % int(l.strip())])
                x = 1
                i += 1
            else:
                x += 1

    bus_times.sort(key=lambda x: -x[1])
