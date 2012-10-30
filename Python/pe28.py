"""
http://projecteuler.net/index.php?section=problems&id=28
Starting with the number 1 and moving to the right in a clockwise
direction a 5 by 5 spiral is formed as follows:

21 22 23 24 25
20  7  8  9 10
19  6  1  2 11
18  5  4  3 12
17 16 15 14 13

It can be verified that the sum of the numbers on the diagonals is 101.

What is the sum of the numbers on the diagonals in a
1001 by 1001 spiral formed in the same way?
"""


def diagonals(size) :
    i, d = 1, 2
    yield i
    while d < size :
        for x in xrange(4):
            i+= d
            yield i
        d += 2

assert sum(diagonals(5)) == 101            
    
assert sum(diagonals(1001)) == 669171001
