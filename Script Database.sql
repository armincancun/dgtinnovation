
CREATE TABLE Driver ( 
	Id                   uniqueidentifier NOT NULL   ,
	DNI                  nvarchar(60) NOT NULL   ,
	FirstName            nvarchar(60)    ,
	LastName             nvarchar(80)    ,
	TotalPoints          smallint    ,
	LostPoints           smallint    ,
	[Status]             bit    ,
	CONSTRAINT Pk_Driver_DriverId PRIMARY KEY  ( Id )
 );

CREATE TABLE Infringement ( 
	Id                   uniqueidentifier NOT NULL   ,
	Description          nvarchar(120) NOT NULL   ,
	Points_To_Discount   smallint NOT NULL   ,
	[Status]             bit NOT NULL   ,
	CONSTRAINT Pk_infringement_infringementId PRIMARY KEY  ( Id )
 );

CREATE TABLE Vehicle ( 
	Id                   uniqueidentifier NOT NULL   ,
	Enrollment           nvarchar(15) NOT NULL   ,
	Brand                nvarchar(50)    ,
	Model                nvarchar(50)    ,
	RegularDriver        uniqueidentifier    ,
	[Status]             bit    ,
	CONSTRAINT Pk_Vehicles_Id PRIMARY KEY  ( Id )
 );

CREATE TABLE DriverInfringement ( 
	Id                   uniqueidentifier NOT NULL   ,
	DateCreated          datetime    ,
	InfringementId       uniqueidentifier NOT NULL   ,
	DriverId             uniqueidentifier NOT NULL   ,
	PointDiscount        smallint NOT NULL   ,
	[Status]             bit NOT NULL   ,
	CONSTRAINT Pk_DriverInfringement_DriverInfringementId PRIMARY KEY  ( Id )
 );

CREATE TABLE DriverVehicle ( 
	Id                   uniqueidentifier NOT NULL   ,
	DiverId              uniqueidentifier NOT NULL   ,
	VehicleId            uniqueidentifier NOT NULL   ,
	CONSTRAINT Pk_DriverVehicle_DriverVehicleId PRIMARY KEY  ( Id )
 );

ALTER TABLE DriverInfringement ADD CONSTRAINT fk_driverinfringement FOREIGN KEY ( InfringementId ) REFERENCES Infringement( Id ) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE DriverInfringement ADD CONSTRAINT fk_driverinfringement_driver FOREIGN KEY ( DriverId ) REFERENCES Driver( Id ) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE DriverVehicle ADD CONSTRAINT fk_drivervehicle_driver FOREIGN KEY ( DiverId ) REFERENCES Driver( Id ) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE DriverVehicle ADD CONSTRAINT fk_drivervehicle_vehicles FOREIGN KEY ( VehicleId ) REFERENCES Vehicle( Id ) ON DELETE NO ACTION ON UPDATE NO ACTION;
