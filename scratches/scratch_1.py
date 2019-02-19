import numpy as np
import pandas as pd
import matplotlib.pyplot as plt
import matplotlib.gridspec as gridspec
# loading the file
file = 'patientData.csv'
data=pd.read_csv(file)
#import pweave
print(data.head())
print()
#x=[]
y=[]
z=[]

x=data.iloc[:,0].values
y=data.iloc[:,1].values
z=data.iloc[:,2].values

print(type(x))
'''
t=range(0,100000)
for i in t:
    #print(i)
    x.append(np.random.rand()-.5)
    y.append(np.random.rand()-.5)
    z.append(np.random.rand()-.5)
    #print(i)
#print(z)
#
'''
nbins=30
Xr=np.fft.fft(x,nbins)
X=abs(Xr[1:round(len(Xr)/2)])
Yr=np.fft.fft(y,nbins)
Y=abs(Yr[1:round(len(Yr)/2)])
Zr=np.fft.fft(z,nbins)
Z=abs(Zr[1:round(len(Zr)/2)])

x2=x-x.mean()
y2=y-y.mean()
z2=z-z.mean()


fig1 = plt.figure()
#print(type(fig))
tt=np.linspace(0,15,len(X))
plt.plot(tt,X,tt,Y,tt,Z,alpha=0.6)
plt.xlabel('Frequency (Hz)')
plt.ylabel('Amplitude')
plt.title('Frequency Response')
plt.legend(('X-axis', 'Y-axis', 'Z-axis'),loc='upper right')
plt.show()
fig1.savefig('plotF.png')
fig1.savefig('plotF.pdf')

fig2 = plt.figure()
score=int((1-(.75*(x2.std()+y2.std()+z2.std())/.7))*100)
gs = gridspec.GridSpec(1, 2, width_ratios=[4,1])
print(gs)
ax1 = plt.subplot(gs[0])
tt2=np.linspace(0,len(x2)/50,len(x2))
plt.plot(tt2,x2,tt2,y2,tt2,z2,alpha=0.6)
plt.xlabel('Time (s)')
plt.ylabel('Movement')
plt.title('Movement Analysis')
plt.legend(('X-axis', 'Y-axis', 'Z-axis'),loc='upper right')
ax2 = plt.subplot(gs[1])
plt.bar(['Higher is better'],score,alpha=0.6,color=['C3'])
plt.ylim((0,100))
plt.title('Health Score: '+str(score))
plt.show()
fig2.savefig('plotT.png')
fig2.savefig('plotT.pdf')

# stats

stats2show=[x2.std(), y2.std(), z2.std()]
fig3 = plt.figure()
#tt2=np.linspace(0,len(x2)/50,len(x2))
plt.bar(['X','Y','Z'],stats2show,alpha=0.6,color=['C0','C1','C2'])

plt.xlabel('Axis')
plt.ylabel('Tremor')
plt.title('Tremor values')
#plt.legend(('X-axis', 'Y-axis', 'Z-axis'),loc='upper right')
plt.show()
fig3.savefig('plotS.png')
fig3.savefig('plotS.pdf')

print('Analysis Completed!')

# Scores

## To create the PDF report:
import time
from reportlab.lib.enums import TA_JUSTIFY
from reportlab.lib.pagesizes import letter
from reportlab.platypus import SimpleDocTemplate, Paragraph, Spacer, Image
from reportlab.lib.styles import getSampleStyleSheet, ParagraphStyle
from reportlab.lib.units import inch

doc = SimpleDocTemplate("form_letter.pdf",pagesize=letter,
                        rightMargin=72,leftMargin=72,
                        topMargin=72,bottomMargin=18)
Story=[]
logo = "plot.png"
magName = "Pythonista"
issueNum = 12
subPrice = "99.00"
limitedDate = "03/05/2010"
freeGift = "tin foil hat"

formatted_time = time.ctime()
full_name = "Mike Driscoll"
address_parts = ["411 State St.", "Marshalltown, IA 50158"]

im = Image(logo, 2*inch, 2*inch)
Story.append(im)

styles=getSampleStyleSheet()
styles.add(ParagraphStyle(name='Justify', alignment=TA_JUSTIFY))
ptext = '<font size=12>%s</font>' % formatted_time

Story.append(Paragraph(ptext, styles["Normal"]))
Story.append(Spacer(1, 12))

# Create return address
ptext = '<font size=12>%s</font>' % full_name
Story.append(Paragraph(ptext, styles["Normal"]))
for part in address_parts:
    ptext = '<font size=12>%s</font>' % part.strip()
    Story.append(Paragraph(ptext, styles["Normal"]))

Story.append(Spacer(1, 12))
ptext = '<font size=12>Dear %s:</font>' % full_name.split()[0].strip()
Story.append(Paragraph(ptext, styles["Normal"]))
Story.append(Spacer(1, 12))

ptext = '<font size=12>We would like to welcome you to our subscriber base for %s Magazine! \
        You will receive %s issues at the excellent introductory price of $%s. Please respond by\
        %s to start receiving your subscription and get the following free gift: %s.</font>' % (magName,
                                                                                                issueNum,
                                                                                                subPrice,
                                                                                                limitedDate,
                                                                                                freeGift)
Story.append(Paragraph(ptext, styles["Justify"]))
Story.append(Spacer(1, 12))


ptext = '<font size=12>Thank you very much and we look forward to serving you.</font>'
Story.append(Paragraph(ptext, styles["Justify"]))
Story.append(Spacer(1, 12))
ptext = '<font size=12>Sincerely,</font>'
Story.append(Paragraph(ptext, styles["Normal"]))
Story.append(Spacer(1, 48))
ptext = '<font size=12>Ima Sucker</font>'
Story.append(Paragraph(ptext, styles["Normal"]))
Story.append(Spacer(1, 12))
doc.build(Story)
print('Report Generated!')
