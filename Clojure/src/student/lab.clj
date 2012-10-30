(ns student.lab)

(defn min-1 [x & xs]  ; [[x & xs ]] would destructure a vector argument
  (loop [mini x 
         [nx & rx] xs]
    ;(println [mini nx])
    (if (nil? nx) mini
      (recur (if (< nx mini) nx mini) rx))))

; (min-1 1 5 3 6 7 0 3)


(defn min-3 [& xs]
  (reduce #(if (< %1 %2) %1 %2) xs))



(defn zipm-1 [ks vs]
  (loop [m {}
         [k & mk] ks
         [v & mv] vs]
    (if k 
      (recur (assoc m k v) mk mv)
      m)))

; (zipm-1 [:a :b :c] [1 2 3])


(defn zipm-3 [ks vs]
  (reduce (fn [m [k v]] (assoc m k v)) {} 
          (map vector ks vs)))


(defn zipm-4 [ks vs]
  (apply hash-map (interleave ks vs)))


(defn zipm-5 [ks vs]
  (into {} (map vector ks vs)))



(defn minmax-1 [x & xs]
  (loop [mini x
         maxi x
         [i & more] xs]
    (if i
      (recur (if (< i mini) i mini)
             (if (> i maxi) i maxi)
             more)
      {:min mini :max maxi})))


(defn minmax-2 [x & xs]
  (reduce (fn [m i] (let [mini (:min m) maxi (:max m)]
                      (assoc m 
                           :min (if (< i mini) i mini)
                           :max (if (> i maxi) i maxi)))) 
          {:min x :max x} xs))


