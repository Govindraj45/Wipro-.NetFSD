SELECT * FROM Inventory WHERE UnitsAvailable < 5;
SELECT d.Name, i.UnitsAvailable FROM Donors d JOIN Inventory i ON d.BloodGroup = i.BloodGroup;
