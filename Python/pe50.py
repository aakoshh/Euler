"""
The prime 41, can be written as the sum of six consecutive primes:

41 = 2 + 3 + 5 + 7 + 11 + 13
This is the longest sum of consecutive primes that adds to a prime below one-hundred.

The longest sum of consecutive primes below one-thousand that adds to a prime, contains 21 terms, and is equal to 953.

Which prime, below one-million, can be written as the sum of the most consecutive primes?
"""

from math import sqrt
from itertools import takewhile

def is_prime(n) :
    for i in xrange(2, int(sqrt(n))+1) :
        if n % i == 0 :
            return False
    return True


def next_prime(n):
    while not is_prime(n) :
        n += 1
    return n


def primes() :
    i = 1
    while True :
        i = next_prime(i+1)
        yield i


def cumulative_sum(s):
    cs = []
    css = 0
    for si in s :
        css += si
        cs.append( css )
    return cs


def longest_seq_under(n) :
    ps = list(takewhile( lambda p : p < n, primes()))
    pss = set(ps)
    cs = cumulative_sum(ps)
    cnt = len(ps)
    max_len = len(list(takewhile(lambda s : s < n, cs)))

    def subsum(i, j):
        return cs[j] - (cs[i-1] if i > 0 else 0)

    def interval_with_length(m) :
        for i in xrange(0, cnt-m+1) :
            j = i + m - 1            
            sij = subsum(i,j)
            if sij >= n :
                return None, None
            if sij in pss : # prime
                return i, j+1
        return None, None
    
    for m in xrange(max_len, 0, -1) :
        f, t = interval_with_length(m)
        if t :
            return ps[f:t]
             
    
assert longest_seq_under(100) == [2, 3, 5, 7, 11, 13]
assert sum(longest_seq_under(1000)) == 953

import timeit
timeit.Timer("sum(longest_seq_under(100000))", "from __main__ import longest_seq_under").timeit(1)
