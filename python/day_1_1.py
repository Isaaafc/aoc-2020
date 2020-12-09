"""
Brute force
"""

expenses = []

with open('../data/day_1_1.txt', 'r') as f:
    expenses = [int(l) for l in f.readlines()]

for i in range(len(expenses)):
    for j in range(i + 1, len(expenses)):
        if expenses[i] + expenses[j] == 2020:
            print(expenses[i] * expenses[j])