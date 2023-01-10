create schema ERS;

create table ERS.users(
     userID int identity,
     username varchar(20) unique not null, 
     password varchar(50) not null,
     userrole varchar(8) not null,
     primary key (userID)
);

drop table ERS.users;
drop table ERS.tickets;

create table ERS.tickets(
    ticketID int identity,
    author int not null foreign key references ERS.users(userID),
    resolver int foreign key references ERS.users(userID),
    ticketDescription varchar(400) not null,
    ticketStatus varchar(8) not null default 'Pending',
    ticketAmount double precision not null,
    primary key (ticketID)
);

insert into ERS.users (username, password, userrole) values ('Sammy','lala5565', 'Employee');
insert into ERS.users (username, password, userrole) values ('Zack','doggo402', 'Manager');
insert into ERS.tickets (author, ticketDescription, ticketAmount) values (1, 'I purchased company office items', 10.00);

select * from ERS.users
select * from ERS.tickets

   
