"""
http://projecteuler.net/index.php?section=problems&id=7

By listing the first six prime numbers: 2, 3, 5, 7, 11, and 13, we can see that the 6th prime is 13.

What is the 10001st prime number?

"""

from pe3 import next_prime
from itertools import takewhile

def primes() :
    i = 1
    while True :
        i = next_prime(i+1)
        yield i

def nth_prime(n) :
    return list(takewhile( lambda ip : ip[0] < n, enumerate(primes()) ))[-1][1]

assert nth_prime(6) == 13

assert nth_prime(10001) == 104743
