import numpy as np
import matplotlib.pyplot as plt
print()
z=[]
x=range(0,100000)
for i in x:
    #print(i)
    z.append(np.random.rand()-.5)
    #print(i)
#print(z)
plt.plot(z)
plt.show()

Z=np.fft.fft(z)

print(type(Z))

plt.plot(Z)
plt.show()

