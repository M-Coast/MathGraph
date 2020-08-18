# Math_Solution
Math_Solution, To solve LinearEquations, LeastSquarePlaneFitting, FlatnessCalculation, etc.






LinearEquations Description

Equations:  Ax=B

Matrix:     [A|B]

Result:     []


LinearEquations 测试

Input
Matrix 3X4
9.00	-1.00	-1.00	7.00	
-1.00	10.00	-1.00	8.00	
-1.00	-1.00	15.00	13.00	

Result:
						1.00	
						1.00	
						1.00	


Processing:

Input Matrix
Matrix 3X4
9.00	-1.00	-1.00	7.00	
-1.00	10.00	-1.00	8.00	
-1.00	-1.00	15.00	13.00	

Forward Elimination Round: 1
Matrix 3X4
9.00	-1.00	-1.00	7.00	
0.00	9.89	-1.11	8.78	
0.00	-1.11	14.89	13.78	

Forward Elimination Round: 2
Matrix 3X4
9.00	-1.00	-1.00	7.00	
0.00	9.89	-1.11	8.78	
0.00	0.00	14.76	14.76	

Normalized
Matrix 3X4
1.00	-0.11	-0.11	0.78	
0.00	1.00	-0.11	0.89	
0.00	0.00	1.00	1.00	

Backward Elimination Round: 1
Matrix 3X4
1.00	-0.11	0.00	0.89	
0.00	1.00	0.00	1.00	
0.00	0.00	1.00	1.00	

Backward Elimination Round: 2
Matrix 3X4
1.00	0.00	0.00	1.00	
0.00	1.00	0.00	1.00	
0.00	0.00	1.00	1.00	

Processed Matrix
Matrix 3X4
1.00	0.00	0.00	1.00	
0.00	1.00	0.00	1.00	
0.00	0.00	1.00	1.00	

Result
1.00
1.00
1.00
