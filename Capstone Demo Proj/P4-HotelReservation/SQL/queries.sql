SELECT * FROM Rooms WHERE IsAvailable = 1;
SELECT g.Name, r.RoomType FROM Guests g JOIN Reservations res ON g.GuestID = res.GuestID JOIN Rooms r ON res.RoomID = r.RoomID;