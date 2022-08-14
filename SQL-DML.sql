--
-- Select waitlist entries that have not yet secured their puppy
--
SELECT      WaitList.Id, LastName, FirstName, PhoneNumber, AmountOwed
FROM        WaitList
INNER JOIN  Person
ON          PersonId = Person.Id
AND         Person.IsActive = 1
WHERE       WaitList.IsActive = 1
AND         AmountOwed > 0
ORDER BY    AmountOwed ASC;

--
-- Select waitlist entries that have secured their puppy
--
SELECT      WaitList.Id, LastName, FirstName, PhoneNumber, DatePuppySecured
FROM        WaitList
INNER JOIN  Person
ON          PersonId = Person.Id
AND         Person.IsActive = 1
WHERE       WaitList.IsActive = 1
AND         DatePuppySecured IS NOT NULL
ORDER BY    DatePuppySecured ASC;