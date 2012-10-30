"""
http://projecteuler.net/index.php?section=problems&id=32

We shall say that an n-digit number is pandigital if it
makes use of all the digits 1 to n exactly once; for example,
the 5-digit number, 15234, is 1 through 5 pandigital.

The product 7254 is unusual, as the identity, 39 * 186 = 7254,
containing multiplicand, multiplier, and product is 1 through 9 pandigital.

Find the sum of all products whose multiplicand/multiplier/product
identity can be written as a 1 through 9 pandigital.

HINT: Some products can be obtained in more than one way so be
sure to only include it once in your sum.
"""


digits = set( str(x) for x in range(1,10) )

is_pandigital = lambda n : len(str(n))==9 and set(str(n)) == digits

from itertools import permutations

def gen_terms() :
    # 4 digit * 1 digit >= 4 digit
    # x digit * x digit <= 2*x digit
    # x digit * y digit >= x+y digit
    for i in xrange(1,5) :
        for pi in permutations( digits, i ) :
            a = int(''.join(pi))
            for j in xrange(1,5-i+1) :
                for pj in permutations( digits-set(pi), j ) :
                    b = int(''.join(pj))
                    c = a*b
                    if is_pandigital( "%s%s%s" % (a,b,c) ) :
                        yield min(a,b), max(a,b), c

pd = list(set(gen_terms()))

s = sum(set(pdi[2] for pdi in pd))
