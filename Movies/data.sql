USE DKMovies;
GO

-- COUNTRIES
INSERT INTO Countries (CountryName, Description)
VALUES 
('USA', 'United States of America'),
('UK', 'United Kingdom'),
('Canada', 'Canada');

-- GENRES
INSERT INTO Genres (GenreName, Description)
VALUES 
('Action', 'Action-packed movies'),
('Comedy', 'Comedy genre'),
('Drama', 'Dramatic content');

-- RATINGS
INSERT INTO Ratings (RatingValue, Description)
VALUES 
('G', 'General audiences'),
('PG-13', 'Parents Strongly Cautioned'),
('R', 'Restricted');

-- LANGUAGES
INSERT INTO Languages (LanguageName, Description)
VALUES 
('English', 'Primary language'),
('French', 'Spoken in France and parts of Canada'),
('Spanish', 'Spoken in Spain and Latin America');

-- PAYMENT METHODS
INSERT INTO PaymentMethods (MethodName, Description)
VALUES 
('Credit Card', 'Visa, MasterCard, etc.'),
('PayPal', 'Online payments'),
('Cash', 'Physical currency');

-- EMPLOYEE ROLES
INSERT INTO EmployeeRoles (RoleName, Description)
VALUES 
('Manager', 'Theater Manager'),
('Cashier', 'Handles ticketing and food sales'),
('Cleaner', 'Maintains cleanliness');

-- USERS
INSERT INTO Users (Username, Email, PasswordHash, FullName, Phone, BirthDate, Gender)
VALUES 
('john_doe', 'john@example.com', 'hash1', 'John Doe', '1234567890', '1990-01-01', 'Male'),
('jane_doe', 'jane@example.com', 'hash2', 'Jane Doe', '0987654321', '1992-05-05', 'Female'),
('alex_lee', 'alex@example.com', 'hash123', 'Alex Lee', '5551234567', '1988-11-20', 'Other'),
('sam_kim', 'sam@example.com', 'hash456', 'Sam Kim', '5559876543', '1995-08-14', 'Female');

-- THEATERS
INSERT INTO Theaters (Name, Location, Phone)
VALUES 
('Cineplex A', 'Downtown LA', '123-456-7890'),
('Galaxy B', 'Uptown NY', '234-567-8901');

-- EMPLOYEES (Ensure that the TheaterID for employees is correct)
INSERT INTO Employees (TheaterID, RoleID, FullName, Email, Phone, Gender, DateOfBirth, CitizenID, Address, Salary)
VALUES 
(1, 1, 'Alice Manager', 'alice@theater.com', '1112223333', 'Female', '1980-07-01', 'ABC123', '123 Main St', 5000.00),
(1, 2, 'Bob Cashier', 'bob@theater.com', '2223334444', 'Male', '1990-09-15', 'DEF456', '456 Elm St', 3000.00),
(2, 1, 'Clara Watts', 'clara@galaxy.com', '3334445555', 'Female', '1985-03-12', 'XYZ789', '10 Beach Rd', 5200.00);

-- Assign manager to theater (EmployeeID = 1 assigned to TheaterID = 1)
UPDATE Theaters SET ManagerID = 1 WHERE TheaterID = 1;

-- DIRECTORS
INSERT INTO Directors (FullName, DateOfBirth, Biography, CountryID)
VALUES 
('Steven Spielberg', '1946-12-18', 'Famous American director', 1),
('Christopher Nolan', '1970-07-30', 'British-American film director', 2);

-- MOVIES
INSERT INTO Movies (Title, Description, DurationMinutes, GenreID, RatingID, ReleaseDate, LanguageID, CountryID, DirectorID)
VALUES 
('Epic Action', 'A great action movie.', 120, 1, 2, '2024-01-01', 1, 1, 1),
('Funny Times', 'Laugh out loud.', 90, 2, 1, '2024-03-15', 1, 2, 2);

-- AUDITORIUMS (Insert after theaters)
INSERT INTO Auditoriums (TheaterID, Name, Capacity)
VALUES 
(1, 'Auditorium 1', 100),
(1, 'Auditorium 2', 80),
(2, 'Auditorium 1', 120);

-- SEATS (Insert after auditoriums)
INSERT INTO Seats (AuditoriumID, RowLabel, SeatNumber)
VALUES 
(1, 'A', 1), (1, 'A', 2), (1, 'B', 1), (1, 'B', 2),
(2, 'A', 1), (2, 'A', 2), (2, 'B', 1), (2, 'B', 2);

-- SHOWTIMES (Insert after movies and auditoriums)
INSERT INTO ShowTimes (MovieID, AuditoriumID, StartTime, DurationMinutes, SubtitleLanguageID, Is3D)
VALUES 
(1, 1, '2025-04-21 14:00', 120, 2, 0),
(2, 2, '2025-04-21 17:00', 90, 3, 1);

-- TICKETS (Insert after showtimes)
INSERT INTO Tickets (UserID, ShowTimeID, SeatID, TotalPrice)
VALUES 
(1, 1, 1, 15.00),
(2, 2, 3, 12.50);

-- TICKET PAYMENTS (Insert after tickets)
INSERT INTO TicketPayments (TicketID, MethodID, PaymentStatus, PaidAmount, PaidAt)
VALUES 
(1, 1, 'Completed', 15.00, GETDATE()),
(2, 2, 'Completed', 12.50, GETDATE());

-- MEASUREMENT UNITS
INSERT INTO MeasurementUnits (UnitName, IsContinuous)
VALUES 
('Piece', 0),
('Liter', 1);

-- CONCESSIONS (Insert after measurement units)
INSERT INTO Concessions (Name, Description, Price, StockLeft, UnitID)
VALUES 
('Popcorn', 'Large popcorn', 5.00, 50, 1),
('Soda', 'Cold drink', 3.00, 100, 2);

-- ORDERS (Insert after users)
INSERT INTO Orders (UserID, TotalAmount, OrderStatus)
VALUES 
(1, 8.00, 'Completed'),
(2, 6.00, 'Completed');

-- ORDER ITEMS (Insert after orders and concessions)
INSERT INTO OrderItems (OrderID, ConcessionID, Quantity, PriceAtPurchase)
VALUES 
(1, 1, 1, 5.00),
(1, 2, 1, 3.00),
(2, 2, 2, 3.00);

-- ORDER PAYMENTS (Insert after orders)
INSERT INTO OrderPayments (OrderID, MethodID, PaymentStatus, PaidAmount, PaidAt)
VALUES 
(1, 1, 'Completed', 8.00, GETDATE()),
(2, 3, 'Completed', 6.00, GETDATE());

-- REVIEWS (Insert after movies and users)
INSERT INTO Reviews (MovieID, UserID, Rating, Comment, IsApproved)
VALUES 
(1, 1, 5, 'Amazing movie!', 1),
(2, 2, 4, 'Really funny!', 1);
