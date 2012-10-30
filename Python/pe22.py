"""
http://projecteuler.net/index.php?section=problems&id=22

Using names.txt (right click and 'Save Link/Target As...'),
a 46K text file containing over five-thousand first names,
begin by sorting it into alphabetical order.
Then working out the alphabetical value for each name,
multiply this value by its alphabetical position in the list
to obtain a name score.

For example, when the list is sorted into alphabetical order,
COLIN, which is worth 3 + 15 + 12 + 9 + 14 = 53, is the 938th name in the list. So, COLIN would obtain a score of 938  53 = 49714.

What is the total of all the name scores in the file?
"""

def get_names(fn):
    with open(fn,'r') as f :        
        return [ n.replace('"','') for n in f.read().split(',') ]

ns = sorted(get_names("pe22_names.txt"))                 

def abc_value( name ) :
    return sum( ord(n.upper()) - ord('A') + 1 for n in name )

assert abc_value('COLIN') == 53

s = sum( (i+1) * abc_value(n) for i,n in enumerate(ns) )


assert s == 871198282
