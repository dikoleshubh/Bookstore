
create table payroll_service(EmployeeId int identity(1,1),EmployeeName varchar(20),PhoneNumber varchar(10) NOT NULL, Address varchar(20) NOT NULL, Department varchar(20) NOT NULL,Gender char(1) NOT NULL,BasicPay float NOT NULL , Deductions float NOT NULL,TaxablePay float NOT NULL ,Tax float  NOT NULL ,NetPay float NOT NULL , StartDate DATETIME DEFAULT GETDATE(),City varchar(10) DEFAULT 'Pune',Country varchar(10) DEFAULT 'IN');
select * from payroll_service;
insert into payroll_service(EmployeeName,PhoneNumber,Address,Department,Gender,BasicPay,Deductions,TaxablePay,Tax,NetPay) values('Champa','3735431','Pune','HR','F','20000','2000','2000','200','18000');
select * from payroll_service;