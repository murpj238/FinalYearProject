source("Settings.R")
library(RODBC)

conn <- odbcDriverConnect(connection = DTSConnection)

sql <- "  SELECT [AverageDroneSpeed],[StartRunTime],COUNT(CollisionLocation_Id) 'NumCollisions' 
  FROM [DTS].[Statistics] 
  GROUP BY [AverageDroneSpeed], StartRunTime 
  HAVING (StartRunTime = '2017-05-24 19:41:47.000' or StartRunTime = '2017-05-24 23:25:28.000'
  or StartRunTime = '2017-05-25 09:02:22.000' or StartRunTime = '2017-05-25 10:42:53.000') and AverageDroneSpeed != 0"
results <- sqlQuery(conn, sql)
png('C:/Users/Jack/Documents/Fourth Year/Fourth Year Project/OptimalCollision.png', width = 1500, height = 1000)
plot(results$NumCollisions ~ results$AverageDroneSpeed, type = 'l', xlab = 'Speed', ylab = 'Number of Collisions', main = 'Optimal Speed')
dev.off()
close(conn)

conn <- odbcDriverConnect(connection = DTSConnection)

resultOne <- sqlQuery(conn,"SELECT TOP 1 RunTime,StartRunTime,AverageDroneSpeed
  FROM [DTS].[Statistics]
  WHERE StartRunTime = '2017-05-24 19:41:47.000' and RunTime < (SELECT TOP 1 RunTime FROM [DTS].[Statistics] WHERE (StartRunTime = '2017-05-24 19:41:47.000'
   and CollisionLocation_Id IS NOT NULL))
  ORDER BY RunTime DESC")

close(conn)

conn <- odbcDriverConnect(connection = DTSConnection)


resultTwo <- sqlQuery(conn,"SELECT TOP 1 RunTime,StartRunTime,AverageDroneSpeed
  FROM [DTS].[Statistics]
  WHERE StartRunTime = '2017-05-24 23:25:28.000' and RunTime < (SELECT TOP 1 RunTime FROM [DTS].[Statistics] WHERE (StartRunTime = '2017-05-24 23:25:28.000'
   and CollisionLocation_Id IS NOT NULL))
  ORDER BY RunTime DESC")

close(conn)

conn <- odbcDriverConnect(connection = DTSConnection)


resultThree <- sqlQuery(conn,"SELECT TOP 1 RunTime,StartRunTime,AverageDroneSpeed
  FROM [DTS].[Statistics]
  WHERE StartRunTime = '2017-05-25 09:02:22.000' and RunTime < (SELECT TOP 1 RunTime FROM [DTS].[Statistics] WHERE (StartRunTime = '2017-05-25 09:02:22.000'
   and CollisionLocation_Id IS NOT NULL))
  ORDER BY RunTime DESC")

close(conn)

conn <- odbcDriverConnect(connection = DTSConnection)


resultFour <- sqlQuery(conn,"SELECT TOP 1 RunTime,StartRunTime,AverageDroneSpeed
  FROM [DTS].[Statistics]
  WHERE StartRunTime = '2017-05-25 10:42:53.000' and RunTime < (SELECT TOP 1 RunTime FROM [DTS].[Statistics] WHERE (StartRunTime = '2017-05-25 10:42:53.000'
   and CollisionLocation_Id IS NOT NULL))
  ORDER BY RunTime DESC")

close(conn)

conn <- odbcDriverConnect(connection = DTSConnection)


temp <- rbind(resultOne, resultTwo)
temp1 <- rbind(temp, resultThree)
results <- rbind(temp1, resultFour)

png('C:/Users/Jack/Documents/Fourth Year/Fourth Year Project/SmoothestRun.png', width = 1500, height = 1000)
plot(results$RunTime ~ results$AverageDroneSpeed ,type = 'l', xlab = 'Run Time in Seconds', ylab = 'Number of Drones', main = 'Time before Collision')
dev.off()

close(conn)
