
-- Insert 4 predefined roles
INSERT INTO Roles (Id, Name) VALUES
(NEWID(), 'Talent'),
(NEWID(), 'Mentor'),
(NEWID(), 'Admin'),
(NEWID(), 'Recruiter');

-- Insert 6 sample users (1 Admin, 2 Talents, 1 Mentor, 2 Recruiters)
INSERT INTO Users (Id, UserName, Email, PasswordHash, Bio, AvatarUrl, RoleId, CreatedAt)
SELECT NEWID(), 'admin_user', 'admin@example.com', 'hashed_password', 'System administrator', '', Id, GETDATE()
FROM Roles WHERE Name = 'Admin'
UNION ALL
SELECT NEWID(), 'talent_anna', 'anna@example.com', 'hashed_password', 'Singer and performer', '', Id, GETDATE()
FROM Roles WHERE Name = 'Talent'
UNION ALL
SELECT NEWID(), 'talent_jack', 'jack@example.com', 'hashed_password', 'Dancer and actor', '', Id, GETDATE()
FROM Roles WHERE Name = 'Talent'
UNION ALL
SELECT NEWID(), 'mentor_lee', 'lee@example.com', 'hashed_password', 'Coach and industry expert', '', Id, GETDATE()
FROM Roles WHERE Name = 'Mentor'
UNION ALL
SELECT NEWID(), 'recruiter_lisa', 'lisa@example.com', 'hashed_password', 'Casting director at TalentSpot', '', Id, GETDATE()
FROM Roles WHERE Name = 'Recruiter'
UNION ALL
SELECT NEWID(), 'recruiter_mark', 'mark@example.com', 'hashed_password', 'Talent agent at BrightFuture', '', Id, GETDATE()
FROM Roles WHERE Name = 'Recruiter';
