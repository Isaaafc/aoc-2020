with open('../data/day_13.txt', 'r') as f:
    arrive_time = int(f.readline().strip())
    bus_times = [int(l.strip()) for l in f.readline().split(',') if l.strip() != 'x']

min_id = max(bus_times) + 1
ttw = min_id

for t in bus_times:
    rem = arrive_time % t

    if rem == 0:
        min_id = t
        ttw = 0
        break
    elif t - rem < ttw:
        min_id = t
        ttw = t - rem

ttw = min_id - (arrive_time % min_id)
print(min_id * ttw)
