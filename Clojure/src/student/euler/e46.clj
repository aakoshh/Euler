(ns student.euler.e46)

(comment "
It was proposed by Christian Goldbach that every odd composite number can be written as the sum of a prime and twice a square.

9 = 7 + 212
15 = 7 + 222
21 = 3 + 232
25 = 7 + 232
27 = 19 + 222
33 = 31 + 212

It turns out that the conjecture was false.

What is the smallest odd composite that cannot be written as the sum of a prime and twice a square?")


(defn prime? [n]
  (let [r (int (Math/sqrt n))]
    (loop [d 2]
      (cond
        (= n 1) false
        (> d r) true
        (zero? (rem n d)) false
        :other (recur (inc d))))))

(defn primes []
  (filter prime? (iterate inc 2)))


(defn e46 []
  (let [ps (primes)
        cs (filter (complement prime?) (iterate #(+ % 2) 3))]
    (loop [[c & cs] cs]
      (if (empty? (for [p (take-while #(<= % (- c 2)) ps)
                        :when (zero? (rem (Math/sqrt (/ (- c p) 2)) 1))] p))
        c (recur cs)))))
        
    