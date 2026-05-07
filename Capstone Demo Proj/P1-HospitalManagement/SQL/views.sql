-- ==========================================
-- Views for Hospital Management System
-- ==========================================

-- 1. Simple View: Active Doctors
CREATE VIEW vw_ActiveDoctors AS
SELECT DoctorID, FirstName, LastName, Specialization, ContactNumber
FROM Doctors;
GO

-- 2. Join-based View: Patient Appointment Details
CREATE VIEW vw_PatientAppointments AS
SELECT 
    a.AppointmentID,
    p.FirstName + ' ' + p.LastName AS PatientName,
    d.FirstName + ' ' + d.LastName AS DoctorName,
    d.Specialization,
    a.AppointmentDate,
    a.Status,
    a.ReasonForVisit
FROM Appointments a
JOIN Patients p ON a.PatientID = p.PatientID
JOIN Doctors d ON a.DoctorID = d.DoctorID;
GO

-- 3. Aggregate View: Revenue by Department
CREATE VIEW vw_RevenueBySpecialization AS
SELECT 
    d.Specialization,
    SUM(b.TotalAmount) AS TotalRevenue,
    COUNT(t.TreatmentID) AS TotalTreatments
FROM Billing b
JOIN Treatments t ON b.TreatmentID = t.TreatmentID
JOIN Appointments a ON t.AppointmentID = a.AppointmentID
JOIN Doctors d ON a.DoctorID = d.DoctorID
WHERE b.PaymentStatus = 'Paid'
GROUP BY d.Specialization;
GO
