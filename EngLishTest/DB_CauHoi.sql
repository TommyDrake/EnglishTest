create database DB_CauHoi;
use DB_CauHoi

CREATE TABLE Questions (
    Id INT PRIMARY KEY IDENTITY,
    Content NVARCHAR(255),
    Answer1 NVARCHAR(100),
    Answer2 NVARCHAR(100),
    Answer3 NVARCHAR(100),
    Answer4 NVARCHAR(100),
    CorrectAnswerIndex INT
);
INSERT INTO Questions (Content, Answer1, Answer2, Answer3, Answer4, CorrectAnswerIndex) VALUES
-- 1
(N'Thủ đô của Việt Nam là?', N'Hà Nội', N'Hồ Chí Minh', N'Đà Nẵng', N'Hải Phòng', 0),
-- 2
(N'Quốc khánh Việt Nam là ngày nào?', N'2/9', N'30/4', N'1/5', N'1/6', 0),
-- 3
(N'Chủ tịch Hồ Chí Minh sinh năm bao nhiêu?', N'1890', N'1900', N'1920', N'1885', 0),
-- 4
(N'Đâu là hành tinh lớn nhất trong Hệ Mặt Trời?', N'Mars', N'Venus', N'Jupiter', N'Saturn', 2),
-- 5
(N'Python là gì?', N'Một loài rắn', N'Một ngôn ngữ lập trình', N'Một ca sĩ', N'Một ứng dụng', 1),
-- 6
(N'AI viết tắt của từ nào?', N'Artificial Intelligence', N'Automatic Internet', N'Auto Input', N'Auto Intelligence', 0),
-- 7
(N'Ngày quốc tế phụ nữ là?', N'8/3', N'20/10', N'1/6', N'2/9', 0),
-- 8
(N'Đơn vị đo điện là gì?', N'Mét', N'Giây', N'Volt', N'Watt', 2),
-- 9
(N'CPU là gì?', N'Thiết bị lưu trữ', N'Bộ xử lý trung tâm', N'Màn hình', N'Loa', 1),
-- 10
(N'Trong toán học, số Pi xấp xỉ bằng?', N'3.14', N'2.17', N'4.5', N'1.62', 0);
