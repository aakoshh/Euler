"""
http://projecteuler.net/index.php?section=problems&id=26

A unit fraction contains 1 in the numerator.
The decimal representation of the unit fractions with
denominators 2 to 10 are given:

1/2	= 	0.5
1/3	= 	0.(3)
1/4	= 	0.25
1/5	= 	0.2
1/6	= 	0.1(6)
1/7	= 	0.(142857)
1/8	= 	0.125
1/9	= 	0.(1)
1/10	= 	0.1
Where 0.1(6) means 0.166666..., and has a 1-digit recurring cycle.
It can be seen that 1/7 has a 6-digit recurring cycle.

Find the value of d  1000 for which 1/d contains the longest
recurring cycle in its decimal fraction part.
"""

def longest(n) :
    L = {}
    i = 0
    while True :
        i += 1
        d, r = 10**i // n, 10**i % n
        if not r :
            return 0
        if r in L :
            return len(str(d)) - len(str(L[r]))
        L[r] = d

for i,r in [(2,0), (3,1), (4,0), (5,0), (6,1), (7,6), (8,0), (9,1), (10,0)] :
    assert longest(i) == r

s = max( (longest(i), i) for i in xrange(1,1000) )[1]                       

assert s == 983
