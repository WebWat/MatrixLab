open MatrixF



let a = Matrix (array2D [[3; 4]; [1; 2]])
let b = Matrix (array2D [[-1; 0]; [2; 2]])

let c = (2 * a + b.Transpose()).Transpose() + (-3) * b * a

printfn "%s" (c.ToString())


