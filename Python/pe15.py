"""
http://projecteuler.net/index.php?section=problems&id=15

Starting in the top left corner of a 22 grid,
there are 6 routes (without backtracking) to the bottom right corner.


How many routes are there through a 2020 grid?
"""

from pe14 import memo

@memo
def fact(n) :
    if n <= 1 : return 1
    return n * fact(n-1)

def fact_norec(n) :
    p = 1
    while not n <= 1 :
        p *= n
        n -= 1
    return p

# we have to go right and down a fixed number of times
# permutate the number of movements, count the unique ones
def num_paths(grid_size) :
    right = down = grid_size
    return fact(right+down) / fact(right) / fact(down)

assert num_paths(2) == 6

assert num_paths(20) == 137846528820
