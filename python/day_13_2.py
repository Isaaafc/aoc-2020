if __name__ == '__main__':
    bus_times = []

    with open('../data/day_13.txt', 'r') as f:
        i = 0
        x = 0

        for l in f.readlines()[1].split(','):
            if l.strip() != 'x':
                bus_times.append((int(l.strip()), x % int(l.strip())))
                i += 1

            x += 1

    p, t = 1, 0

    for b, dt in bus_times:
        while (dt + t) % b != 0:
            t += p

        p *= b

    print(t)