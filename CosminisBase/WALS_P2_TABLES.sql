create schema WALS_P2;

create table WALS_P2.users(
		userId int identity,
		username varchar(30) unique not null,
		password varchar(30) not null,
		account_age datetime not null,
		goldCount int,
  		eggCount int,
  		eggTimer datetime not null,
  		notifications int,
  		aboutMe varchar(500),
  		primary key (userId)
 		);

create table WALS_P2.companions(
		companionId int identity,
		user_fk int not null foreign key references WALS_P2.users(userId),
		species_fk int not null foreign key references WALS_P2.species(speciesId),
		nickname varchar(30),
		mood varchar(8) not null check (mood in ('Happy', 'Sad', 'Angry', 'Tired', 'Anxious', 'Excited', 'Chill')),
		hunger int,
		timeSinceLastChangedMood datetime,
		timeSinceLastChangedHunger datetime,
		companion_birthday datetime,
		primary key (companionId)
		);
	
create table WALS_P2.conversation(
		conversationId int identity,
		species_fk int not null foreign key references WALS_P2.species(speciesId),
		quality int not null,
		message varchar (50) not null,
		primary key (conversationId)
		);
	
create table WALS_P2.species(
		speciesId int identity,
		foodElementId_fk int not null foreign key references WALS_P2.foodElement(foodElementId),
		speciesName varchar(25) not null,
		description varchar(255) not null,
		baseStr int,
		baseDex int,
		baseInt int,
		elementType varchar(8) not null check (elementType in ('Volcanic', 'Glacial', 'Forest', 'Sky', 'Holy', 'Dark')),
		primary key (speciesId)
		);
	
	
create table WALS_P2.foodElement(
		foodElementId int identity,
		foodElement varchar(8) not null check (foodElement in ('Spicy', 'Cold', 'Leafy', 'Fluffy', 'Blessed', 'Cursed')),
		primary key (foodElementId)
		);
		
create table WALS_P2.foodStats(
		foodStatsId int identity,
		foodElement_fk int not null foreign key references WALS_P2.foodElement(foodElementId),
		description varchar(255),
		foodName varchar(45) not null,
		hungerRestore int not null,
		primary key (foodStatsId)
		);
	
create table WALS_P2.foodInventory(
		userId_fk int not null foreign key references WALS_P2.users(userId),
		foodStatsId_fk int not null foreign key references WALS_P2.foodStats(foodStatsId),
		foodCount int,
		primary key (userId_fk, foodStatsId_fk)
		);
	
create table WALS_P2.friends(
		relationshipId int identity,
		userId_from int not null foreign key references WALS_P2.users(userId),
		userId_to int not null foreign key references WALS_P2.users(userId),
		status varchar(8) not null check (status in ('Pending', 'Accepted', 'Removed', 'Blocked')),
		primary key (relationshipId)
		);
	
create table WALS_P2.posts(
		postId int identity,
		userId_fk int not null foreign key references WALS_P2.users(userId),
		content varchar(600) not null,
		primary key (postId)
		);
		
create table WALS_P2.likes(
		userId_fk int not null foreign key references WALS_P2.users(userId),
		postId_fk int not null foreign key references WALS_P2.posts(postId),
		primary key (userId_fk, postId_fk)
		);
	
create table WALS_P2.comments(
		commentId int identity,
		userId_fk int not null foreign key references WALS_P2.users(userId),
		postId_fk int not null foreign key references WALS_P2.posts(postId),
		content varchar(255) not null,
		primary key (commentId)
		);

/*COMMAND TO SCAFFOLD
 * 
 * dotnet ef dbcontext scaffold <"Server=tcp:p2dbs.database.windows.net,1433;Initial Catalog=wearelosingsteam;Persist Security Info=False;User ID=wearelosingsteam;Password=weL0stSteam;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"> Microsoft.EntityFrameworkCore.SqlServer --startup-project </DataAccess> --force output -dir Entities --no-onconfiguring
 * 
 * ^Force output dir part might not have the right syntax...
 * 
 */	
insert into WALS_P2.foodStats (foodElement_fk, description, foodName, hungerRestore) values (6,'Wear floaties.', 'Devil Fruit', 50);		

insert into WALS_P2.foodInventory (userId_fk, foodStatsId_fk, foodCount) values (4, 1, -49999);

select * from WALS_P2.friends;
delete from WALS_P2.friends;

update WALS_P2.foodInventory set userId_fk = 4, foodStatsId_fk = 1, foodCount = 10;

drop table WALS_P2.conversation;

--Entries for POSTS--

insert into WALS_P2.emotionChart (quality, emotion) values (10, 'Blissful');
insert into WALS_P2.emotionChart (quality, emotion) values (1, 'Hostile');
insert into WALS_P2.emotionChart (quality, emotion) values (3, 'Distant');
insert into WALS_P2.emotionChart (quality, emotion) values (4, 'Inadequate');
insert into WALS_P2.emotionChart (quality, emotion) values (6, 'Thankful');
insert into WALS_P2.emotionChart (quality, emotion) values (8, 'Playful');
insert into WALS_P2.emotionChart (quality, emotion) values (9, 'Inspired');

--Entries for POSTS--

insert into WALS_P2.posts (userId_fk, content) values (1, 'Hey everyone, this is my first post!');

insert into WALS_P2.posts (userId_fk, content) values (5, 'My name is Jimmy and I really love this social app.');

insert into WALS_P2.posts (userId_fk, content) values (4, 'I just LOVE the companions in this app!');

insert into WALS_P2.posts (userId_fk, content) values (5, 'Hey yall, its me again Jimmy.  I really want to get all the companions this site has to offer.  The graphics are obviously super cool and all my friends wanna register on this website too!');

--Entries for FRIENDS--

insert into WALS_P2.friends (userId_from, userId_to, status) values (7, 2, 'Accepted');

insert into WALS_P2.friends (userId_from, userId_to, status) values (5, 4, 'Pending');

insert into WALS_P2.friends (userId_from, userId_to, status) values (3, 10, 'Blocked');

insert into WALS_P2.friends (userId_from, userId_to, status) values (10, 1, 'Accepted');

insert into WALS_P2.friends (userId_from, userId_to, status) values (10, 7, 'Accepted');

--Entries for LIKES--

insert into WALS_P2.likes (userId_fk, postId_fk) values (1, 4);

insert into WALS_P2.likes (userId_fk, postId_fk) values (2, 4);

insert into WALS_P2.likes (userId_fk, postId_fk) values (3, 2);

--Entries for FOODINVENTORY--

insert into WALS_P2.foodInventory (userId_fk, foodStatsId_fk, foodCount) values (2, 1, 10);

--Entries for FOODELEMENT--

insert into WALS_P2.foodElement (foodElement) values ('Spicy');

insert into WALS_P2.foodStats (foodElement) values ('Cold');

insert into WALS_P2.foodStats (foodElement) values ('Leafy');	

insert into WALS_P2.foodStats (foodElement) values ('Fluffy');	

insert into WALS_P2.foodStats (foodElement) values ('Blessed');	

insert into WALS_P2.foodStats (foodElement) values ('Cursed');	

--Entries for FOODSTATS--

insert into WALS_P2.foodStats (foodElement_fk, description, foodName, hungerRestore) values (1,'Better have [soy] milk!', 'Chili', 50);

insert into WALS_P2.foodStats (foodElement_fk, description, foodName, hungerRestore) values (2,'I bet you wanted a hot lunch but your boss only bought cold cuts...', 'Cold cut sandwich', 50);

insert into WALS_P2.foodStats (foodElement_fk, description, foodName, hungerRestore) values (3,'A salad that gives you rose colored glasses.', '*Special* salad', 50);

insert into WALS_P2.foodStats (foodElement_fk, description, foodName, hungerRestore) values (4,'Make some smores, better have Infernog around.', 'Marshmallows', 50);

insert into WALS_P2.foodStats (foodElement_fk, description, foodName, hungerRestore) values (5,'Become blessed with this beverage.', 'Holy water', 50);

insert into WALS_P2.foodStats (foodElement_fk, description, foodName, hungerRestore) values (6,'Wear floaties!', 'Devil fruit', 50);		

--Entries for USERS--

insert into WALS_P2.users (username, password, goldCount, eggCount, account_age, eggTimer) values ('WaterMelon', 'F1re', 2, 2, GETDATE(), GETDATE());

insert into WALS_P2.users (username, password, goldCount, eggCount) values ('Bruh', 'BoIIIIIIIIIIIIII', 32, 27);

insert into WALS_P2.users (username, password, goldCount, eggCount) values ('Idiot', 'Sandwich', 0, 0);

insert into WALS_P2.users (username, password, goldCount, eggCount) values ('Sally', 'zionz546', 10005, 4);

insert into WALS_P2.users (username, password, goldCount, eggCount) values ('Jimmy', 'doggo402', 31, 0);

--Entries for COMPANIONS--


insert into WALS_P2.species (user_fk, species_fk, nickname, mood, hunger) values (3, 1, 'Supernovog!', 'Happy', 50);

insert into WALS_P2.species (user_fk, species_fk, nickname, mood, hunger) values (1, 4, 'Seth', 'Chill', 69);

insert into WALS_P2.species (user_fk, species_fk, nickname, mood, hunger) values (6, 6, 'Asmodeus', 'Angry', 6);

insert into WALS_P2.species (user_fk, species_fk, nickname, mood, hunger) values (4, 3, 'Test', 'Chill', 100);

--Entries for SPECIES--

insert into WALS_P2.species (foodElementId_fk, speciesName, description, baseStr, baseDex, baseInt, elementType) values (1,'SuperNova', 'Blasts off at the speed of light!', 10, 10, 10, 'Volcanic');

insert into WALS_P2.species (foodElementId_fk, speciesName, description, baseStr, baseDex, baseInt, elementType) values (2,'Pluto', 'Chills shall overcome you!', 10, 10, 10, 'Glacial');

insert into WALS_P2.species (foodElementId_fk, speciesName, description, baseStr, baseDex, baseInt, elementType) values (3,'Buds', 'What they lack in focus, they make up for in chilllllll!', 10, 10, 10, 'Forest');

insert into WALS_P2.species (foodElementId_fk, speciesName, description, baseStr, baseDex, baseInt, elementType) values (4,'Cosmo', 'A nebulous personality!', 10, 10, 10, 'Sky');

insert into WALS_P2.species (foodElementId_fk, speciesName, description, baseStr, baseDex, baseInt, elementType) values (5,'Librian', 'May your rest be eternal.', 10, 10, 10, 'Holy');

insert into WALS_P2.species (foodElementId_fk, speciesName, description, baseStr, baseDex, baseInt, elementType) values (6,'Cancer', 'Blursed friend.', 10, 10, 10, 'Dark');

delete from WALS_P2.foodStats where foodStatsId = 4;

delete from WALS_P2.companions where companionId = 2;

delete from WALS_P2.friends where userId_from = 15;

create table WAL_P2.users;

alter table WALS_P2.users drop column showcaseCompanion_fk;

alter table WALS_P2.users add showcaseCompanion_fk int not null foreign key references WALS_P2.companions(companionId);

alter table sublanguages.users drop constraint likes_cilantro;

--These are for our changes right now on 8/13

alter table WALS_P2.companions add TimeSinceLastFed datetime;

alter table WALS_P2.companions add TimeSinceLastPet datetime;

alter table WALS_P2.species drop column elementType;

alter table WALS_P2.species drop constraint CK__species__element__15DA3E5D;

alter table WALS_P2.species add opposingEle int foreign key references WALS_P2.foodElement(foodElementId);

update WALS_P2.species set opposingEle = 1 where speciesId = 4;

update WALS_P2.species set opposingEle = 2 where speciesId = 3;

update WALS_P2.species set opposingEle = 3 where speciesId = 6;

update WALS_P2.species set opposingEle = 4 where speciesId = 5;

update WALS_P2.species set opposingEle = 5 where speciesId = 8;

update WALS_P2.species set opposingEle = 6 where speciesId = 7;