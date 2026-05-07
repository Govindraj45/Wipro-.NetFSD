-- ==========================================
-- Functions for Hospital Management System
-- ==========================================

-- 1. Scalar Function: Get Total Paid by Patient
CREATE FUNCTION fn_GetTotalPaidByPatient (@PatientID INT)
RETURNS DECIMAL(10,2)
AS
BEGIN
    DECLARE @TotalPaid DECIMAL(10,2);
    
    SELECT @TotalPaid = ISNULL(SUM(b.TotalAmount), 0)
    FROM Billing b
    JOIN Treatments t ON b.TreatmentID = t.TreatmentID
    JOIN Appointments a ON t.AppointmentID = a.AppointmentID
    WHERE a.PatientID = @PatientID AND b.PaymentStatus = 'Paid';
    
    RETURN @TotalPaid;
END;
GO

-- 2. Table-Valued Function: Get Appointments by Date Range
CREATE FUNCTION fn_GetAppointmentsByDateRange (@StartDate DATETIME, @EndDate DATETIME)
RETURNS TABLE
AS
RETURN
(
    SELECT 
        AppointmentID,
        PatientID,
        DoctorID,
        AppointmentDate,
        Status
    FROM Appointments
    WHERE AppointmentDate BETWEEN @StartDate AND @EndDate
);
GO
