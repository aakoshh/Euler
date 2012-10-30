(ns student.euler)

(defn divides? [x d] 
  "Check if x is divisible by d"
  (zero? (rem x d)))

(defn divides-any? [& divisors]
  "Returns predicate to check if a given number is divisible by any of the divisors"
  (fn [x] (some #(divides? x %) divisors)))

(defn problem-1 [upper]
  (reduce + (filter (divides-any? 3 5) (range upper))))


(problem-1 1000)

