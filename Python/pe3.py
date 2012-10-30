"""
http://projecteuler.net/index.php?section=problems&id=3

The prime factors of 13195 are 5, 7, 13 and 29.

What is the largest prime factor of the number 600851475143 ?
"""
from math import sqrt

def is_prime(n) :
    for i in xrange(2, int(sqrt(n))+1) :
        if n % i == 0 :
            return False
    return True

def next_prime(n):
    while not is_prime(n) :
        n += 1
    return n

def prime_factors(n) :
    f = 2
    while n >= f > 1 :
        if n % f == 0 :
           yield f
           n /= f
        else :
            f = next_prime(f+1)

mf = max( prime_factors( 600851475143 ) )

assert mf == 6857
