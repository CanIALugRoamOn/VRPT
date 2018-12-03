# Attention Model
You can find a listeners attention curve and some tips on [keeping the attention] (https://speakingaboutpresenting.com/content/7-ways-audience-attention-presentation/) in general or how to improve [scientific presentations] (http://www.scientificleaders.com/presentations). Other parameters that can influence the attention can be found [here] (https://airandspace.si.edu/rfp/exhibitions/files/j1-exhibition-guidelines/3/An%20Attention-Value%20Model%20of%20Museum%20Visitors.pdf). Note that the document talks about museum visitors. However, crossovers in the content are likely because it is still about the attention of an audience.
First ideas for the VRPT are:
- Process the feedback messages giving each of them a weight that influences the listeners attention. E.g. speaking too soft or slow facilitate the audience to be distracted.
- Use the general attention curves from the links above. You can e.g. track intermediate conclusions.
- Simulation of a noisy environment 

## Curve Modelling
The curves can be approximately modeled by the addition of to normal distributed random variables.
Let X &#126 N(0,1) and Y &#126 N(3000,1) where 3000 are in seconds, thus 50 minutes. Let A=X+Y. Note that we are not interested in the area beneath the curve but at the value at a point in time t.