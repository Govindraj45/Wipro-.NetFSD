SELECT * FROM Shows WHERE SeatsAvailable > 0;
SELECT u.Name, s.Title FROM Users u JOIN Tickets t ON u.UserID = t.UserID JOIN Shows s ON t.ShowID = s.ShowID;