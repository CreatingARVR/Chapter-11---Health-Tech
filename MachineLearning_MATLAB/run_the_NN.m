load('matlab.mat')  % loads the NN structure
samples_size=35000; % define sample size here	
in=zeros(3,samples_size);  % features
out=zeros(1,samples_size); % labels (regression values)
nntool                     % running the Neural Network toolbox