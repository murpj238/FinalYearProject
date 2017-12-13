# Drone Traffic Simulation

My idea is to create a system that simulates the traffic of drones in a cityscape. 
The simulation will be a 3D Model of a city with drones flying through the air. 
The drones will fly between a certain heights using the cover of the building to 
protect them from the elements. The idea is that I will successfully be able to 
create a simulation of a smooth traffic flow of drones, regardless of whatever external 
factors may occur. The system will also have to find a way to maximise the traffic 
threshold. The traffic threshold is the point at which there are so many vehicles 
travelling the same route that traffic begins to slow down.

The map will be divided into a series of cells. These cells will horizontally combine 
to create lanes of traffic.  As all drones may not be the same size the cells must 
be large enough to cater for the largest drone, and also allow extra room so that 
the drone can react in case of an unforeseen event. The cells will also stack upward 
to create multiple levels of traffic flow. Lanes may also be assigned to a drone based 
on the speed of the drone. For example, the fastest drones will travel in the top lane.

Each individual drone will have to know its own location on the map, i.e. what cell it 
is in. It must also know the location of the other drones. As mentioned previously the 
drones will be able to react to various events that might affect drones in their vicinity 
or themselves. These events include sudden loss of power, strong wind, a drone crash. 
Each of these events will no doubt cause a disruption in the flow of traffic so the 
unaffected drones will have to realise this and react accordingly. The affected drones will 
also have to react to in a safe manner so that they do not cause damage to property, 
other drones or more importantly pedestrians. My system will be designed to randomly 
execute events like theses to demonstrate that drones can deal with the situation with 
minimal disruption to traffic.

It is my view that a system like this will shape the future of legislation around drones. 
As it stands the laws controlling drone usage are very limiting. These stringent controls 
on the use of drones is negatively affecting the many beneficial uses of drones. These uses 
range from commercial use like package delivery to emergency response. The aim of my system 
is to create a model by which you can have hundreds or thousands of auto-piloted drones 
flying above the streets in a safe and continuous manner.

## Additional Resources

- Git [cheat sheet](https://gitlab.computing.dcu.ie/sblott/local-gitlab-documentation/blob/master/cheat-sheet.md)
- Gitlab [CI environment](https://gitlab.computing.dcu.ie/sblott/docker-ci-environment) and it's [available software](https://gitlab.computing.dcu.ie/sblott/docker-ci-environment/blob/master/Dockerfile)
- Example projects with CI configured:
   * [Python](https://gitlab.computing.dcu.ie/sblott/test-project-python)
   * [Java](https://gitlab.computing.dcu.ie/sblott/test-project-java)
   * [MySql](https://gitlab.computing.dcu.ie/sblott/test-project-mysql)
