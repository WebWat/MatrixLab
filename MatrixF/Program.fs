open MatrixF



let m1 = Matrix(20, 20, (fun i j -> i + j))
let m2 = Matrix(20, 20, (fun i j -> i * j))
let result = -m1

printfn "test"


