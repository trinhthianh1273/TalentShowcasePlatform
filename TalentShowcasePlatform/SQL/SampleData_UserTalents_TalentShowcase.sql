
-- Giả định đã có CategoryId được tạo từ các tên cụ thể
-- Giả định người dùng đã có trong bảng Users theo email định danh

-- Talent Anna: Music, Dance
INSERT INTO UserTalents (Id, UserId, CategoryId)
SELECT NEWID(), u.Id, c.Id FROM Users u, Categories c
WHERE u.Email = 'anna@example.com' AND c.Name = 'Music'
UNION ALL
SELECT NEWID(), u.Id, c.Id FROM Users u, Categories c
WHERE u.Email = 'anna@example.com' AND c.Name = 'Dance';

-- Talent Jack: Acting, Comedy
INSERT INTO UserTalents (Id, UserId, CategoryId)
SELECT NEWID(), u.Id, c.Id FROM Users u, Categories c
WHERE u.Email = 'jack@example.com' AND c.Name = 'Acting'
UNION ALL
SELECT NEWID(), u.Id, c.Id FROM Users u, Categories c
WHERE u.Email = 'jack@example.com' AND c.Name = 'Comedy';

-- Mentor Lee: Music, Acting
INSERT INTO UserTalents (Id, UserId, CategoryId)
SELECT NEWID(), u.Id, c.Id FROM Users u, Categories c
WHERE u.Email = 'lee@example.com' AND c.Name = 'Music'
UNION ALL
SELECT NEWID(), u.Id, c.Id FROM Users u, Categories c
WHERE u.Email = 'lee@example.com' AND c.Name = 'Acting';
