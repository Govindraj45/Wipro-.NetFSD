-- ==========================================
-- Triggers for Hospital Management System
-- ==========================================

-- 1. AFTER INSERT Trigger on Appointments
CREATE TRIGGER trg_AfterAppointmentInsert
ON Appointments
AFTER INSERT
AS
BEGIN
    INSERT INTO AuditLogs (TableName, Operation, RecordID, ChangedBy)
    SELECT 'Appointments', 'INSERT', inserted.AppointmentID, SYSTEM_USER
    FROM inserted;
END;
GO

-- 2. AFTER UPDATE Trigger on Billing
CREATE TRIGGER trg_AfterBillingUpdate
ON Billing
AFTER UPDATE
AS
BEGIN
    IF UPDATE(PaymentStatus)
    BEGIN
        INSERT INTO AuditLogs (TableName, Operation, RecordID, ChangedBy)
        SELECT 'Billing', 'UPDATE_STATUS', inserted.BillID, SYSTEM_USER
        FROM inserted;
    END
END;
GO

-- 3. AFTER DELETE Trigger on Patients
CREATE TRIGGER trg_AfterPatientDelete
ON Patients
AFTER DELETE
AS
BEGIN
    INSERT INTO AuditLogs (TableName, Operation, RecordID, ChangedBy)
    SELECT 'Patients', 'DELETE', deleted.PatientID, SYSTEM_USER
    FROM deleted;
END;
GO
