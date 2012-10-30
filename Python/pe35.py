"""
http://projecteuler.net/index.php?section=problems&id=35

The number, 197, is called a circular prime because all rotations of the digits: 197, 971, and 719, are themselves prime.

There are thirteen such primes below 100: 2, 3, 5, 7, 11, 13, 17, 31, 37, 71, 73, 79, and 97.

How many circular primes are there below one million?
"""

from pe10 import build_sieve

sieve = build_sieve(10**6)

def rotations( n ) :
    digits = str(n)
    return set(int( digits[x:]+digits[:x] ) for x in xrange(len(digits)))

def gen_circular(sieve) :
    s = sieve[:]   #copy 
    for i in xrange(len(s)):
        p = s[i]
        if p :            
            is_circular = True
            pr = rotations(p)
            for r in pr :
                if not s[r] :
                    is_circular = False
                s[r] = 0 # don't check the permutations
            if is_circular :
                for r in pr :
                    yield r

cn = set(gen_circular(sieve))                
        
s = len(cn)

assert s == 55
