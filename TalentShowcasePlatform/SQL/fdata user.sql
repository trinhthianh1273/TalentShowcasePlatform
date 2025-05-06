INSERT INTO Users (UserName, Email, PasswordHash, Bio, AvatarUrl, Location, RoleId, CreatedAt)
VALUES
('johndoe', 'johndoe@gmail.com', '12345678', 'Software developer and coffee lover.', 'johndoe_avt.jpg', 'New York, USA', NEWID(), GETDATE()),
('janesmith', 'jane.smith@yahoo.com', '12345678', 'Traveler, blogger, and foodie.', 'janesmith_avt.jpg', 'Los Angeles, USA', NEWID(), GETDATE()),
('michael89', 'michael.89@hotmail.com', '12345678', 'Photographer & designer.', 'michael89_avt.jpg', 'Toronto, Canada', NEWID(), GETDATE()),
('emily_rose', 'emily.rose@gmail.com', '12345678', 'Passionate about writing and art.', 'michael89.avt.jpg', 'London, UK', NEWID(), GETDATE()),
('david_k', 'david.kim@outlook.com', '12345678', 'Tech geek. Gamer. Dreamer.', 'david_k.jpg', 'Seoul, South Korea', NEWID(), GETDATE()),
('lucyliu', 'lucy.liu@gmail.com', '12345678', 'Marketing strategist and dog mom.', 'lucyliu.jpg', 'Singapore', NEWID(), GETDATE()),
('marco_v', 'marco.v@protonmail.com', '12345678', 'Love building things with code.', 'marco_v.jpg', 'Berlin, Germany', NEWID(), GETDATE()),
('sofia.g', 'sofia.gonzalez@gmail.com', '12345678', 'Nature enthusiast and yogi.', 'sofiag.jpg', 'Madrid, Spain', NEWID(), GETDATE()),
('alexchan', 'alex.chan@yahoo.com', '12345678', 'Engineer. Problem solver.', 'alexchan.jpg', 'Hong Kong', NEWID(), GETDATE()),
('natalie_b', 'natalie.bennet@live.com', '12345678', 'Writer and coffee addict.', 'natalieb.jpg', 'Melbourne, Australia', NEWID(), GETDATE());
