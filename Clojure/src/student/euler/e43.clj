(ns student.euler.e43
  (:use [clojure.contrib.combinatorics :only (permutations)]))


(comment "
The number, 1406357289, is a 0 to 9 pandigital number because it is made up of each of the digits 0 to 9 in some order, 
but it also has a rather interesting sub-string divisibility property.

Let d1 be the 1st digit, d2 be the 2nd digit, and so on. In this way, we note the following:

    d2d3d4=406 is divisible by 2
    d3d4d5=063 is divisible by 3
    d4d5d6=635 is divisible by 5
    d5d6d7=357 is divisible by 7
    d6d7d8=572 is divisible by 11
    d7d8d9=728 is divisible by 13
    d8d9d10=289 is divisible by 17

Find the sum of all 0 to 9 pandigital numbers with this property.
")

(defn divides? [n d]
  (zero? (rem n d)))

(defn list-to-int [int-lst] ; returns number in reverse order
  (Long/parseLong (apply str int-lst)))

(defn pandigitals [n]
  "0 to n pandigital numbers"
  (map vec 
       (filter #(not= 0 (first %)) 
               (permutations (range (inc n))))))


(defn sub-num [n digit len]  
  (let [from (dec digit)]
    (list-to-int (subvec n from (+ from len)))))


(defn pan-divisible? [n]  
  (every? (fn [[i d]] (divides? i d))
          (map-indexed (fn [i d] 
                         [(sub-num n (+ 2 i) 3), d]) 
                       [2 3 5 7 11 13 17])))

(assert (pan-divisible? [1 4 0 6 3 5 7 2 8 9]))


(defn e43 []
  (reduce + (map list-to-int (filter pan-divisible? (pandigitals 9)))))

; (assert (= 16695334890 (e43)))



; dog slow

(defn lpad 
  ([n l p]
  (let [s (str n)]
    (str (apply str (repeat (- l (count s)) p)) s)))
  ([n l] (lpad n l 0)))

; create list of multiplicants and try to combine them
(defn multi [d]
  "Return numbers divisible by d up to 999"
  (map #(lpad % 3) 
       (take-while #(< % 1000)
                   (iterate #(+ d %) d))))

(defn no-dup? [s]
  (every? #(= 1 %) (vals (frequencies s))))


(defn prefix? [p s]
  "Can be combined to build a longer pandigital number"
  (or (empty? s) 
      (and (= (subs p 1 3) (subs s 0 2))
           (not (.contains s (str (first p)))))))

(defn add-prefix [p s]
  (if (empty? s) p 
    (str (first p) s)))

(defn add-missing [n]
  "add missing digit at the first position"
  (let [missing (apply disj (set (seq "0123456789")) (seq n))]
    (add-prefix missing n))) 

(defn combine-multi [m s]
  "Going in reverse order in multiplicant groups, create sequence"
  (if (empty? m) (list s) ; terminal sequence. return list to be used in for (otherwise characters will be iterated)
    (for [n (first m) :when (prefix? n s) 
          pds (combine-multi (rest m) (add-prefix n s))] ; list of terminal sequences
      pds))) ; by listing it separately here, it is combined with n, not nested


(defn pandivs []
  "generate multiplicative bags for each prime, combine them and filter pandigitals"
  (let [multiples (map #(filter no-dup? %)
                       (map multi [2 3 5 7 11 13 17]))]
    (filter #(and (no-dup? %)
                  (not= \0 (first %)))
            (map add-missing (combine-multi (reverse multiples) "")))))


(defn e43-fast []
  (reduce + (map read-string (pandivs))))


(assert (= 16695334890 (e43-fast)))


(defn digits->int
  [ds]
  (reduce #(+ (* 10 %1) %2) ds))
 

