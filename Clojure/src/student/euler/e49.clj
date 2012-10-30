(ns student.euler.e49
  (:use [clojure.contrib.combinatorics :only (permutations)]))

(comment "
The arithmetic sequence, 1487, 4817, 8147, in which each of the terms increases by 3330, 
is unusual in two ways: (i) each of the three terms are prime, and, 
(ii) each of the 4-digit numbers are permutations of one another.

There are no arithmetic sequences made up of three 1-, 2-, or 3-digit primes, exhibiting this property, 
but there is one other 4-digit increasing sequence.

What 12-digit number do you form by concatenating the three terms in this sequence?")


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

(defn perms [n]
  (map #(Integer/parseInt (apply str %)) (permutations (str n))))

(defn regulars [s]
  "get regular triplets from a list of numbers"
  (let [ss (apply sorted-set s)
        z #(+ %2 (- %2 %1))]
    (for [x ss
          y (filter #(> % x) ss)
          :when (ss (z x y))]
      [x y (z x y)])))
          

(defn regular-sequences []
  (let [ps (->> (primes)
             (drop-while #(< % 1000))
             (take-while #(< % 10000))
             (apply sorted-set))]
    (loop [rps ps
           res []]
      (if (empty? rps)
        res
        (let [p (first rps)
              vs (filter rps (perms p))
              s (regulars vs)]
          (if (seq s)
            (recur (apply disj rps vs) (conj res s))
            (recur (apply disj rps vs) res)))))))
        

(defn e49 []
  (apply str (first (second (regular-sequences)))))
  
  
(assert (= "296962999629" (e49)))