(ns student.euler.e39
  (:use [clojure.contrib.math :only (expt floor)]))

; If p is the perimeter of a right angle triangle with integral length sides, {a,b,c}, there are exactly three solutions for p = 120.
; {20,48,52}, {24,45,51}, {30,40,50}
; For which value of p <= 1000, is the number of solutions maximised?


(defn right-triangle? [sides]
  "Is Pythagoras right? Expect sides in sorted order"
  (let [sorted sides]
    (= (+ (expt (first sorted) 2) (expt (second sorted) 2))
       (expt (last sorted) 2))))

; (every? right-triangle? '[ (20,48,52), (24,45,51), (30,40,50) ])

(defn triangle-property? [sides]
  "Check that a + b > c"
  (every? identity 
    (let [perimiter (reduce + sides)]
      (for [s sides]
        (> (- perimiter s) s)))))

(every? triangle-property? '[ (20,48,52), (24,45,51), (30,40,50) ])

(defn triangle-sides [perimiter]
  "Given a triangle perimiter, generate numbers that add up to it"
  (set (for [a (range 1 (floor (/ perimiter 3)))
             b (range (- (floor (/ perimiter 2)) a) (floor (/ perimiter 2)))
             c [(- perimiter a b)]
             :when (and (= perimiter (+ a b c))
                        (triangle-property? [a b c]))]
         (sort [a b c]))))

(defn right-triangle-sides [perimiter]
  "Given a triangle perimiter, generate numbers that add up to it"
  (set (for [a (range 1 (floor (/ perimiter 3)))
             b (range (- (floor (/ perimiter 2)) a) (floor (/ perimiter 2)))
             c [(- perimiter a b)]
             :when (and (= perimiter (+ a b c))
                        ;(triangle-property? [a b c])
                        (zero? (- (expt c 2) (expt a 2) (expt b 2))))]
         (sort [a b c]))))

; (triangle-sides 10)

(defn right-triangles [perimiter]
  "Return the list of all right triangles with a given perimiter"
  (filter right-triangle? (triangle-sides perimiter)))

; (right-triangles 120)

(defn max-right-triangles [maxp]
  (apply max-key 
         #(count (:triangles %)) 
         (for [p (range 3 (inc maxp))] 
           {:perimeter p :triangles (right-triangle-sides p)})))

(defn e39 [] 
  (max-right-triangles 1000))



(defn triplets [max-perimiter]
  "Given a max perimiter, return all Pythagoras triplets"
  (let [mp (int max-perimiter)]
  (for [a (range 1 (/ mp 2))
        b (range a (/ mp 2))
        c (range 1 (min (+ a b) (- mp a b)) )
        :when (and (>= mp (+ a b c))
                   (= (* c c) (+ (* a a) (* b b))))]
    [a b c])))




