# Blog: Drone Traffic Simulation

**Jack Murphy**

## First Installment of Fourth Year Project Blog

The work I have done so far on my fourth year project has mainly been research and information gathering. 
I have emailed various lectures with an idea that was based on collision detection/avoidance. 
This has had a mixed response with many thinking that there would not be enough work involved.

From this idea one lecturer pointed me in the direction of drones, this added complexity to the 
idea as it involves navigation in 3D. The challenge and complexity of this idea appealed to me. 
Meeting with this lecturer he suggested I learn as much as I can about drones and the regulation 
of drones to try and nail down a specific project idea. So my task now is to find some area 
where drone regulation falls short. The first site I plan to look at is https://www.iaa.ie/general-aviation/drones/drone-regulations-guidance 
which seems to have plenty of information on the region of drones.

After becoming familiar with the regulations for drones in Ireland I will check European legislation
and then US legislation. I will write more when I have made some ground with this.

-Jack Murphy

## Drone research information

After doing a significant amount of research about drones I found some information which could
be beneficial. I read the guidelines set out by the IAA. The European guidelines are for drones
over 150kg and anything under that is under the authority of the IAA.

The main regulations on drone usage for drones over 1kg

* Does not allow use of cameras on drones
* Does not allow for use in urban areas
* Must stay within 300m of the control station
* Must stay within view of the  operator
* Must not fly near airport/aerodrome
* Over a group of people. e.g. more than 10

The problem with these restrictive rules is that many people are not registering their drones 
or are just ignoring the rules. The only way to avoid these rules is if you apply for special
operations permission which requires undergoing training and getting a qualification. If people
are going to ignore these rules can we get rid of the safety concerns with these.

* The main danger with flying in an urban area is that there are too many obstacles. If the drone can navigate around and avoid these obstacles there is no need for this regulation. Obstacles would include, street lights, traffic lights, buildings, electricity/phone lines, trees etc.
    * Things that are traceable, traffic lights, buildings, electricity and phone lines
    * Trees are not traceable so they would need to be detected on the fly
* The use of cameras is restricted for privacy reasons, if this is the case camera feeds could be programmed to pixelate peoples faces.
* The danger of a drone being out of view of/ too far away from the controller is that it could crash without the knowledge of the owner
    * Could be solved by introducing gps tracking/camera on the drone
* To solve the problem of flying near airports the drone could be programmed to not allow flight near an airport, i.e. to navigate around a restricted zone

These regulations all present interesting challenges. I think the area where the regulations 
fall short is in the tracking of the drones that are registered. There is no way to tell if 
a drone has flown in a restricted area or not. If you had this information you could program 
the drone now avoid these areas. This would be an interesting direction to take the project in.

-Jack

## My ideas for a project

From the research I have done so far I have identified various gaps in the legislation related 
to drones. I have come up with some ideas for projects that would make up for these gaps.

* Create a system that tracks the GPS coordinates of the drone while keeping up to date information on the restricted flying zones. If the drone is piloted into a restricted area the system will kick in and fly the drone out of the restricted area.
* Create a system that will detect a low battery on the drone, calculate if there is enough battery to make it back to the control unit . If it can't make it to the control unit it will land as close and in the safest position as possible. Once it has chosen the best landing position it will send the location back to the control unit.
* Create a system that shows a map of the current drones position, it then highlights the areas that the drone can fly based on restricted areas and the battery life of the drone. If the drone is outside the bounds of the highlighted area that means its battery is not enough to bring it back to the control unit. The system will alert the user if they are flying to close to the bounds of the map and if they try to pass the system will kick in and fly back in.

Each of these ideas could be combined and extended to avoid other obstacles like electricity 
lines, etc. Those are the ideas I have so far, I will add more as they come up

-Jack

## Research for final idea

I have now settled on the idea of a 3D model of a traffic management system for drones. This 
will have the automated drones flying from one area of the city to another. They will follow the 
streets so they can use the buildings as shelter. The traffic should run smoothly and the drones
will take advantage of the height of the buildings and the width of the street. The drones 
must be able to track their locations and feed them back to a central system. This allows the drones
to know their own location as well the location of other drones. I plan to use the Unity game 
engine to run the simulation of the drones. I will add a model of a 1 square kilometre area of a 
city to the game engine. I will then create my drone object, potentially in C# and run them 
through the simulation. I will run the max number of drones as possible to try and cause a crash.

**Some websites that could be of interest:**

https://cadmapper.com/#metro

http://www.sketchup.com/#find-3d-models-anchor

-Jack
## Improving My Idea

After attending my project proposal the lecturers and even some other students gave me some 
great feedback. The main feedback I was given from the lecturers was that creating just an automation 
of the traffic flow of drones would be too simple. So I have decided, with the help of my supervisor,
to add events that will give the project an extra level of complexity.

**These events include:**
* A single drone not working
* A crash
* If one drone is low on battery and must return to the controller unit
* For all of these events, I will need to program how the other drones act in this scenerio
* I will also take into account the traffic jam threshold

From speaking with my supervisor I was directed to an [article](http://www.sciencemag.org/news/2008/03/traffic-jams-happen-get-used-it).
This article details a study performed by a group of Japanese researchers on traffic jams. They 
concluded in their study that after a certain threshold has been crossed due to variations in speed
etc a traffic jam will inevitably occur. So when I am creating my traffic flow I will need to take
this into consideration.

-Jack
## Activity Since My Last Entry

Since my project proposal I have gone about setting up the tools I will need to implement this 
project. I have installed my IDE with the necessary extensions that I will need. I have created
my GitLab repository and cloned the repository onto my local machine using Tortoise Git. I have 
also migrated my blogs from wordpress where I started them to the blog section of the GitLab repository.

-Jack
## Progress with Functional Spec Document

I have now begun working on my functional specification for this project. The functional specification
outlines the full details of the project and how I will implement it. Having done some research on
how Unity game engine works I have now decided how I will use it in my project. My job now will be to
do some further study on how to create a project using Unity. So I have found that the overall solution
and model is made using unity and the objects that I use in the simulation and how they interact with
the environment will be coded in C#. From this C# code I will work with object from a database.

When implementing this project I would like to make use of some of the things I learned while working on 
INTRA. These include design patterns, in particular the Repository pattern and I would like to develop
my code with a test first approach. This will greatly reduce the chance of my solution incurring technical
debt. This week I hope to learn more about using Unity and also continue with the progress I have made on my functional
spec document.

-Jack

## Progress Update

Since my last blog entry, I have been familiarising myself with Unity and how to create projects and work with
Objects in the Game Engine. I have been following a number of tutorials on the Unity website as well as
on YouTube

- [Unity 5 Tutorial: Basics](https://www.youtube.com/watch?v=Ep0rlBQRcVc&t=22s)
- [Unity Tutorial: The Basics (For Beginners)](https://www.youtube.com/watch?v=QUCEcAp3h28)
- [To learn about the interface I used these videos](https://unity3d.com/learn/tutorials/topics/interface-essentials)

As well as following these tutorials, I spoke with a number of people who have used Unity before. They were
able to give me great insight into how to use it and the benefits of using it. After following the tutorials
I created a test project to familiarise myself with the Unity interface and how to create a basic project. 

![Test Project Screenshot 1](./images/TestProjScreenshot1.png "Test Project Screenshot 1")
![Test Project Screenshot 2](./images/TestProjScreenshot2.png "Test Project Screenshot 2")
![Test Project Screenshot 3](./images/TestProjScreenshot3.png "Test Project Screenshot 3")
![Test Project Screenshot 4](./images/TestProjScreenshot4.png "Test Project Screenshot 4")

Over the next week I hope to continue with my Functional Spec and hopefully finish it next weekend.
I also plan on attempting to import a model of Paris. Once this has successfully been imported I will
begin programming objects into the map.

-Jack

## Getting Started with the Implementation

Today I will begin coding the project. By the end of this week
I hope to have a fully functioning Data Access Layer, created in a code first approach. This approach
involves creating C# classes which will represent database tables and the class variables defined will 
represent the table attributes. I will use this approach as it is a much more fluent way of working
with database objects. It allows you to have greater control over your database tables as other 
implementations in Entity Framework tend to create a lot of useless code which is hard to understand
and not very extensible.

The benefits of this approach are outlined in more detail 
[here](http://www.itworld.com/article/2700195/development/3-reasons-to-use-code-first-design-with-entity-framework.html)

Once I have my Data Access Layer created this will be the backend of my system which will store all
the information about the drones. After this has been created I can create the environment and world
that the drones will occupy.

-Jack

## Creating the world

I now have a basic backend implemented, this consists of a Data Access Layer (DAL) and a series of tables on
a local SQL Server. I now need to create the world which will hold all the objects. This stage will require a 
lot of time and research. This is the case as I have no knowledge 3D modelling, researching the best way to do this
for my purposes the world can be made in unity. The world does not need to be overly detailed as the focus of the project
is on the output and not the user interface.

If I have a set of statistics that I am happy with, then I can possibly make the simulation more appealing visually
for the purposes of a demonstration. I estimate that this modelling will take between 1 and 2 weeks.

-Jack

## Meeting with Supervisor
Since my last blog I have been working on creating a model of a visual city.I have spent a lot of
hours researching this and designing this. After meeting with my supervisor I have agreed that
a basic city will suit the purposes of what I need for the moment. The main aim for me is to create
a working simulation of drones flying through the air. One or two to start with and a monitor of the time they are running.
I will increase the speed of them until a collision occurs. When this has completed I can graphically represent
the statistics to show the correlation between speed and time.

This will be my task for this week. I aim to have this up and running by next weekend. After I have this
I can increase the number of drones and possibly make the navigation more complex.

-Jack

## Getting Things Moving

Following my last blog, I have made a number of discoveries about Unity and working within it. I have
now added a drone into the world along with a basic moving camera (http://coffeebreakcodes.com/move-zoom-and-rotate-camera-unity3d/) to follow the drones movement. The drone now
follows a set path  through the world going from a start location to a finish location. 

My next task is to randomly generate the drone in the world. Once I have this done I can randomly generate more
drones. I plan to run the drones at two different speeds and try and cause a crash. Once this is done I 
will save the collision information in the server. I also hope to refactor out the logic to a separate class library
Instead of running the logic in the Assets files. From this new class library I can make references to the DAL. I hope
to implement these features over the coming weekend. 

-Jack

## Change of Game Engine

Since my last blog, I began the process of separating the classes I have into a business logic
class library. After beginning this process it became apparent that this could not be done using
Unity.

#### The Problem
The problem is in that Unity and inherits it's scripting capabilities from a framework called Mono.
Mono is the equivalent of .Net v2.0 (https://forum.unity3d.com/threads/unity-5-current-net-version-and-futur.368385/). 
This is well below the version of my class libraries. Therefore I cannot connect to my Logic layer and subsequent data
access layer.

#### The Solution
To fix this problem, I am going to change my game engine from Unity to Xenko. Xenko is a new game engine
software that uses the latest version of .Net and is in direct competition with Unity.

#### The Advantages
* Latest version of .Net
* Unity .Net version from 2006 and would take a lot of work to implement in this version
* Layout is very similar to Unity
* Class libraries containing Data Access, Domain and Drone Logic already implemented
* Would just require implementing the world
* More control over the Game Logic and implementaion

#### Disadvantages
* Will take time to become familiar with the new editor
* May have to change some logic in the Drone Logic layer depending on Xenko syntax
* Not as much support and documentation online
* In the late stages of development but still in development none the less

Weighing up the advantages and disadvantages it seems that the advantages far outweigh the disadvantages
this is why my solution is to change to the Xenko game engine to complete my project. In the next week
I hope to have the new world create and the Drone Logic layer slotted into it. Then I can start to add more
drones in to try and cause a crash.

-Jack

## Progress Update

Since deciding to change the game engine for my simulation I have rebuilt the world and am now working
on the navigation. In order to add drones to the world along with allowing them to navigate to a chosen point
I need to set up a data access layer that will query the database for the location and drone objects.

I have set up this data access layer correctly using the Repository and Unit of Work design patterns. The reason
I chose these patterns was due to the fact that abstracts the query logic from entity framework. This allows the project to be less reliant on
entity framework for its querying capabilities. So if I need to update the entity framework version or decided to go with
a different method of data access, it is not a major change. It is also enables unit tests to be performed with much less 
complexity which ties in to the test-driven approach I am taking with my project. 

Now that I have this set up my next task is to fill the database. Once the database has been populated I will
begin programming the logic to randomly add drone and navigate through the world.

-Jack

## Progress and Difficulties

Since my last update I have populated the tables in my database with navigation
points which will be used by the drones to navigate the map. I have also added in
logic to randomly generate drones in a normally distributed pattern of arrivals. I was 
able to implement this to work with a specified number of drones.

As I have outlined before I am undertaking the project with a test-driven approach. So
in light of this I created unit tests to verify my logic which checks if a navigation point
exists and unit tests to test the normally distributed list of times as well as the logic
to check if it is time to generate a drone.

Since the last update I also encountered some difficulties. One of the major difficulties
which I encountered was for adding in my outside class logic to the game library. This came
as a result of the game library implementing the Portable Class Library (PCL) feature of .Net. This 
feature ultimately allows the library to be implemented on multiple platforms similar to the Java
runtime. However, my external libraries are generic class libraries which only run on Windows. I tried
a number of fixes including converting all my class libraries to PCL's. This did not work as I needed to 
use Entity Framework and some of its features for my data access and PCL's have not yet got the support for
the Entity Framework features I needed. In the end I turned to the Xenko Game Engine forum to discover
if it was possible to change the Game class from a PCL to a regular class library, as I only need to run the 
project on Windows. My forum post can be seen here https://forums.xenko.com/t/can-anyone-tell-me-is-possible-to-change-the-game-pcl/917

Another problem I encountered was add a drone via code and loading the model, however this problem was solved much less time.
In my next blog I will discuss my meeting with my supervisor and further progress I have made.

-Jack

## Meeting with Supervisor

After meeting with my supervisor on Tuesday (29/03/2017), I have further direction on
where to focus my efforts and where the project is going. As of now, I have a table and 
domain object setup to hold my statistics. However, before the meeting I was unsure of what kind
of data I should be collecting. Some of the I knew such as drone count and collision time/location, but 
other data metrics like average speed of the drones at the time of data collection, average distance travelled, etc.

I will now be able to add these to my statistics table for graphical display. I have agreed with my supervisor
that by our next meeting I will have this table filled with data and possibly displayed graphically. My next step
is to implement the navigation logic. I hope to implement this with recursion using a goal based algorithm. Before implementing
this I must research possible optimal solutions as this is the basis for the entire simulation. The navigation will need to
be efficient as their may potentially be thousands of drones in the system which need all need to adjust there movement in a single tick.

Once the navigation is implemented I will be able to combine all elements and possibly create integration test to
validate he fitting together of the different elements.

-Jack

## Implementing Navigation

I have now begun the process of implementing navigation. This is the backbone and possibly the most complex element of the simulation.
The main idea is that a drone will have a start point and a target point, when it is created an algorithm will generate the shortest route 
from the start point to the target point. However, it is constrained by the fact that the drones cannot pass through buildings or move
diagonally.

I have been looking at a number of solution to this problem, I have researched the travelling salesman problem as the issue is similar to that.
As well as this I have researched various algorithms for the shortest path and goal oriented algorithms. The solutions and various papers I found
are as follows:

- [The travelling salesman problem](http://www.csd.uoc.gr/~hy583/papers/ch11.pdf)
- [Maze Router problem using Lee's Algorithm](http://users.eecs.northwestern.edu/~haizhou/357/lec6.pdf)
- Lee, C.Y., 1961. An algorithm for path connections and its applications. IRE transactions on electronic computers, (3), pp.346-365.

These works provide interesting solutions to various similar problems, however from my perspective it is hard to see how these could be applied to
improve my navigation algorithm. As it stands I am implementing a form of the Nearest Neighbour Algorithm known as Linear Search. This is a simple solution
but sometimes the simplest solution is the optimal solution. I will continue to implement this but will also meet with lecturer Dr. Liam Tuohey who may be
to provide insight into optimal solutions as his area of research is around mathmatical programming and simulation. I hope to wrap up the navigation algorithm
in the next week and once that has been implemented the other areas should fall into place.

-Jack

## Moving forward with Navigation

Since my last update I met with lecturer Dr. Liam Tuohey, with him I discussed the problem of navigation in relation to my simulation.
It is not a straight forward approach where a nearest neighbour or travelling salesman problem could be used. After much discussion we 
came across the Manhattan Distance. This came about as a result of discussing the current algorithm I have in place and that I am  using
Euclydian distance. It seems this problem can be categorised as a Minimum Manhattan Distance problem. This manhattan distance is the distance
between two points where the distance is the sum of the distances between all points between the start and the target.

Liam was able to provide me with two useful paper's that outline the Minimum Manhattan Distance network problem
these papers are as follows:

- Chepoi, V., Nouioua, K. and Vaxes, Y., 2008. A rounding algorithm for approximating minimum Manhattan networks. Theoretical Computer Science, 390(1), pp.56-69.
- Benkert, M., Wolff, A., Widmann, F. and Shirabe, T., 2006. The minimum Manhattan network problem: Approximations and exact solutions. Computational Geometry, 35(3), pp.188-208.

From what I could gather from both of these papers there is no set or accepted solution for finding the Minimum Manahattan Network (MMN), i.e. the 
shortest distance between the start and the target. It seems there is only methods to approximate the MMN. The first paper (Chepoi, Nouioua and Vaxes 2008) seems to 
provide the best solution of the two, it has a  rounding factor 2-approximation algorithm using a linear programming formula. This paper brings in the idea of an efficient
point, and the set of all the 'efficient' points makes up the Pareto Envelope. It seems that I must use this apporach when creating my algorithm to find the optimal route
for the drones. The one drawback is that this approach is based on points in a 2D plane, but I am sure that it could be scaled to work with 3D points
however this may increase the complexity which is currently O(n log n). I plan to work on this and have it completed by the end of the weekend.
I have also made the decision to pre-calculate and store the the neighbouring point for each point in the database along with the distance to the neighbouring point.
I believe this will greatly reduce the run time of my navigation algorithm.

-Jack

## Supervisor Meeting

Since my last update I have met with my supervisor on Monday (24/04/2017). After meeting we established that the
navigation should be in the form of one way streets rather than shortest distance. This method will allow for more control
over the drones and less chance of a crash occurring. One way streets will also add more complexity, as some points may not be
reachable on the same 2D plane therefore it will require going up a level or down a level to reach some points.

To do this I will rewrite the Navigation Points table to take into account the different streets and thus the direction to be
travelled on the streets. I will also rewrite the navigation method to allow for the new streets system. I will need to extend
the model in the game engine will also need to be extended to create the extended map into more of a grid system. I hope to implement this new system by the end of the weekend.

-Jack

## Progress Update

Since my last blog I have been working on implementing navigation. I since update the world increasing the number of navigation
points to 144. This is laid out in the form of 6 streets by 6 streets. Each street has a point on the left and a point on the right.
I also updates the logic to take into account the street the points are on and thus what direction they can travel. This made the navigation
significantly more complex. I have been working on generating the list of navigation point since my last update. I spent a long time
trying different methods, using various conditions and even drawing out grids on paper to visualise the paths. However, I was still unable to 
come up with a solution, this gave me the idea of listing out all the possible conditions in a table format as follows:

[Condition Table Here](./Files/ConditionsTable.xlsx)

This gave me the idea for my algorithm to determine which point to choose, from this I wrote out all the conditions in pseudocode
and testing the hypothesis for which point to choose using a diagram of the grid. Then I refactored this pseudocode into more efficient
method. This is method I have in place, a number of bugs appeared in this however I was able to determine and fix the bugs by logging all the start and
end points for the route generation method. The the table with the log can be seen here:

[Navigation Log File](./Files/RandomNavigationPoints.csv)

I also met with my supervisor on Wednesday (10/05/2017) we discussed my progress with the project and the plan for the submission
and for the demonstration. My next task is to implement the navigation in the simulation. This will allow me to run the simulation and 
gather the statistic and generate graphical displays with R.

-Jack
