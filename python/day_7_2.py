import re

def count(graph, head, num):
    if head not in graph or len(graph[head]) == 0:
        return num
    
    s = num * (sum([count(graph, h[0], h[1]) for h in graph[head]]) + 1)

    return s

lines = []

with open('../data/day_7.txt', 'r') as f:
    lines = [l.strip() for l in f.readlines()]

graph = dict()
ptn = re.compile(r'(\d+ )?([a-z\s]+) bag')

for l in lines:
    s = l.split(' contain ')
    key = ptn.match(s[0]).group(2)

    values = [b.strip() for b in s[1].split(',') if 'no other bags' not in b]
    values = [(ptn.match(b).group(2), int(ptn.match(b).group(1).strip())) for b in values]

    graph[key] = values

print(count(graph, 'shiny gold', 1) - 1)