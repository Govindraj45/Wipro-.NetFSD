SELECT * FROM Events;
SELECT e.EventName, COUNT(b.BookingID) FROM Events e LEFT JOIN Bookings b ON e.EventID = b.EventID GROUP BY e.EventName;