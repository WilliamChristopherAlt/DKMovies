USE DKMovies;
GO

-- COUNTRIES
CREATE TABLE Countries (
    CountryID INT IDENTITY PRIMARY KEY,
    CountryName NVARCHAR(100) UNIQUE NOT NULL,
    Description NVARCHAR(255)
);

-- GENRES
CREATE TABLE Genres (
    GenreID INT IDENTITY PRIMARY KEY,
    GenreName NVARCHAR(100) UNIQUE NOT NULL,
    Description NVARCHAR(255)
);

-- RATINGS
CREATE TABLE Ratings (
    RatingID INT IDENTITY PRIMARY KEY,
    RatingValue NVARCHAR(10) UNIQUE NOT NULL,
    Description NVARCHAR(255)
);

-- DIRECTORS
CREATE TABLE Directors (
    DirectorID INT IDENTITY PRIMARY KEY,
    FullName NVARCHAR(255) NOT NULL,
    DateOfBirth DATE,
    Biography NVARCHAR(MAX),
    CountryID INT FOREIGN KEY REFERENCES Countries(CountryID) ON DELETE SET NULL
);

-- LANGUAGES
CREATE TABLE Languages (
    LanguageID INT IDENTITY PRIMARY KEY,
    LanguageName NVARCHAR(100) NOT NULL UNIQUE,
    Description NVARCHAR(MAX)
);


-- USERS
CREATE TABLE Users (
    UserID INT IDENTITY PRIMARY KEY,
    Username NVARCHAR(100) UNIQUE NOT NULL,
    Email NVARCHAR(255) UNIQUE NOT NULL,
    PasswordHash NVARCHAR(255) NOT NULL,
    FullName NVARCHAR(255),
    Phone NVARCHAR(20),
    BirthDate DATE,
    Gender NVARCHAR(10) CHECK (Gender IN ('Male', 'Female', 'Other')),
    ProfileImagePath NVARCHAR(500),
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- THEATERS
CREATE TABLE Theaters (
    TheaterID INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL,
    Location NVARCHAR(255) NOT NULL,
    Phone NVARCHAR(20),
    ManagerID INT
);

-- AUDITORIUMS
CREATE TABLE Auditoriums (
    AuditoriumID INT IDENTITY PRIMARY KEY,
    TheaterID INT FOREIGN KEY REFERENCES Theaters(TheaterID) ON DELETE CASCADE,
    Name NVARCHAR(50) NOT NULL,
    Capacity INT NOT NULL
);

-- SEATS
CREATE TABLE Seats (
    SeatID INT IDENTITY PRIMARY KEY,
    AuditoriumID INT FOREIGN KEY REFERENCES Auditoriums(AuditoriumID) ON DELETE CASCADE,
    RowLabel CHAR(1) NOT NULL,
    SeatNumber INT NOT NULL,
    CONSTRAINT UC_Seat UNIQUE (AuditoriumID, RowLabel, SeatNumber)
);

-- MOVIES
CREATE TABLE Movies (
    MovieID INT IDENTITY PRIMARY KEY,
    Title NVARCHAR(255) NOT NULL,
    Description NVARCHAR(MAX),
    DurationMinutes INT NOT NULL,
    GenreID INT FOREIGN KEY REFERENCES Genres(GenreID),
    RatingID INT FOREIGN KEY REFERENCES Ratings(RatingID),
    ReleaseDate DATE,
    LanguageID INT FOREIGN KEY REFERENCES Languages(LanguageID),
    CountryID INT FOREIGN KEY REFERENCES Countries(CountryID),
    DirectorID INT FOREIGN KEY REFERENCES Directors(DirectorID),
    ImagePath NVARCHAR(500)
);

-- SHOWTIMES
CREATE TABLE ShowTimes (
    ShowTimeID INT IDENTITY PRIMARY KEY,
    MovieID INT NOT NULL,
    AuditoriumID INT NOT NULL,
    StartTime DATETIME NOT NULL,
    DurationMinutes INT NOT NULL,
    SubtitleLanguageID INT NULL,
    Is3D BIT NOT NULL DEFAULT 0,
    CONSTRAINT FK_ShowTimes_Movie FOREIGN KEY (MovieID) REFERENCES Movies(MovieID) ON DELETE CASCADE,
    CONSTRAINT FK_ShowTimes_Auditorium FOREIGN KEY (AuditoriumID) REFERENCES Auditoriums(AuditoriumID) ON DELETE CASCADE,
    CONSTRAINT FK_ShowTimes_SubtitleLanguage FOREIGN KEY (SubtitleLanguageID) REFERENCES Languages(LanguageID) ON DELETE NO ACTION
);

-- TICKETS
CREATE TABLE Tickets (
    TicketID INT IDENTITY PRIMARY KEY,
    UserID INT FOREIGN KEY REFERENCES Users(UserID) ON DELETE CASCADE,
    ShowTimeID INT FOREIGN KEY REFERENCES ShowTimes(ShowTimeID) ON DELETE CASCADE,
    SeatID INT FOREIGN KEY REFERENCES Seats(SeatID) ON DELETE NO ACTION,
    PurchaseTime DATETIME DEFAULT GETDATE(),
    TotalPrice DECIMAL(10, 2) NOT NULL,
    CONSTRAINT UC_SeatBooking UNIQUE (SeatID, ShowTimeID)
);

-- PAYMENT METHODS
CREATE TABLE PaymentMethods (
    MethodID INT IDENTITY PRIMARY KEY,
    MethodName NVARCHAR(50) UNIQUE NOT NULL,
    Description NVARCHAR(255)
);

-- TICKET PAYMENTS
CREATE TABLE TicketPayments (
    PaymentID INT IDENTITY PRIMARY KEY,
    TicketID INT FOREIGN KEY REFERENCES Tickets(TicketID) ON DELETE CASCADE,
    MethodID INT FOREIGN KEY REFERENCES PaymentMethods(MethodID),
    PaymentStatus NVARCHAR(50) CHECK (PaymentStatus IN ('Pending', 'Completed', 'Failed')) NOT NULL,
    PaidAmount DECIMAL(10, 2),
    PaidAt DATETIME
);

-- EMPLOYEE ROLES
CREATE TABLE EmployeeRoles (
    RoleID INT IDENTITY PRIMARY KEY,
    RoleName NVARCHAR(100) UNIQUE NOT NULL,
    Description NVARCHAR(255)
);

-- EMPLOYEES
CREATE TABLE Employees (
    EmployeeID INT IDENTITY PRIMARY KEY,
    TheaterID INT FOREIGN KEY REFERENCES Theaters(TheaterID) ON DELETE CASCADE,
    RoleID INT FOREIGN KEY REFERENCES EmployeeRoles(RoleID),
    FullName NVARCHAR(255) NOT NULL,
    Email NVARCHAR(255) UNIQUE NOT NULL,
    Phone NVARCHAR(20),
    Gender NVARCHAR(10) CHECK (Gender IN ('Male', 'Female', 'Other')),
    DateOfBirth DATE,
    CitizenID NVARCHAR(50) UNIQUE,
    Address NVARCHAR(255),
    HireDate DATE DEFAULT GETDATE(),
    Salary DECIMAL(10, 2) NOT NULL
);

-- UNIT TYPES
CREATE TABLE MeasurementUnits (
    UnitID INT IDENTITY PRIMARY KEY,
    UnitName NVARCHAR(50) UNIQUE NOT NULL,
    IsContinuous BIT NOT NULL
);

-- CONCESSIONS
CREATE TABLE Concessions (
    ConcessionID INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255),
    Price DECIMAL(6, 2) NOT NULL,
    StockLeft INT NOT NULL CHECK (StockLeft >= 0),
    UnitID INT FOREIGN KEY REFERENCES MeasurementUnits(UnitID),
    IsAvailable BIT DEFAULT 1,
    ImagePath NVARCHAR(500)
);

-- ORDERS
CREATE TABLE Orders (
    OrderID INT IDENTITY PRIMARY KEY,
    UserID INT FOREIGN KEY REFERENCES Users(UserID) ON DELETE CASCADE,
    OrderTime DATETIME DEFAULT GETDATE(),
    TotalAmount DECIMAL(10, 2),
    OrderStatus NVARCHAR(50) CHECK (OrderStatus IN ('Pending', 'Completed', 'Cancelled')) DEFAULT 'Pending'
);

-- ORDER ITEMS
CREATE TABLE OrderItems (
    OrderItemID INT IDENTITY PRIMARY KEY,
    OrderID INT FOREIGN KEY REFERENCES Orders(OrderID) ON DELETE CASCADE,
    ConcessionID INT FOREIGN KEY REFERENCES Concessions(ConcessionID),
    Quantity INT NOT NULL CHECK (Quantity > 0),
    PriceAtPurchase DECIMAL(6, 2) NOT NULL
);

-- ORDER PAYMENTS
CREATE TABLE OrderPayments (
    PaymentID INT IDENTITY PRIMARY KEY,
    OrderID INT FOREIGN KEY REFERENCES Orders(OrderID) ON DELETE CASCADE,
    MethodID INT FOREIGN KEY REFERENCES PaymentMethods(MethodID),
    PaymentStatus NVARCHAR(50) CHECK (PaymentStatus IN ('Pending', 'Completed', 'Failed')) NOT NULL,
    PaidAmount DECIMAL(10, 2),
    PaidAt DATETIME
);

-- REVIEWS
CREATE TABLE Reviews (
    ReviewID INT IDENTITY PRIMARY KEY,
    MovieID INT FOREIGN KEY REFERENCES Movies(MovieID) ON DELETE CASCADE,
    UserID INT FOREIGN KEY REFERENCES Users(UserID) ON DELETE CASCADE,
    Rating INT CHECK (Rating BETWEEN 1 AND 5) NOT NULL,
    Comment NVARCHAR(MAX),
    CreatedAt DATETIME DEFAULT GETDATE(),
    IsApproved BIT DEFAULT 0
);

-- THEATER MANAGER FK
ALTER TABLE Theaters
ADD CONSTRAINT FK_Theater_Manager
FOREIGN KEY (ManagerID) REFERENCES Employees(EmployeeID);
