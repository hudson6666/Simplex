# Simplex Algorithm

# Demo

First you need make the problem a normalized form.

Example:

> max z = 0.75x_4 - 20x_5 + 0.5x_6 - 6x_7  
> s.t.  
> x_1 + 0.25x_4 - 8x_5 - x_6 + 9x_7 = 0  
> x_2 + 0.5x_4 - 12x_5 - 0.5x_6 + 3x_7 = 0  
> x_3 + x_6 = 1  
> x_1 ... x_7 >= 0


Input it as follows,

> Input m, n: m refers count of x+1(for b), n refers count of equations  
`8 3`  
> Input c  
`0 0 0 0.75 -20 0.5 -6`  
> Input matrix  
`1 0 0 0.25 -8 -1 9 0`  
`0 1 0 0.5 -12 -0.5 3 0`  
`0 0 1 0 0 1 0 1`  
> Input xb  
`0 1 2`

xb are the base x variables at the beginning.

### Note: Everything starts from 0!

The program will pause at every loop, press any key to continue.

The output will be as follows,

```
Start looping
Z:
0 0 0 0.75 -20 0.5 -6
the:
0 0 -1
xb:
0 1 2
the:0:0
onRowChange:3:0
4 0 0 1 -32 -4 36 0
-2 1 0 0 4 1.5 -15 0
0 0 1 0 0 1 0 1
Z:
-3 0 0 0 4 3.5 -33
the:
-1 0 -1
xb:
3 1 2
the:1:0
onRowChange:4:1
-12 8 0 1 0 8 -84 0
-0.5 0.25 0 0 1 0.375 -3.75 0
0 0 1 0 0 1 0 1
Z:
-1 -1 0 0 0 2 -18
the:
0 0 1
xb:
3 4 2
the:2:1
onRowChange:5:2
-12 8 -8 1 0 0 -84 -8
-0.5 0.25 -0.375 0 1 0 -3.75 -0.375
0 0 1 0 0 1 0 1
Z:
-1 -1 -2 0 0 0 -18
-12 8 -8 1 0 0 -84 -8
-0.5 0.25 -0.375 0 1 0 -3.75 -0.375
0 0 1 0 0 1 0 1
End with:2
Z:
-1 -1 -2 0 0 0 -18
```

which indicates that the problem has an optimized solution `2`.
