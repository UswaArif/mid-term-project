Select ca.Id as ClassAttendanceId,sa.StudentId,s.Name,s.RegistrationNumber,ca.AttendanceDate,
CASE WHEN sa.AttendanceStatus = '1' THEN 'Present' 
     WHEN sa.AttendanceStatus = '2' THEN 'Absent'
	 WHEN sa.AttendanceStatus = '3' THEN 'Leave'
	 Else 'Late'
END as AttendanceStatus 
from ClassAttendance as ca 
INNER JOIN (Select AttendanceId,StudentId,AttendanceStatus from StudentAttendance) as sa ON sa.AttendanceId = ca.Id INNER JOIN
(Select FirstName+' '+LastName as Name,Id,RegistrationNumber from Student) as s ON s.Id=sa.StudentId where sa.StudentId =1



Select sr.StudentId,s.FirstName+' '+s.LastName as Name,s.RegistrationNumber,ac.Name as Component,r.CloId,
(cast(cast(rl.MeasurementLevel as decimal(10,2))/4 * cast((ac.TotalMarks) as decimal(10,2)) as decimal(10,2)))
as ObtainedMarks,ac.TotalMarks,rl.MeasurementLevel from StudentResult as sr 
INNER JOIN (
Select Id,FirstName,LastName,RegistrationNumber from Student 
)as s ON sr.StudentId = s.Id  INNER JOIN (Select MeasurementLevel,Id from RubricLevel 
) as rl ON rl.Id = sr.RubricMeasurementId and
s.Id=sr.StudentId  INNER JOIN (Select Id,TotalMarks,Name,RubricId from AssessmentComponent ) as ac 
ON ac.Id = sr.AssessmentComponentId
INNER JOIN (Select Id,cloId from Rubric) as r ON r.Id = ac.RubricId INNER JOIN
(Select Name,Id from Clo) as c ON c.Id=r.CloId
where c.Name = 'CLO2'

Select ca.Id as ClassAttendanceId,sa.StudentId,s.Name,s.RegistrationNumber,ca.AttendanceDate,
CASE WHEN sa.AttendanceStatus = '1' THEN 'Present' 
     WHEN sa.AttendanceStatus = '2' THEN 'Absent'
	 WHEN sa.AttendanceStatus = '3' THEN 'Leave'
	 Else 'Late'
END as AttendanceStatus 
from ClassAttendance as ca 
INNER JOIN (Select AttendanceId,StudentId,AttendanceStatus from StudentAttendance) as sa ON sa.AttendanceId = ca.Id INNER JOIN
(Select FirstName+' '+LastName as Name,Id,RegistrationNumber from Student) as s ON s.Id=sa.StudentId where ca.Id=1

/* assessment*/
/*Select sr.StudentId,s.FirstName+' '+s.LastName as Name,s.RegistrationNumber,ac.Name as Component,r.CloId,
SUM(cast(cast(rl.MeasurementLevel as decimal(10,2))/4 * cast((ac.TotalMarks) as decimal(10,2)) as decimal(10,2)))
as ObtainedMarks,a.TotalMarks,rl.MeasurementLevel from StudentResult as sr 
INNER JOIN (
Select Id,FirstName,LastName,RegistrationNumber from Student 

)as s ON sr.StudentId = s.Id  INNER JOIN (Select MeasurementLevel,Id from RubricLevel 
) as rl ON rl.Id = sr.RubricMeasurementId and
s.Id=sr.StudentId  INNER JOIN (Select Id,TotalMarks,Name,RubricId,AssessmentId from AssessmentComponent ) as ac 
ON ac.Id = sr.AssessmentComponentId
INNER JOIN (Select Id,cloId from Rubric) as r ON r.Id = ac.RubricId INNER JOIN
(Select Id,TotalMarks from Assessment) as a ON a.Id=ac.AssessmentId GROUP BY sr.StudentId*/
SELECT sr.StudentId, s.FirstName+' '+s.LastName AS Name, s.RegistrationNumber, 
SUM(CAST(CAST(rl.MeasurementLevel AS DECIMAL(10,2))/4 * CAST((ac.TotalMarks) AS DECIMAL(10,2)) AS DECIMAL(10,2))) AS ObtainedMarks, a.TotalMarks as TotalAssessmentMarks,a.Title as AssessmentName
FROM StudentResult AS sr 
INNER JOIN (
    SELECT Id, FirstName, LastName, RegistrationNumber 
    FROM Student 
) AS s ON sr.StudentId = s.Id  
INNER JOIN (
    SELECT MeasurementLevel, Id 
    FROM RubricLevel 
) AS rl ON rl.Id = sr.RubricMeasurementId AND s.Id = sr.StudentId  
INNER JOIN (
    SELECT Id, TotalMarks, Name, RubricId, AssessmentId 
    FROM AssessmentComponent 
) AS ac ON ac.Id = sr.AssessmentComponentId
INNER JOIN (
    SELECT Id, cloId 
    FROM Rubric
) AS r ON r.Id = ac.RubricId 
INNER JOIN (
    SELECT Id, TotalMarks,Title 
    FROM Assessment
) AS a ON a.Id = ac.AssessmentId where a.Title = 'Lab1'
GROUP BY sr.StudentId, s.FirstName, s.LastName, s.RegistrationNumber, a.TotalMarks,a.Title 


/*Select sr.StudentId from StudentResult as sr INNER JOIN
(Select AssessmentId,Id from AssessmentComponent) as ac ON ac.Id = sr.StudentId INNER JOIN
(Select Id,TotalMarks from Assessment) as a ON a.Id=ac.AssessmentId

Select sr.StudentId,s.FirstName+' '+s.LastName as Name,s.RegistrationNumber,
(cast(cast(rl.MeasurementLevel as decimal(10,2))/4 * cast((ac.TotalMarks) as decimal(10,2)) as decimal(10,2)))
as ObtainedMarks,a.TotalMarks from StudentResult as sr 
INNER JOIN (
Select Id,FirstName,LastName,RegistrationNumber from Student 

)as s ON sr.StudentId = s.Id  INNER JOIN (Select MeasurementLevel,Id from RubricLevel 
) as rl ON rl.Id = sr.RubricMeasurementId and
s.Id=sr.StudentId  INNER JOIN (Select Id,TotalMarks,Name,RubricId,AssessmentId from AssessmentComponent ) as ac 
ON ac.Id = sr.AssessmentComponentId
INNER JOIN (Select Id,cloId from Rubric) as r ON r.Id = ac.RubricId INNER JOIN
(Select Id,TotalMarks from Assessment) as a ON a.Id=ac.AssessmentId*/

SELECT sr.StudentId, s.FirstName+' '+s.LastName AS Name, s.RegistrationNumber, 
SUM(CAST(CAST(rl.MeasurementLevel AS DECIMAL(10,2))/4 * CAST((ac.TotalMarks) AS DECIMAL(10,2)) AS DECIMAL(10,2))) AS ObtainedMarks, 
(SELECT SUM(TotalMarks) FROM Assessment) AS TotalMarks
FROM StudentResult AS sr 
INNER JOIN (
    SELECT Id, FirstName, LastName, RegistrationNumber 
    FROM Student 
) AS s ON sr.StudentId = s.Id  
INNER JOIN (
    SELECT MeasurementLevel, Id 
    FROM RubricLevel 
) AS rl ON rl.Id = sr.RubricMeasurementId AND s.Id = sr.StudentId  
INNER JOIN (
    SELECT Id, TotalMarks, Name, RubricId, AssessmentId 
    FROM AssessmentComponent 
) AS ac ON ac.Id = sr.AssessmentComponentId
where sr.StudentId = 1
GROUP BY sr.StudentId, s.FirstName, s.LastName, s.RegistrationNumber












