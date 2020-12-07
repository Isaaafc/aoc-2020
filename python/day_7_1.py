import re

def search(graph, key, head, visited):
    if len(graph[head]) == 0 or head in visited:
        return False

    if key in graph[head]:
        return True

    for h in graph[head]:
        if search(graph, key, h, visited + [head]):
            return True

    return False

lines = []

with open('../data/day_7.txt', 'r') as f:
    lines = [l.strip() for l in f.readlines()]

graph = dict()
ptn = re.compile(r'(\d+ )?([a-z\s]+) bag')

all_colors = []

for l in lines:
    s = l.split(' contain ')
    key = ptn.match(s[0]).group(2)
    values = [ptn.match(b.strip()).group(2) for b in s[1].split(',') if 'no other bags' not in b]

    all_colors = all_colors + [key] + values

    graph[key] = values

cnt = 0

for c in list(set(all_colors)):
    if search(graph, 'shiny gold', c, []):
        cnt += 1

print(cnt)