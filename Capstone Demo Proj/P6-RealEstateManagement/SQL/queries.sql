SELECT * FROM Properties WHERE Status = 'Available';
SELECT p.Address, c.Name FROM Properties p JOIN Transactions t ON p.PropID = t.PropID JOIN Clients c ON t.BuyerID = c.ClientID;