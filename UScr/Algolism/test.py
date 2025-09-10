Var = [29,10,23,1,4]

temp=0

for i in range(len(Var)):
    for j in range(len(Var[1:])):
        if Var[i] < Var[j]:
            temp = Var[i] 
            Var[i] = Var[j]
            Var[j] = temp
print(Var)
