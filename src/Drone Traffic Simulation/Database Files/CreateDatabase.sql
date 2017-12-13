CREATE DATABASE [DroneTrafficSimulation]

GO

USE [DroneTrafficSimulation]
GO


CREATE TABLE [DTS].[Drones](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[IsLive] [bit] NOT NULL,
	[NavigationPoints_Id] [uniqueidentifier] NULL,
	[Scale_Id] [int] NOT NULL,
 CONSTRAINT [PK_Drones] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [DTS].[NavigationPoints](
	[Id] [uniqueidentifier] NOT NULL,
	[XPosition] [real] NOT NULL,
	[YPosition] [real] NOT NULL,
	[ZPosition] [real] NOT NULL,
	[IsCollisionPoint] [bit] NOT NULL,
	[XNeighbourId] [uniqueidentifier] NOT NULL,
	[XNeighbourDistance] [float] NULL,
	[ZNeighbourId] [uniqueidentifier] NOT NULL,
	[ZNeighbourDistance] [float] NULL,
	[Drone_Id] [int] NULL,
 CONSTRAINT [PK_NavigationPoints] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [DTS].[Scales](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[XSize] [real] NOT NULL,
	[YSize] [real] NOT NULL,
	[ZSize] [real] NOT NULL
) ON [PRIMARY]

GO

CREATE TABLE [DTS].[Statistics](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DroneCount] [int] NOT NULL,
	[CurrentTimeInRun] [datetime] NOT NULL,
	[StartRunTime] [datetime] NOT NULL,
	[RunTime] [int] NOT NULL,
	[AverageDistanceTravelled] [float] NOT NULL,
	[AverageDroneSpeed] [float] NOT NULL,
	[CollisionLocation_Id] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Statistics] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [DTS].[Streets](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IsHorizontal] [bit] NOT NULL,
	[XCoordinateOne] [real] NULL,
	[XCoordinateTwo] [real] NULL,
	[IsVertical] [bit] NOT NULL,
	[ZCoordinateOne] [real] NULL,
	[ZCoordinateTwo] [real] NULL,
	[Direction] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Street] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


