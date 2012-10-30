"""
http://projecteuler.net/index.php?section=problems&id=33

The fraction 49/98 is a curious fraction, as an inexperienced
mathematician in attempting to simplify it may incorrectly
believe that 49/98 = 4/8, which is correct, is obtained by cancelling the 9s.

We shall consider fractions like, 30/50 = 3/5, to be trivial examples.

There are exactly four non-trivial examples of this type of fraction,
less than one in value, and containing two digits in the numerator
and denominator.

If the product of these four fractions is given in its lowest common terms,
find the value of the denominator.
"""


def curious_pairs() :
    for n in xrange(1,10) :
        for d in xrange(n+1,10) : # n/d < 1
            for i in xrange(1,10) :
                f1 = float(10*n+i) / float(10*i+d)
                f0 = float(n) / float(d)
                if abs(f1 - f0) < 10**-6 :
                    yield n,d,i

cp = list(curious_pairs())

prod = lambda seq : reduce(lambda acc,x : acc*x, seq)
n, d = prod( p[0] for p in cp ), prod( p[1] for p in cp )

def gcd(a,b): return b and gcd(b, a % b) or a

s = d / gcd(n,d)

assert s == 100

