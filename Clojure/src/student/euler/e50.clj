(ns student.euler.e50)

(comment "
The prime 41, can be written as the sum of six consecutive primes:

41 = 2 + 3 + 5 + 7 + 11 + 13
This is the longest sum of consecutive primes that adds to a prime below one-hundred.

The longest sum of consecutive primes below one-thousand that adds to a prime, contains 21 terms, and is equal to 953.

Which prime, below one-million, can be written as the sum of the most consecutive primes?")

; http://stackoverflow.com/questions/8064336/why-is-clojure-10-times-slower-than-python-for-the-equivalent-solution-of-euler

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


(defn cumulative-sum [s]
  (reduce 
    (fn [v, x] (conj v (+ (last v) x))) 
    [(first s)] 
    (rest s)))

(defn cumulative-sum-2 [s]
  (loop [[x & xs] s
         ss 0
         acc (transient [])]
    (if x      
      (let [ssx (+ ss x)]
        (recur xs ssx (conj! acc ssx)))
      (persistent! acc))))


(defn cumulative-sum-3 [s]
  (reduce 
    (fn [v, x] (conj v (+ (v (dec (count v))) x)))
    [(first s)] 
    (rest s)))


(defn longest-seq-under [n]
  "Longest prime seq with sum under n"
  (let [ps (vec (take-while #(< % n) (primes)))
        prime-set (set ps)
        cs (cumulative-sum ps)
        cnt (count ps)
        sub-sum (fn [i j] ; avoid summing up in each iteration             
                  (- (cs j) (get cs (dec i) 0)))
        [from to]
        (loop [i 0 ; from
               mf 0 ; max seq from
               mt 0]
          (let [min-j (+ i (- mt mf))]
            (if (or (>= min-j cnt) ; reached the end
                    (> (sub-sum i min-j) n)) ; only larger numbers follow: this is the key
              [mf mt] ; the longest sofar and no use going forward
              (let [[mfi mti] (reduce (fn [c x] ; select the longer range
                                        (if (> (- (x 1) (x 0)) 
                                               (- (c 1) (c 0))) x c))
                                    [mf mt]
                                    (for [j (range min-j cnt) ; at least m long
                                          :let [sij (sub-sum i j)] ; sum from i to j
                                          :while (< sij n)
                                          :when (prime-set sij)]
                                      [i j]))]
                (recur (inc i) mfi mti)))))]
    (subvec ps from (inc to))))


(defn longest-seq-under-test [n]
  "Longest prime seq with sum under n"
  (let [ps (vec (take-while #(< % n) (primes)))
        prime-set (set ps)
        cs (cumulative-sum ps)
        cnt (count ps)
        max-len (count (take-while #(< % n) cs))
        sub-sum (fn [i j] ; avoid summing up in each iteration   
                  (- (cs j) (get cs (dec i) 0)))]
        (loop [m max-len] ; longest first
          (if (not (zero? m))
            (let [[i j] (last
                          (for [i (range 0 (inc (- cnt m)))
                                :let [j (+ i (dec m))
                                      s (sub-sum i j)]
                                :while (< s n)
                                :when (prime-set s)]
                            [i j]))]
              (if (not (nil? j)) 
                (subvec ps i (inc j))
                (recur (dec m))))))))


(defn longest-seq-under-test2 [n]
  "Longest prime seq with sum under n"
  (let [ps (vec (take-while #(< % n) (primes))) ; prime numbers up to n
        prime-set (set ps)  ; set for testing of inclusion
        cs (cumulative-sum-2 ps)
        cnt (count ps)
        max-len (count (take-while #(< % n) cs)) ; cannot have longer sequences
        sub-sum (fn [i j] ; sum of primes between the i-th and j-th      
                  (- (cs j) (get cs (dec i) 0)))
        seq-with-len (fn [m] ; try m length prime sequences and return the first where the sum is prime
                       (loop [i 0] ; try with the lowest sum
                         (if (> i (- cnt m)) ; there are no more elements for and m length sequence
                           nil ; could not find any
                           (let [j (+ i (dec m)) ; fix length
                                 s (sub-sum i j)]
                             (if (>= s n) ; overshoot
                               nil
                               (if (prime-set s) ; sum is prime
                                 [i (inc j)] ; we just looked for the first
                                 (recur (inc i))))))))] ; shift window
        (loop [m max-len] ; try with the longest sequence
          (if (not (zero? m))
            (let [[i j] (seq-with-len m) ]
              (if j 
                (subvec ps i j)
                (recur (dec m))))))))
            
; (set! *warn-on-reflection* true)              
                             

(assert (= [2 3 5 7 11 13] (longest-seq-under 100)))

(let [s1000  (longest-seq-under 1000)]
  (assert (= 21 (count s1000)))
  (assert (= 953 (reduce + s1000))))

(defn e50 [] ; same algorithm runs fast in Python
  (reduce + (longest-seq-under 1000000)))
          


(defn make-seq-accumulator
  [[x & xs]]
  (map first (iterate
              (fn [[sum [s & more]]]
                [(+ sum s) more])
              [x xs])))

(def prime-sums
  (conj (make-seq-accumulator (primes)) 0))

(defn euler-50 [goal]
  (loop [c 1]
    (let [bots (reverse (take c prime-sums))
          tops (take c (reverse (take-while #(> goal (- % (last bots)))
                                            (rest prime-sums))))]
      ;(println bots tops) 
      (or (some #(when (prime? %) %)
                (map - tops bots))
          (recur (inc c))))))

; (time (euler-50 1000000))

; (def ps (vec (take-while #(< % 1000000) (primes))))
; cs (cumulative-sum ps)