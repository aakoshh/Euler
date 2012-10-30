# -*- coding: cp1252 -*-
"""
http://projecteuler.net/index.php?section=problems&id=31

In England the currency is made up of pound, £, and pence, p,
and there are eight coins in general circulation:

1p, 2p, 5p, 10p, 20p, 50p, £1 (100p) and £2 (200p).
It is possible to make £2 in the following way:

1£1 + 150p + 220p + 15p + 12p + 31p
How many different ways can £2 be made using any number of coins?
"""

coins = (1,2,5,10,20,50,100,200) 

maxways = reduce( lambda x,y : x * (200/y + 1), coins, 1)
# would need to filter the sum from 6 billion combinations

from pe14 import memo

def solve( n, coins ) :    
    coins = sorted(coins, reverse=True)#descending goes to zero faster    
    @memo #the same sum is reached on multiple ways
    def num_comb( n, firstcoin ) :
        if not n : return 1 #no money left
        if n == coins[-1] : return 1 #only the smallest coin fits
        for i, c in enumerate(coins[firstcoin:]) :            
            if n >= c :
                #the greatest coin that fits. add the number of ways
                #of possible combinations when the coin is included, or when it's excluded
                incl = num_comb( n-c, firstcoin+i + (1 if n-c < c else 0) )
                excl = num_comb( n, firstcoin+i+1 ) # as if it did not exist
                return incl + excl #this is where the branching effect kicks in
        return 0 #no more coins
    return num_comb( n, 0 ) 

assert solve(1,coins) == 1
assert solve(2,coins) == 2
assert solve(3,coins) == 2
assert solve(4,coins) == 3
assert solve(5,coins) == 4

#assert solve(200,coins) == 73682
